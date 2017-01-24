using System.Linq;
using System.Xml.Linq;

namespace EclipticLib.Extensions
{
    public static class XmlExtensions
    {
        public static string GetAttributeValue(this XNode node, string attributeName)
        {
            var elem = node as XElement;
            if (elem == null) return null;

            var attrib = elem.Attributes().FirstOrDefault(a => a.Name == attributeName);
            return attrib != null ? attrib.Value : null;
        }
    }
}
