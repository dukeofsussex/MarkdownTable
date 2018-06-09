# MarkdownTable
Generate markdown tables in CSharp. Supports column alignment.

## "Short" Example

```csharp
using MarkdownTable;

Table table = new Table()
{
    Headers = new List<Column>()
    {
        new Column()
        {
            Name = "Column with default alignment (left)"
        },
        new Column()
        {
            Alignment = Alignment.Right,
            Name = "Column Right"
        },
        new Column()
        {
            Alignment = Alignment.Center,
            Name = "Column Center"
        },
        new Column()
        {
            Alignment = Alignment.Left,
            Name = "Column Left"
        }
    },
    Rows = new List<Row>()
    {
        new Row()
        {
            Cells = new List<string>()
            {
                "First cell",
                "Longer than second",
                "Short",
                "Cell #4"
            }
        },
        new Row()
        {
            Cells = new List<string>()
            {
                "Still shorter than header",
                "Second",
                "Longer",
                "Way too wide cell in comparison to all others"
            }
        },
        ...
    }
};

string generatedTable = MarkdownTable.GenerateTable(table);
```

The generated table string then looks like this:
```
| Column with default alignment (left) |       Column Right | Column Center | Column Left                                   |
|--------------------------------------|-------------------:|:-------------:|-----------------------------------------------|
| First cell                           | Longer than second |    Short      | Cell #4                                       |
| Still shorter than header            |             Second |    Longer     | Way too wide cell in comparison to all others |
```

and in markdown:

| Column with default alignment (left) |       Column Right | Column Center | Column Left                                   |
|--------------------------------------|-------------------:|:-------------:|-----------------------------------------------|
| First cell                           | Longer than second |    Short      | Cell #4                                       |
| Still shorter than header            |             Second |    Longer     | Way too wide cell in comparison to all others |