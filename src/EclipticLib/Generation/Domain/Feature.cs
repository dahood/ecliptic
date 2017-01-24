using System.Collections.Generic;
using System.IO;
using System.Text;


namespace EclipticLib.Generation.Domain
{
    public class Feature
    {
        public Feature()
        {
            Scenarios = new List<Scenario>();
            Tags = new List<string>();
        }

        public virtual string Title { get; set; }
        public string Description { get; set; }
        public List<Scenario> Scenarios { get; set; }
        public List<string> Tags { get; set; }
        public Scenario Background { get; set; }

        public virtual string SaveToFile(string directoryName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var saveAs = Path.Combine(directoryName, Title + ".xlsx.feature");
            File.WriteAllText(saveAs, ToString());
            return saveAs;
        }


        public override string ToString()
        {
            var builder = new StringBuilder();
            
            foreach (var tag in Tags)
            {
                builder.Append("@");
                builder.Append(tag);
                builder.Append(" ");
            }
            builder.AppendLine();
            builder.Append("Feature: ");
            builder.AppendLine(Title);
            builder.AppendLine(Description);
            builder.AppendLine();
            
            //Background
            if (Background != null)
            {
                Background.WriteOn(builder);
            }

            foreach (var scenario in Scenarios)
            {
                scenario.WriteOn(builder);
            }
            return builder.ToString();
        }
    }
}