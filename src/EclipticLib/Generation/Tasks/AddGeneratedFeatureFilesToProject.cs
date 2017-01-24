using System.Collections.Generic;
using System.Xml.Linq;
using EclipticLib.Extensions;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.Tasks
{
    public class AddGeneratedFeatureFilesToProject : EclipticTask
    {
        public AddGeneratedFeatureFilesToProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public void Run(List<TranslatedItem> items)
        {
            var root = GetCompileRootItemGroup("None");
            items.ForEach(each =>
            {
                AddSpreadsheetFile(each, root);
                AddFeatureFile(each, root);
            });
        }

        private static void AddSpreadsheetFile(TranslatedItem item, XContainer doc)
        {
            var xelem = new XElement("None");
            xelem.Add(new XAttribute("Include", item.SpreadSheetpath.GetRelativepath()));
            doc.Add(xelem);
        }

        private static void AddFeatureFile(TranslatedItem item, XContainer doc)
        {
            var xelem = new XElement("None");
            xelem.Add(new XAttribute("Include", item.FeatureFilePath.GetRelativepath()));
            xelem.Add(new XElement("Generator", "SpecFlowSingleFileGenerator"));
            doc.Add(xelem);
        }
    }
}