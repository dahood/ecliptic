using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    public class AddMissingSpreadsheetFilesToProject : EclipticTask
    {
        public AddMissingSpreadsheetFilesToProject(EclipticProperties properties, XDocument document)
            : base(properties, document)
        {
        }

        public override void Run()
        {
            var root = GetCompileRootItemGroup("None");
            var spreadSheetsInProjectFile = FindDescendentsThatMatch(root, "Include", value => value.EndsWith(EclipticFileFilter.SpreadsheetFilter));

            var toAdd = Directory.EnumerateFiles(Properties.SpreadsheetFolder, EclipticFileFilter.SpreadsheetFileSearchPattern, SearchOption.AllDirectories).ToList();

            toAdd.ForEach(item =>
            {
                var relativepath = item.GetRelativepath();
                if (!spreadSheetsInProjectFile.Any(s => string.Equals(s, relativepath)))
                {
                    // the project file doesn't contains the spreadsheet. add it.
                    Console.WriteLine("Adding {0} to project file.", relativepath);
                    AddSpreadsheetFile(relativepath, root);
                }
            });
        }

        private static void AddSpreadsheetFile(string fileToInclude, XContainer doc)
        {
            var xelem = new XElement("None");
            xelem.Add(new XAttribute("Include", fileToInclude));
            doc.Add(xelem);
        }
    }
}