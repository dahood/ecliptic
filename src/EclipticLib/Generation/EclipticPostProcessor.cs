using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using EclipticLib.Extensions;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.Tasks;

namespace EclipticLib.Generation
{
    public class EclipticPostProcessor
    {
        private readonly EclipticProperties properties;

        public EclipticPostProcessor(EclipticProperties properties)
        {
            this.properties = properties;
        }

        public virtual void Process(List<TranslatedItem> featureFilesGenerated)
        {
            GenerateFeatureClassesForChangedAndNewFeatures(featureFilesGenerated);
            AddNewFilesToProjectFile();
        }

        private void AddNewFilesToProjectFile()
        {
            var projectDocument = XDocument.Load(properties.AcceptanceTestProject);

            projectDocument.Changed += projectDocument_Changed;
            // Add new Excel, feature and class files to the project file.
            GetTask<AddMissingFeatureFilesToProject>(projectDocument).Run();
            GetTask<AddMissingSpreadsheetFilesToProject>(projectDocument).Run();
            GetTask<AddMissingFeatureClassesToProject>(projectDocument).Run();


            // Remove any References that we don't have a file for in the project file. Do we need this?

            // don't save the project file if nothing changes. this only causes an un-necessary and slow reload of the project file by Visual Studio.
            if (projectFileChanged)
                SaveProject(projectDocument);
        }

        private bool projectFileChanged;

        void projectDocument_Changed(object sender, XObjectChangeEventArgs e)
        {
            projectFileChanged = true;
        }

        private void GenerateFeatureClassesForChangedAndNewFeatures(List<TranslatedItem> featureFilesGenerated)
        {
            var projectDocument = XDocument.Load(properties.AcceptanceTestProject);
            var copyOfProjectDocument = new XDocument(projectDocument);

            // Remove everything from this temporary file except those that we are generating new feature.cs files
            GetTask<RemoveSpreadsheetsFromProject>(copyOfProjectDocument).Run();
            GetTask<RemoveFeatureFilesFromProject>(copyOfProjectDocument).Run();
            GetTask<RemoveFeatureClassesFromProject>(copyOfProjectDocument).Run();
            GetTask<RemoveAndCacheUnsupportedMsBuildTargetsBeforeSpecflowGeneration>(copyOfProjectDocument).Get();

            GetTask<AddGeneratedFeatureFilesToProject>(copyOfProjectDocument).Run(featureFilesGenerated);
            
            SaveTempProject(copyOfProjectDocument);

            var result = GetTask<RegenerateFeatureClasses>(copyOfProjectDocument).Get();
            AssertSpecflowGenerationWasSuccessful(result);

            DeleteTempProject();
        }

        private void DeleteTempProject()
        {
            File.Delete(properties.AcceptanceTestTempProject);
        }

        private static void AssertSpecflowGenerationWasSuccessful(ProcessResult result)
        {
            Console.WriteLine("".PadRight(50, '-'));
            Console.WriteLine("Ecliptic - Specflow Generator");
            Console.WriteLine(result.StandardOutput);

            if (result.ErrorOutput.IsEmpty()) return;
            
            Console.WriteLine("Error with Specflow.generateall");
            Console.WriteLine(result.ErrorOutput);
            Console.WriteLine("".PadRight(50, '-'));
            Console.WriteLine("Press <enter> to exit");
            Console.ReadLine();
            Environment.Exit(1);
        }

        private void SaveTempProject(XDocument projectDocument)
        {
            var output = projectDocument.ToString().Replace("xmlns=\"\"", string.Empty);
            File.WriteAllText(properties.AcceptanceTestTempProject, output);
        }

        private void SaveProject(XDocument projectDocument)
        {
            var output = projectDocument.ToString().Replace("xmlns=\"\"", string.Empty);
            File.WriteAllText(properties.AcceptanceTestProject, output);
        }

        protected virtual T GetTask<T>(XDocument projectDocument)  where T : EclipticTask
        {
            return (T)Activator.CreateInstance(typeof (T), properties, projectDocument);
        }
    }
}
