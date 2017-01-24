using System.Collections.Generic;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Domain.Gherkin
{
    public class GherkinRow
    {
        private readonly GherkinTable table;

        public GherkinRow(GherkinTable table)
        {
            this.table = table;
            Cells = new List<string>();
        }

        public List<string> Cells { get; set; }

        public void AddContent(string columnContents, int column)
        {
            Cells.Add(table.GetColumn(column).AddContent(columnContents));
        }

        public string CellValue(int column)
        {
            return Cells[column].PadWithMargin(table.GetColumn(column).Width);
        }
    }
}