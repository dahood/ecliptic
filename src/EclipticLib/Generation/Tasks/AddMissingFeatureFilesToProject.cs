using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    public class AddMissingFeatureFilesToProject : EclipticTask
    {
        public AddMissingFeatureFilesToProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override void Run()
        {
            var root = GetCompileRootItemGroup("None");
            var featureFilesInProjectFile = FindDescendentsThatMatch(root, "Include", value => value.EndsWith(EclipticFileFilter.FeatureFileFilter));

            var toAdd = Directory.EnumerateFiles(Properties.FeatureFileFolder, EclipticFileFilter.FeatureFileSearchPattern,
                SearchOption.AllDirectories).ToList();

            toAdd.ForEach(item =>
            {
                var relativepath = item.GetRelativepath();
                if (!featureFilesInProjectFile.Any(s => string.Equals(s, relativepath)))
                {
                    // the project file doesn't contains the feature file. add it.
                    Console.WriteLine("Adding {0} to project file.", relativepath);
                    AddFeatureFile(relativepath, root);
                }
            });

        }

        private static void AddFeatureFile(string fileToInclude, XContainer doc)
        {
            var xelem = new XElement("None");
            xelem.Add(new XAttribute("Include", fileToInclude));
            xelem.Add(new XElement("Generator", "SpecFlowSingleFileGenerator"));
            doc.Add(xelem);
        }
    }
}