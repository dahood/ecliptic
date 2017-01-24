if(Test-Path Function:\Get-PfxCertificate){
    Move Function:\Get-PfxCertificate Function:\Get-PfxCertificateOriginal
}
if(!$env:OPENSSL_CONF){
    $here = Split-Path $script:MyInvocation.MyCommand.Path
    $env:OPENSSL_CONF = Resolve-Path "$here\openssl.cfg"
}
<#
    .SYNOPSIS
        Extends the default Get-PfxCertificate function to add 'password' support.

    .DESCRIPTION
        Since the default Get-PfxCertificate function only prompts for a password, it's difficult to automate the process. This function adds password support to the default function

    .PARAMETER filePath
        A relative path to the pfx file

    .PARAMETER literalPath
        The literal path to the pfx file

    .PARAMETER password
        The password associated with the pfx file

    .PARAMETER x509KeyStorageFlag
        Defines where and how to import the private key of an X.509 certificate.
        Valid flags are: [DefaultKeySet, Exportable, MachineKeySet, PersistKeySet, UserKeySet, UserProtected]

    .EXAMPLE
        Get-PfxCertificate "$here\myCert.pfx" 'P@$$W0rd'

    .EXAMPLE
        Get-PfxCertificate -literalPath "C:\certs\myCert.pfx" 'P@$$W0rd'

    .EXAMPLE
        Get-PfxCertificate ".\myCert.pfx" 'P@$$W0rd' 'UserKeySet'

    .EXAMPLE
        Get-PfxCertificate "$here\myCert.pfx"
        This simply calls the original method

    .EXAMPLE
        Get-PfxCertificate -literalPath "C:\certs\myCert.pfx"
        This simply calls the original method with a literal path

    .NOTES
        the -x509KeyStorageFlag flag is only used if you are also passing in a -password

#>
function Get-PfxCertificate {
    [CmdletBinding(DefaultParameterSetName='ByPath')]
    param(
        [Parameter(Position=0, Mandatory=$true, ParameterSetName='ByPath')] [string[]] $filePath,
        [Parameter(Mandatory=$true, ParameterSetName='ByLiteralPath')] [string[]] $literalPath,
        
        [Parameter(Position=1, ParameterSetName='ByPath')] 
        [Parameter(Position=1, ParameterSetName='ByLiteralPath')] [string] $password,

        [Parameter(Position=2, ParameterSetName='ByPath')]
        [Parameter(Position=2, ParameterSetName='ByLiteralPath')] [string] 
        [ValidateSet('DefaultKeySet','Exportable','MachineKeySet','PersistKeySet','UserKeySet','UserProtected')] $x509KeyStorageFlag = 'DefaultKeySet'
    )

    if($PsCmdlet.ParameterSetName -eq 'ByPath'){
        $literalPath = Resolve-Path $filePath 
    }

    if(!$password){
        $cert = Get-PfxCertificateOriginal -literalPath $literalPath
    } else {
        $cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
        $cert.Import($pfxFilePath, $password, $X509KeyStorageFlag)
    }

    return $cert
}

<#
    .SYNOPSIS
        Generates a new Private Key (.key) and a Certificate Signing Request (.csr)

    .DESCRIPTION
        Uses openssl.exe to Generate a new Private Key and a Certificate Signing Request. 
        The two files that will be created will be [name].key and [name].csr

    .NOTES
        Please ensure openssl.exe is available on your $env:PATH

    .PARAMETER name
        The name associated with your new certificate

    .PARAMETER password
        The password associated with your new certificate

    .PARAMETER subject
        The subject information associated with your new certificate

    .PARAMETER outPath
        The location where your new certificates will be dropped

    .Example
        New-PrivateKeyAndCertificateSigningRequest 'cert-name' 'certP@ssword" '/CN=bla' 'C:\path\to\output'
#>
function New-PrivateKeyAndCertificateSigningRequest{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,position=0)][string]$name,
        [Parameter(Mandatory=$true,position=1)][string][alias('pwd')]$password, 
        [Parameter(Mandatory=$true,position=2)][alias('cn')][ValidateScript({$_.Contains('CN=') -and $_.StartsWith('/')})][string]$subject, 
        [Parameter(Mandatory=$true,position=3)][string][alias('out')]$outPath
    )
    
    $env:RANDFILE = $RANDFILE = "$outpath\.rnd"

    $key = "$name.key"
    $csr = "$name.csr"

    try{
        Push-Location $outPath
        Exec { openssl.exe req -nodes -newkey rsa:2048 -keyout $key -out $csr -subj $subject | out-null } 
    } finally {
        Pop-Location
        Remove-Item "$outpath\.rnd" -Force -ErrorAction SilentlyContinue
    }
    
    Write-Output @{
        'path' = $outPath
        'key' = $key
        'csr' = $csr
        'name' = $name
        'subject' = $subject
    }
}

