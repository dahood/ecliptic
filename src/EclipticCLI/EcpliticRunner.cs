using System;
using System.IO;
using EclipticLib;
using EclipticLib.Extensions;
using EclipticLib.Generation;
using EclipticLib.Utility.Logging;

namespace EclipticCLI
{
    public class EcpliticRunner
    {
        public void Run(string[] args)
        {
            try
            {
                ValidateCommandLineArguments(args);

                var acceptanceTestDirectory = args[0];
                Log.Debug(x => x($"working on {acceptanceTestDirectory}"));

                AssertAcceptanceTestDirectoryExists(acceptanceTestDirectory);
                var properitesFilePath = acceptanceTestDirectory.LocateFile(Constants.EclipticProperties);
                Log.Debug(x => x($"property {properitesFilePath} found"));

                Environment.CurrentDirectory = properitesFilePath.DirectoryName;
                var properties = new EclipticProperties(properitesFilePath.FullName);

                new EclipticControl(properties).Run();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error:  " + e.Message + " Stack Trace: " + e.StackTrace);
                Prompt("Press enter to exit");
                Environment.Exit(1);
            }
        }

        private static void Prompt(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
        
        private static void AssertAcceptanceTestDirectoryExists(string acceptanceTestDirectory)
        {
            if (Directory.Exists(acceptanceTestDirectory)) return;

            Console.WriteLine("Acceptance Test Directory " + acceptanceTestDirectory + " does not exist.");
            Environment.Exit(1);
        }

        private static void ValidateCommandLineArguments(string[] args)
        {
            if (args.Length == 1) return;

            Console.WriteLine("Usage: ecliptic [directory containing settingsFile].  ");
            Environment.Exit(1);
        }
    }
}
