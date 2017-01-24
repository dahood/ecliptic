using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace EclipticLib.Excel
{
    public class EpplusExcelReader : IExcelReader
    {
        public DataSet ReadToDataSet(string excelFile)
        {
            var ds = new DataSet();

            using (var pck = new ExcelPackage())
            {
                pck.Load(File.OpenRead(excelFile));
                foreach (var table in pck.Workbook.Worksheets.Select(CreateTable).Where(table => table != null))
                {
                    ds.Tables.Add(table);
                }
            }
            return ds;
        }

        private static DataTable CreateTable(ExcelWorksheet ws)
        {
            var tbl = new DataTable(ws.Name);

            if (ReferenceEquals(null, ws.Dimension)) return null;

            ws.Calculate();

            var columnCount = GetColumnCount(ws);

            for (var i = 0; i < columnCount; i++)
            {
                tbl.Columns.Add();
            }

            for (var rowNum = 1; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, columnCount];
                var row = tbl.NewRow();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
                tbl.Rows.Add(row);
            }
            return tbl;
        }

        private static int GetColumnCount(ExcelWorksheet ws)
        {
            var maxColumn = 0;
            for (var rowNum = 1; rowNum < ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var columns = wsRow.Count();
                if (columns > maxColumn)
                    maxColumn = columns;
            }
            return maxColumn;
        }
    }
}