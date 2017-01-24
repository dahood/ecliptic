using System.Collections.Generic;
using System.Text;

namespace EclipticLib.Generation.Domain
{
    public class Scenario
    {
        public bool IsBackground { get; set; }
        public bool IsSetup { get; set; }

        public Scenario()
        {
            Statements = new List<Statement>();
        }

        public string Title { private get; set; }
        public List<Statement> Statements { get; set; }
        public SetupFeature Setup { private get; set; }

        public void WriteOn(StringBuilder builder)
        {
            if (IsBackground)
            {
                builder.AppendLine("Background:");
            }
            else
            {
                if (!IsSetup)
                    builder.AppendLine("Scenario: " + Title);
                else
                    builder.AppendLine(Title);
            }

            if (Setup != null)
            {
                builder.AppendLine(Setup.ToString());
            }

            foreach (var statement in Statements)
            {
                builder.AppendLine(statement.ToString());
            }
        }
    }
}