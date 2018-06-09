namespace MarkdownTable
{
    using System.Collections.Generic;

    public class Table
    {
        public List<Column> Headers { get; set; }

        public List<Row> Rows { get; set; }
    }
}
