using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using System.Drawing;

namespace Lunar.Utils.Grid_Class
{
    public class GridSummary
    {
        public static void PreencherSumario(SfDataGrid grid, string valorTotalColumnName)
        {
            // Definindo a linha de sumário
            GridTableSummaryRow tableSummaryRow = new GridTableSummaryRow
            {
                Name = "TableSummary",
                ShowSummaryInRow = true,
                TitleColumnCount = 1,
                Title = " Total: {ValorTotal}",
                Position = VerticalPosition.Bottom,
                CalculationUnit = Syncfusion.Data.SummaryCalculationUnit.AllRows
            };

            // Definindo a coluna de sumário
            GridSummaryColumn summaryColumn = new GridSummaryColumn
            {
                Name = "ValorTotal",
                SummaryType = SummaryType.DoubleAggregate,
                Format = "{Sum:c}",
                MappingName = valorTotalColumnName
            };

            // Limpando qualquer sumário existente e adicionando o novo
            grid.TableSummaryRows.Clear();
            tableSummaryRow.SummaryColumns.Add(summaryColumn);
            grid.TableSummaryRows.Add(tableSummaryRow);

            // Estilizando a linha de sumário
            grid.Style.TableSummaryRowStyle.Font = new GridFontInfo(new Font("Microsoft Sans Serif", 12f, FontStyle.Regular));
        }

        public static void PreencherSumario(SfDataGrid grid, string valorTotalColumnName1, string quantidadeColumnName2)
        {
            // Definindo a linha de sumário
            GridTableSummaryRow tableSummaryRow = new GridTableSummaryRow
            {
                Name = "TableSummary",
                ShowSummaryInRow = true,
                TitleColumnCount = 1,
                Title = "Total: {ValorTotal}  Quantidade: {Quantidade}",
                Position = VerticalPosition.Bottom,
                CalculationUnit = Syncfusion.Data.SummaryCalculationUnit.AllRows
            };

            // Definindo a primeira coluna de sumário
            GridSummaryColumn summaryColumn1 = new GridSummaryColumn
            {
                Name = "ValorTotal",
                SummaryType = SummaryType.DoubleAggregate,
                Format = "{Sum:c}",
                MappingName = valorTotalColumnName1
            };

            // Definindo a segunda coluna de sumário
            GridSummaryColumn summaryColumn2 = new GridSummaryColumn
            {
                Name = "Quantidade",
                SummaryType = SummaryType.DoubleAggregate,
                Format = "{Sum:N2}",
                MappingName = quantidadeColumnName2
            };

            // Limpando qualquer sumário existente e adicionando o novo
            grid.TableSummaryRows.Clear();
            tableSummaryRow.SummaryColumns.Add(summaryColumn1);
            tableSummaryRow.SummaryColumns.Add(summaryColumn2);
            grid.TableSummaryRows.Add(tableSummaryRow);

            // Estilizando a linha de sumário
            grid.Style.TableSummaryRowStyle.Font = new GridFontInfo(new Font("Microsoft Sans Serif", 12f, FontStyle.Regular));
        }

    }
}
