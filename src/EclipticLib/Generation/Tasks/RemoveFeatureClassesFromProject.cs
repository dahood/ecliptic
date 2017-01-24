using System.Xml.Linq;

namespace EclipticLib.Generation.Tasks
{
    public class RemoveFeatureClassesFromProject : EclipticTask
    {
        public RemoveFeatureClassesFromProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override void Run()
        {
            var root = GetCompileRootItemGroup("Compile");
            RemoveDecendentsThatMatch(root, "Include", value => value.EndsWith(EclipticFileFilter.GeneratedClassFileFilter));
        }
    }
}
