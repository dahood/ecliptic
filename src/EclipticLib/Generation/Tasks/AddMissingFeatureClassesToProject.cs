using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    /// <summary>
    /// Compares the files in the class files found in the directory to what is in the project file. If a class file is not in the project file, it's added.
    /// </summary>
    public class AddMissingFeatureClassesToProject : EclipticTask
    {
        public AddMissingFeatureClassesToProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override void Run()
        {
            var root = GetCompileRootItemGroup("Compile");
            var existingClassesInProject = FindDescendentsThatMatch(root, "Include", value => value.EndsWith(EclipticFileFilter.GeneratedClassFileFilter));

            var toAdd = Directory.EnumerateFiles(Properties.FeatureFileFolder, EclipticFileFilter.GeneratedClassFileSearchPattern,
                SearchOption.AllDirectories).ToList();

            toAdd.ForEach(x =>
            {
                var fileToInclude = x.GetRelativepath();
                if (!existingClassesInProject.Any(s => string.Equals(s, fileToInclude)))
                {
                    var associatedFeatureFile = fileToInclude.GetFeatureFileFor();
                    Console.WriteLine("Adding {0} to project file.", fileToInclude);
                    AddGeneratedFeatureClass(root, fileToInclude, associatedFeatureFile);
                    AddLastGeneratedAttributeTo(associatedFeatureFile, fileToInclude);
                }
            });
        }

        private static void AddGeneratedFeatureClass(XContainer element, string fileToInclude, string associatedFeatureFile)
        {
            var xelem = new XElement("Compile");
            xelem.Add(new XAttribute("Include", fileToInclude));
            xelem.Add(new XElement("AutoGen", true));
            xelem.Add(new XElement("DesignTime", true));
            xelem.Add(new XElement("DependentUpon", new FileInfo(associatedFeatureFile).Name));
            element.Add(xelem);
        }

        private void AddLastGeneratedAttributeTo(string associatedFeatureFile, string fileToInclude)
        {
        }
    }
}