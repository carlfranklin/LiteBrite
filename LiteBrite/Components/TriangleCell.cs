namespace LiteBrite.Components;

public record TriangleCell(string? Top, string? Right, string? Bottom, string? Left)
{
    public static readonly TriangleCell Empty = new(null, null, null, null);

    public bool IsEmpty => Top == null && Right == null && Bottom == null && Left == null;

    public TriangleCell WithIndex(int index, string? color) => index switch
    {
        0 => this with { Top = color },
        1 => this with { Right = color },
        2 => this with { Bottom = color },
        3 => this with { Left = color },
        _ => this
    };

    public string? GetIndex(int index) => index switch
    {
        0 => Top,
        1 => Right,
        2 => Bottom,
        3 => Left,
        _ => null
    };

    // Serialized as "tri:#ff2020|#00ff44||#00aaff" (empty segment = null)
    public string Serialize() => $"tri:{Top ?? ""}|{Right ?? ""}|{Bottom ?? ""}|{Left ?? ""}";

    public static TriangleCell? TryDeserialize(string s)
    {
        if (!s.StartsWith("tri:")) return null;
        var parts = s[4..].Split('|');
        if (parts.Length != 4) return null;
        return new TriangleCell(
            NullIfEmpty(parts[0]),
            NullIfEmpty(parts[1]),
            NullIfEmpty(parts[2]),
            NullIfEmpty(parts[3])
        );
    }

    private static string? NullIfEmpty(string s) => string.IsNullOrEmpty(s) ? null : s;
}
