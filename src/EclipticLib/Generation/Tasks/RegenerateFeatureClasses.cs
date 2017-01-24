using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace EclipticLib.Generation.Tasks
{
    public class RegenerateFeatureClasses : EclipticTask<ProcessResult>
    {
        private const string Command = @"GenerateSpecflow.bat ";

        public RegenerateFeatureClasses(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override ProcessResult Get()
        {
            var processInfo = new ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Command))
            {
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = Path.Combine(Environment.CurrentDirectory, Properties.AcceptanceTestTempProject)
            };

            var process = Process.Start(processInfo);
            process.WaitForExit();

            var result = new ProcessResult(process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd());

            process.Close();
            return result;
        }
    }

    public class ProcessResult
    {
        public string StandardOutput { get; private set; }
        public string ErrorOutput { get; private set; }

        public ProcessResult(string standardOutput, string errorOutput)
        {
            StandardOutput = standardOutput;
            ErrorOutput = errorOutput;
        }
    }
}