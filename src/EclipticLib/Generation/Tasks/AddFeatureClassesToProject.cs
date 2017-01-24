using System.IO;
using System.Linq;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    public class AddFeatureClassesToProject : EclipticTask
    {
        public AddFeatureClassesToProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override void Run()
        {
            var toAdd = Directory.EnumerateFiles(Properties.FeatureFileFolder, EclipticFileFilter.GeneratedClassFileSearchPattern,
                                                 SearchOption.AllDirectories).ToList();
            var root = GetCompileRootItemGroup("Compile");
            toAdd.ForEach(x =>
            {
                var fileToInclude = x.GetRelativepath();
                var associatedFeatureFile = fileToInclude.GetFeatureFileFor();
                AddGeneratedFeatureClass(root, fileToInclude, associatedFeatureFile);
                AddLastGeneratedAttributeTo(associatedFeatureFile, fileToInclude);
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