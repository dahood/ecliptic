# Todos - Release 1.0 - Windows release

* Nuget package working with install.ps1
* Nuget package working with 'ecliptic generate'
* Nuget package working with 'ecliptic run'
* Investigate if ecliptic can be a solution-only package (no lib, no build, no content) - but how to bootstrap the powershell?
* Document how ecliptic.properties works in readme.md
* Update examples to use consistent scenario
	* Bank Account
	* Scenario 1 - Opening Balance
	* Scenario 2 - Cash Withdrawl
	* Scenario 3 - Multiple Bank Account Summarization
	* Scenario 4 - Transfers Between Accounts
* Update readme.md to include a few screenshots, easy nuget install procedures, powershell commands - focus on Getting Started steps
* Move to GitHub
* Publish to Nuget.org (create build task Publish, wraps nuget.exe prompts for password)

# Todo - Release 1.1 - Windows improvements

* Report code coverage
* Improve code coverage for libraries, exe, and test deployment of nuget package
* Improvements to Windows run commands, allow for individual feature file to be run (possible generate and run)
* Consider choco package for Ecliptic based on tools only (not project specific)

# Todos - Release 2.0 - Linux release

* Move to Mono - ensure compliant assemblies, might require packaging EPPlus with windows vs unix distribution in Nuget package
* Support for parsing .ods files - OpenDocument format ODF (http://en.wikipedia.org/wiki/OpenDocument) - look at odftoolkit at apache as a possible library - http://incubator.apache.org/odftoolkit/simple/index.html
* Support for running gherkin in bash shell (against Cucumber/ruby stack?) - mirror Powershell commands
* Support for Eclipse projects (Clojure? Java?)