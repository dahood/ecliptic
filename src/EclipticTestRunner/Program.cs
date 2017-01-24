using System;
using EclipticLib.Test;

namespace EclipticTestRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            var arguments = new TestRunnerArguments(args);

            var testRunner = new TestRunnerFactory().Get(arguments);
            if (testRunner == null)
                Environment.Exit(1);
            try
            {
                testRunner.Run();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message + " Stack Trace: " + e.StackTrace);
                Console.WriteLine("press <enter> to continue");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }
    }
}
