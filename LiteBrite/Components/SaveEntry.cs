namespace LiteBrite.Components;

public record SaveEntry(
    string Id,
    string Name,
    string Type,
    int Rows,
    int Cols,
    string[] Cells,
    string SavedAt
);