<#
    .SYNOPSIS
        Generates a new Private Key (.key)

    .DESCRIPTION
        Uses openssl.exe to Generate a new Private Key . 
        The newly created file will be [name].key

    .NOTES
        Please ensure openssl.exe is available on your $env:PATH

    .PARAMETER name
        The name associated with your new certificate

    .PARAMETER password
        The password associated with your new certificate

    .PARAMETER subject
        The subject information associated with your new certificate

    .PARAMETER outPath
        The location where your new certificates will be dropped

    .Example
        New-PrivateKey 'cert-name' 'certP@ssword" '/CN=bla' 'C:\path\to\output'
#>
function New-PrivateKey {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,position=0)][string]$name,
        [Parameter(Mandatory=$true,position=1)][string][alias('pwd')]$password, 
        [Parameter(Mandatory=$true,position=2)][alias('cn')][ValidateScript({$_.Contains('CN=') -and $_.StartsWith('/')})][string]$subject, 
        [Parameter(Mandatory=$true,position=3)][alias('out')][string]$outPath
    )
    
    $env:RANDFILE = $RANDFILE = "$outpath\.rnd"

    $key = "$name.key"

    try {
        Push-Location $outPath
        Exec {openssl.exe genrsa -passout pass:$password -out $key 2048 -subj $subject -noverify  | out-null} 
    } finally {
        Pop-Location
        Remove-Item "$outpath\.rnd" -Force -ErrorAction SilentlyContinue
    }
    
    Write-Output @{
        'path' = $outPath
        'key' = $key
        'name' = $name
        'subject' = $subject
    }
}


<#
    .SYNOPSIS
        Generates a new Certificate Signing Request (.csr)

    .DESCRIPTION
        Uses openssl.exe to Generate a new Certificate Signing Request. 
        The newly created file will be [name].csr

    .NOTES
        Please ensure openssl.exe is available on your $env:PATH

    .PARAMETER certData
        The hashtable containing all of the required data (useful when piping calls).

    .PARAMETER outPath
        The location where your new certificate will be dropped

    .PARAMETER key
        Name of the certificate key (name.key)
        Must reside in the $outPath

    .PARAMETER csr
        Name of the certificate signing request (name.csr)
        Must reside in the $outPath

    .PARAMETER name
        The name associated with your new certificate

    .Example
        New-CertificateSigningRequest 'cert-name' 'C:\path\to\output' '/CN=bla' 'cert-name.key'
#>
function New-CertificateSigningRequest {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,position=0, ValueFromPipeline=$True, ParameterSetName='a')][hashtable]$certData,
        
        [Parameter(Mandatory=$true,position=0, ParameterSetName='b')] [string] $name,
        [Parameter(Mandatory=$true,position=1, ParameterSetName='b')] [alias('out')][string] $outPath,
        [Parameter(Mandatory=$true,position=2, ParameterSetName='b')] [alias('cn')][ValidateScript({$_.Contains('CN=') -and $_.StartsWith('/')})][string]$subject,
        [Parameter(Mandatory=$true,position=3, ParameterSetName='b')] [string] $key
    )

    if($PsCmdlet.ParameterSetName -eq 'b') {
        # Recursive call using the hashtable
        New-CertificateSigningRequest  @{
            'path' = $outPath
            'key' = $key
            'name' = $name
            'subject' = $subject
        }
    }

    $env:RANDFILE = $RANDFILE = "$outpath\.rnd"

    $csr = "$($certData.name).csr"
    
    try{
        Push-Location $certData.path
        Exec { openssl.exe req -new -key $certData.key -out $csr -subj $certData.subject  | out-null} 
    } finally {
        Pop-Location
        Remove-Item "$outpath\.rnd" -Force -ErrorAction SilentlyContinue
    }
    
    $certData.Add('csr',$csr)
    Write-Output $certData
}

<#
    .SYNOPSIS
        Generates a new Certificate (.crt)

    .DESCRIPTION
        Uses openssl.exe to Generate a new Certificate. 
        The newly created file will be [name].crt

    .NOTES
        Please ensure openssl.exe is available on your $env:PATH

    .PARAMETER certData
        The hashtable containing all of the required data (useful when piping calls).

    .PARAMETER outPath
        The location where your new certificate will be dropped

    .PARAMETER key
        Name of the certificate key (name.key)
        Must reside in the $outPath

    .PARAMETER csr
        Name of the certificate signing request (name.csr)
        Must reside in the $outPath

    .PARAMETER name
        The name associated with your new certificate

    .Example
        New-Certificate 'cert-name' 'C:\path\to\output' 'certName.key' 'certName.csr'
