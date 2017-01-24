using System.Xml.Linq;

namespace EclipticLib.Generation.Tasks
{
    public class RemoveSpreadsheetsFromProject : EclipticTask
    {
        public RemoveSpreadsheetsFromProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override void Run()
        {
            var root = GetCompileRootItemGroup("None");
            RemoveDecendentsThatMatch(root, "Include", value => value.EndsWith(EclipticFileFilter.SpreadsheetFilter));
        }
    }
}