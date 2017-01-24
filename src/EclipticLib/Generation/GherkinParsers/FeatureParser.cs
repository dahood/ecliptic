using System.Data;
using EclipticLib.Extensions;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.Domain.Gherkin;

namespace EclipticLib.Generation.GherkinParsers
{
    public abstract class FeatureParser : IFeatureParser
    {
        protected readonly Feature Feature;
        protected readonly DataTable FixtureTable;
        private readonly SetupFeatureParser setupFeatureParser;
        protected readonly EclipticProperties eclipticProperties;

        protected FeatureParser(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties)
            : this(feature, fixtureTable, new SetupFeatureParser(), eclipticProperties)
        {
        }

        private FeatureParser(Feature feature, DataTable fixtureTable, SetupFeatureParser setupFeatureParser, EclipticProperties eclipticProperties)
        {
            Feature = feature;
            FixtureTable = fixtureTable;
            this.setupFeatureParser = setupFeatureParser;
            this.eclipticProperties = eclipticProperties;
        }

        public abstract void Parse();

        public virtual Scenario ReadScenarioFromWorksheet(DataTable table)
        {
            var scenario = new Scenario();
            scenario.Title = table.TableName;

            //replace with table to table mapping (transform from ExcelDataTable to GherkinFeatureDataTable
            for (var rowNumber = 0; rowNumber < table.Rows.Count; rowNumber++)
            {
                var rowContentsOfFirstCell = table.DataRowValue(rowNumber, 0, eclipticProperties).Trim();

                if (!rowContentsOfFirstCell.IsGherkinKeywordOrEclipticKeyword()) continue;
                if (rowContentsOfFirstCell.IsEclipticKeyword())
                {
                    HandleEclipticKeyword(table, rowContentsOfFirstCell, rowNumber, scenario);
                }
                else    // is Gherkin keyword
                {
                    scenario.Statements.Add(HandleGherkinKeyword(table, rowContentsOfFirstCell, rowNumber));
                }
            }
            return scenario;
        }

        private void HandleEclipticKeyword(DataTable table, string rowContentsOfFirstCell, int rowNumber, Scenario scenario)
        {
            switch (rowContentsOfFirstCell.EnumParse<EclipticKeyword>())
            {
                case EclipticKeyword.Setup:
                {
                    var setupFilePath = (string)table.Rows[rowNumber][1];
                    setupFilePath.AssertFilePathExists("Setup");
                    var setupFeature = setupFeatureParser.Parse(setupFilePath);
                    scenario.Setup = new SetupFeature(setupFeature);
//                    Feature.Setup = new SetupFeature(setupFeature);
                }
                    break;
                case EclipticKeyword.Scenario:
                    scenario.Title = table.DataRowValue(rowNumber, 1, eclipticProperties);
                    break;
            }
        }

        private Statement HandleGherkinKeyword(DataTable table, string rowContentsOfFirstCell, int currentRow)
        {
            var statement = new Statement();
            
            statement.Command = rowContentsOfFirstCell.EnumParse<GherkinKeyword>();
            statement.Description = table.DataRowValue(currentRow, 1);
            
            if (table.Rows[currentRow].ItemArray.Length <= 2) return statement;

            var gherkinTable = new GherkinTable();
            
            ReadTableHeader(table, currentRow, gherkinTable);

            CollectRowData(table, currentRow, gherkinTable);
            statement.Table = gherkinTable;
            return statement;
        }

        private void CollectRowData(DataTable table, int currentRow, GherkinTable gherkinTable)
        {
            for (var rowOffset = 1;
                table.Rows.Count > (currentRow + rowOffset) &&
                !table.DataRowValue(currentRow + rowOffset, 0).IsGherkinKeywordOrEclipticKeyword() &&
                NotEmptyRow(table, currentRow, rowOffset);
                rowOffset++)
            {
                if (IsCommentedOutRow(table, currentRow, rowOffset))
                {
                    continue;
                }

                var row = gherkinTable.NewRow();
                
                for (var currentColumn = 2; currentColumn < gherkinTable.Columns.Count+2; currentColumn++)
                {
                    var columnContents = table.DataRowValue(currentRow + rowOffset, currentColumn, eclipticProperties);
                    row.AddContent(columnContents, currentColumn-2);
                }
            }
        }

        private static bool IsCommentedOutRow(DataTable table, int currentRow, int rowOffset)
        {
            var dataRowValue = table.DataRowValue(currentRow+rowOffset, 0);
            return dataRowValue.IsNotEmpty() && 
                (dataRowValue.Trim().StartsWith("//") || dataRowValue.Trim().StartsWith("--"));
        }

        private static bool NotEmptyRow(DataTable table, int currentRow, int rowOffset)
        {
            var numberOfColumns = table.Columns.Count;
            for (var currentColumn = 2; currentColumn < numberOfColumns; currentColumn++)
            {
                if (table.DataRowValue(currentRow + rowOffset, currentColumn).IsNotEmpty())
                {
                    return true;
                }
            }
            return false;
        }

        private static void ReadTableHeader(DataTable table, int currentRow, GherkinTable gherkinTable)
        {
            for (var currentColumn = 2; currentColumn < table.Columns.Count; currentColumn++)
            {
                var columnName = table.DataRowValue(currentRow, currentColumn);
                if (columnName.IsEmpty()) return;
                gherkinTable.AddColumn(columnName);
            }
        }
    }
}