#>
function New-Certificate {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,position=0, ValueFromPipeline=$true, ParameterSetName='a')] [hashtable] $certData,

        [Parameter(Mandatory=$true,position=0, ParameterSetName='b')] [string] $name,
        [Parameter(Mandatory=$true,position=1, ParameterSetName='b')] [alias('out')][string] $outPath,
        [Parameter(Mandatory=$true,position=2, ParameterSetName='b')] [string] $key,
        [Parameter(Mandatory=$true,position=3, ParameterSetName='b')] [string] $csr
    )
    
    if($PsCmdlet.ParameterSetName -eq 'b') {
    # Recursive call using the hashtable
        New-Certificate  @{
            'path' = $outPath
            'key' = $key
            'name' = $name
            'csr' = $csr
        }
    }

    $env:RANDFILE = $RANDFILE = "$outpath\.rnd"
    $crt = "$($certData.name).crt"
    
    try{
        Push-Location $certData.path
        Exec { openssl.exe x509 -req -days 365 -in $certData.csr -signkey $certData.key -out $crt  -text -inform DER | out-null } 
    } finally {
        Pop-Location
        Remove-Item "$outpath\.rnd" -Force -ErrorAction SilentlyContinue
    }

    $certData.Add('crt', $crt)
    Write-output $certData
}

<#
    .SYNOPSIS
        Generates a new Certificate containing both the public and private keys (.pfx)

    .DESCRIPTION
        Uses openssl.exe to Generate a new Public/Private Key pair. 
        The newly created file will be [name].pfx

    .NOTES
        Please ensure openssl.exe is available on your $env:PATH

    .PARAMETER certData
        The hashtable containing all of the required data (useful when piping calls).

    .PARAMETER outPath
        The location where your new certificate will be dropped

    .PARAMETER key
        Name of the certificate key (name.key)
        Must reside in the $outPath

    .PARAMETER crt
        Name of the certificate (name.crt)
        Must reside in the $outPath

    .PARAMETER name
        The name associated with your new certificate

    .PARAMETER password
        The password associated with your new certificate

    .Example
        New-PfxCertificate 'c:\path\to\output' 'certName.key' 'certName.crt" 'certName' 'certP@ssword'
#>
function New-PfxCertificate {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true,position=0, ValueFromPipeline=$true, ParameterSetName='a')] [hashtable] $certData,
        
        [Parameter(Mandatory=$true,position=0, ParameterSetName='b')][alias('out')][string] $outPath,
        [Parameter(Mandatory=$true,position=1, ParameterSetName='b')][string] $key,
        [Parameter(Mandatory=$true,position=2, ParameterSetName='b')][string] $crt,
        [Parameter(Mandatory=$true,position=3, ParameterSetName='b')][string] $name,

        [Parameter(Mandatory=$true,position=1, ParameterSetName='a')]
        [Parameter(Mandatory=$true,position=4, ParameterSetName='b')]
        [alias('pwd')][string] $password
    )

    if($PsCmdlet.ParameterSetName -eq 'b') {
        # Recursive call using the hashtable
        New-PfxCertificate  @{
            'path' = $outPath
            'key' = $key
            'crt' = $crt
            'name' = $name
        }
    }
    
    $env:RANDFILE = $RANDFILE = "$outpath\.rnd"

    $pfx = "$($certData.name).pfx"

    try{
        Push-Location $certData.path
        $key = if(Test-Path $certData.key -erroraction silentlycontinue){ $certData.key } else { GenerateCertFromString $certData.name $certData.key 'key' }
        $crt = if(Test-Path $certData.crt -erroraction silentlycontinue){ $certData.crt } else { GenerateCertFromString $certData.name $certData.crt 'crt' }
    
        Exec { openssl.exe pkcs12 -export -inkey $key -in $crt -out $pfx -name $certData.name  -passout pass:$password | out-null} 
    } finally {
        Pop-Location
        Clear-TemporaryCertificates
        Remove-Item "$outpath\.rnd" -Force -ErrorAction SilentlyContinue
    }
    
    $certData.key = $key
    $certData.crt = $crt
    $certData.Add('pfx', $pfx)
    Write-output $certData
}

# Private Functions

function GenerateCertFromString($name, $value, $extension) {
    if(!($script:TempCerts)){
        $script:TempCerts = @()
    }
    
    $file = "$env:TEMP\$name.$extension"
    $script:TempCerts += $file
    $encoding = [System.Text.Encoding]::GetEncoding('windows-1252')
    [System.IO.File]::WriteAllText($file, $value, $encoding) | out-null
    
    return $file
}

function Clear-TemporaryCertificates {
    if($script:TempCerts) {
        $script:TempCerts | % {
            Remove-Item $_ -Force -erroraction SilentlyContinue
        }
        $script:TempCerts.Clear()
    }
}