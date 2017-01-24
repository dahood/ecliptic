using System.Data;

namespace EclipticLib.Excel
{
    public interface IExcelReader
    {
        DataSet ReadToDataSet(string excelFile);
    }
}
