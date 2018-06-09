namespace MarkdownTable
{
    public enum Alignment
    {
        Left,
        Center,
        Right
    }

    public class Column
    {
        public Alignment Alignment { get; set; }

        public string Name { get; set; }
    }
}
