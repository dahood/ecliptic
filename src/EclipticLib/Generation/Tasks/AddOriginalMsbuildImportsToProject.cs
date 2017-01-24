using System.Collections.Generic;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    public class AddOriginalMsbuildImportsToProject : EclipticTask<IEnumerable<XElement>>
    {
        public AddOriginalMsbuildImportsToProject(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public void Go(IEnumerable<XElement> imports)
        {
            var project = ProjectElement;
            imports.ForEach(project.Add);
        }
    }
}