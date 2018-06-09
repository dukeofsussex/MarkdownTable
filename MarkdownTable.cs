namespace MarkdownTable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MarkdownTable
    {
        public static string GenerateTable(Table table)
        {
            // Completely empty table
            if (table == null || (table.Headers == null && table.Rows == null))
            {
                throw new ArgumentNullException("Empty table");
            }

            bool hasHeaders = table.Headers != null && table.Headers.Count > 0;
            bool hasRows = table.Rows != null && table.Rows.Count > 0;
            StringBuilder tableBuilder = new StringBuilder();
            List<int> columnWidths;
            List<Alignment> alignments;

            // Setup the initial lists with the header values
            if (hasHeaders)
            {
                columnWidths = table.Headers.Select(h => h.Name.Length).ToList();
                alignments = table.Headers.Select(h => h.Alignment).ToList();
            }

            // No header present, fill with dummy data for now
            else
            {
                columnWidths = Enumerable.Repeat(0, table.Rows[0].Cells.Count).ToList();
                alignments = Enumerable.Repeat(Alignment.Center, columnWidths.Count).ToList();
            }

            // Update the column widths to make sure we are using the widest one
            if (hasRows)
            {
                foreach (Row row in table.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (columnWidths[i] < row.Cells[i].Length)
                        {
                            columnWidths[i] = row.Cells[i].Length;
                        }
                    }
                }
            }

            // Build table headers and the seperator
            if (hasHeaders)
            {
                BuildRow(table.Headers.Select(h => h.Name).ToList(), columnWidths, alignments, true, ref tableBuilder);
                BuildSeparator(columnWidths, alignments, hasRows, ref tableBuilder);
            }

            // Build table rows
            if (hasRows)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    BuildRow(table.Rows[i].Cells, columnWidths, alignments, i < (table.Rows.Count - 1), ref tableBuilder);
                }
            }

            return tableBuilder.ToString();
        }

        private static void BuildRow(List<string> cellValues, List<int> columnWidths, List<Alignment> alignments, bool addNewLine, ref StringBuilder tableBuilder)
        {
            // Iterate over all table cells
            for (int i = 0; i < cellValues.Count; i++)
            {
                tableBuilder.Append("| ");

                // Pad the cell depending on the alignment
                switch (alignments[i])
                {
                    case Alignment.Left:
                        tableBuilder.Append(cellValues[i].PadRight(columnWidths[i]));
                        break;
                    case Alignment.Right:
                        tableBuilder.Append(cellValues[i].PadLeft(columnWidths[i]));
                        break;
                    case Alignment.Center:
                    default:
                        string temp = cellValues[i].PadLeft((columnWidths[i] / 2) + (cellValues[i].Length / 2));
                        temp = temp.PadRight(columnWidths[i]);
                        tableBuilder.Append(temp);
                        break;
                }

                // Add extra padding, to ensure the longest string is still padded on each side
                tableBuilder.Append(' ');
            }

            tableBuilder.Append('|');

            if (addNewLine)
            {
                tableBuilder.AppendLine();
            }
        }

        private static void BuildSeparator(List<int> columnWidths, List<Alignment> alignments, bool addNewLine, ref StringBuilder builtTable)
        {
            for (int i = 0; i < columnWidths.Count; i++)
            {
                builtTable.Append("|");

                // The reason for adding onto the columnWidth is due to the cell receiving an extra two
                // spaces padding, one on each side
                switch (alignments[i])
                {
                    case Alignment.Left:
                        builtTable.Append(new string('-', columnWidths[i] + 2));
                        break;
                    case Alignment.Right:
                        builtTable.Append(new string('-', columnWidths[i] + 1));
                        builtTable.Append(':');
                        break;
                    case Alignment.Center:
                    default:
                        builtTable.Append(':');
                        builtTable.Append(new string('-', columnWidths[i]));
                        builtTable.Append(':');
                        break;
                }
            }

            builtTable.Append('|');

            if (addNewLine)
            {
                builtTable.AppendLine();
            }
        }
    }
}
