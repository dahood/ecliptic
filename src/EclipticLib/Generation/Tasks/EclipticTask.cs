using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Tasks
{
    public abstract class EclipticTask
    {
        protected readonly XNamespace DefaultNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        protected readonly string MsbuildNamespace = "schemas.microsoft.com/developer/msbuild/2003";

        protected XDocument Document { get; set; }
        protected EclipticProperties Properties { get; private set; }
     
        protected EclipticTask(EclipticProperties properties, XDocument document)
        {
            Document = document;
            Properties = properties;
        }

        public virtual XElement GetCompileRootItemGroup(string itemGroupCategory)
        {
            var includes =
                (from p in Document
                    .Descendants(DefaultNamespace + "Project")
                    .Descendants(DefaultNamespace + "ItemGroup")
                    .Descendants(DefaultNamespace + itemGroupCategory)
                 select p).FirstOrDefault();
            if (includes == null)
            {
                Console.WriteLine("Could not find an XmlElement in the XmlPath Project/ItemGroup/{0}. Add one to the project file.", itemGroupCategory);
            }
            return includes.Parent;
        }

        public virtual XElement ProjectElement
        {
            get
            {
                return (from p
                    in Document.Descendants(DefaultNamespace + "Project")
                    select p).First();
            }
        }

        protected List<string> FindDescendentsThatMatch(XElement root, string include, Func<string, bool> predicate)
        {
            var result = new List<string>();

            root.Descendants().ToList().ForEach(each =>
            {
                var fileName = each.GetAttributeValue(include);

                if (!String.IsNullOrWhiteSpace(fileName) && predicate(fileName))
                {
                    result.Add(fileName);
                }
            });

            return result;
        }

        protected void RemoveDecendentsThatMatch(XElement root, string include, Func<string, bool> predicate)
        {
            root.Descendants().ToList().ForEach(each =>
            {
                var fileName = each.GetAttributeValue(include);
                if (fileName == null) return;
                if (predicate(fileName)) each.Remove();
            });
        }

        //most implement one or the other
        public virtual void Run()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class EclipticTask<TArg> : EclipticTask
    {
        protected EclipticTask(EclipticProperties properties, XDocument document) : base(properties, document)
        {
        }

        public virtual TArg Get()
        {
            throw new NotImplementedException();
        }

        public virtual void Run(TArg arg)
        {
            throw new NotImplementedException();
        }
    }
}
