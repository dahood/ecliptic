using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EclipticLib.Generation.Tasks
{
    public class RemoveAndCacheUnsupportedMsBuildTargetsBeforeSpecflowGeneration : EclipticTask<IEnumerable<XElement>>
    {
        public RemoveAndCacheUnsupportedMsBuildTargetsBeforeSpecflowGeneration(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public override IEnumerable<XElement> Get()
        {
            var imports = (from p in Document
                           .Descendants(DefaultNamespace + "Project")
                           .Descendants(DefaultNamespace + "Import")
                           select p).ToList();

            var tasksWithBeforeTargets = (from p in Document
                                          .Descendants(DefaultNamespace + "Target")
                                          .Where(x => x.Attributes().Any(att => att.Name == "BeforeTargets"))
                                          select p).ToList();
            imports.AddRange(tasksWithBeforeTargets);                 
            
            imports.ForEach(x => x.Remove());
            return imports;
        }
    }
}