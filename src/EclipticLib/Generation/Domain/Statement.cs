using System.Text;
using EclipticLib.Generation.Domain.Gherkin;

namespace EclipticLib.Generation.Domain
{
    public class Statement
    {
        public GherkinKeyword Command { get; set; }
        public string Description { get; set; }

        public GherkinTable Table { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Command);
            sb.Append(" ");
            sb.Append(Description);
            if (Table == null ||Table.Columns.Count <= 0)
            {
                sb.AppendLine(" ");
                return sb.ToString();
            }

            sb.AppendLine("");
            sb.Append("|");
            foreach(var column in Table.Columns)
            {
                sb.Append(column);
                sb.Append("|");
            }
            sb.AppendLine("");

            foreach(var row in Table.Rows)
            {
                sb.Append("|");
                for (var i = 0; i < row.Cells.Count; i++)
                {
                    sb.Append(row.CellValue(i));
                    sb.Append("|");
                }
                sb.AppendLine("");
            }
            return sb.ToString();
        }
    }
}