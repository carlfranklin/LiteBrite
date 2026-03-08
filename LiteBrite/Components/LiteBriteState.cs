namespace LiteBrite.Components;

public class LiteBriteState
{
    public string SelectedColor { get; private set; } = "#ff2020";
    public SaveEntry? SelectedSquare { get; private set; }
    public bool IsStampMode => SelectedSquare != null;
    public List<SaveEntry> Squares { get; private set; } = [];
    public List<SaveEntry> AllSaves { get; private set; } = [];
    public List<ColorEntry> Colors { get; private set; } = DefaultColors();

    public event Action? OnChanged;

    public static List<ColorEntry> DefaultColors() =>
    [
        new("Red",    "#ff2020"),
        new("Orange", "#ff8c00"),
        new("Yellow", "#ffff00"),
        new("Green",  "#00ff44"),
        new("Blue",   "#00aaff"),
        new("Violet", "#cc00ff"),
    ];

    public void SelectColor(string color)
    {
        SelectedColor = color;
        SelectedSquare = null;
        OnChanged?.Invoke();
    }

    public void SelectSquare(SaveEntry square)
    {
        SelectedSquare = SelectedSquare?.Id == square.Id ? null : square;
        OnChanged?.Invoke();
    }

    public void UpdateSaves(List<SaveEntry> allSaves)
    {
        AllSaves = allSaves;
        Squares = allSaves.Where(s => s.Type == "Square").ToList();
        if (SelectedSquare != null)
            SelectedSquare = Squares.FirstOrDefault(s => s.Id == SelectedSquare.Id);
        OnChanged?.Invoke();
    }

    public void UpdateColors(List<ColorEntry> colors)
    {
        Colors = colors;
        if (!colors.Any(c => c.Hex == SelectedColor) && colors.Count > 0)
            SelectedColor = colors[0].Hex;
        OnChanged?.Invoke();
    }

    public bool IsColorUsed(string hex) =>
        AllSaves.Any(save => save.Cells.Any(c => c == hex));
}
