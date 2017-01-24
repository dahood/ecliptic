using System.Collections.Generic;

namespace EclipticLib.Generation.Domain.Gherkin
{
    public class GherkinTable
    {
        public GherkinTable()
        {
            Columns = new List<GherkinColumn>();
            Rows = new List<GherkinRow>();
        }
        public List<GherkinColumn> Columns { get; set; }
        public List<GherkinRow> Rows { get; set; }

        public void AddColumn(string columnName)
        {
            Columns.Add(new GherkinColumn(columnName));
        }

        public GherkinRow NewRow()
        {
            var gherkinRow = new GherkinRow(this);
            Rows.Add(gherkinRow);
            return gherkinRow;
        }

        public GherkinColumn GetColumn(int column)
        {
            return Columns[column];
        }
    }
}