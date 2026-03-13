using System.Text;

namespace LiteBrite.Components;

public static class SvgHelper
{
    public static string ToDataUrl(SaveEntry stamp)
    {
        var sb = new StringBuilder();
        sb.Append($"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {stamp.Cols} {stamp.Rows}'>");
        sb.Append($"<rect width='{stamp.Cols}' height='{stamp.Rows}' fill='#000'/>");

        bool isTriangle = stamp.Mode == "Triangles";

        for (int r = 0; r < stamp.Rows; r++)
            for (int c = 0; c < stamp.Cols; c++)
            {
                var cell = stamp.Cells[r * stamp.Cols + c];
                if (string.IsNullOrEmpty(cell)) continue;

                if (isTriangle)
                {
                    var tc = TriangleCell.TryDeserialize(cell);
                    if (tc == null) continue;
                    // cx/cy = cell top-left, mid = center point
                    double x = c, y = r, mx = c + 0.5, my = r + 0.5;
                    if (tc.Top    != null) sb.Append($"<polygon points='{x},{y} {x+1},{y} {mx},{my}' fill='{tc.Top}'/>");
                    if (tc.Right  != null) sb.Append($"<polygon points='{x+1},{y} {x+1},{y+1} {mx},{my}' fill='{tc.Right}'/>");
                    if (tc.Bottom != null) sb.Append($"<polygon points='{x+1},{y+1} {x},{y+1} {mx},{my}' fill='{tc.Bottom}'/>");
                    if (tc.Left   != null) sb.Append($"<polygon points='{x},{y+1} {x},{y} {mx},{my}' fill='{tc.Left}'/>");
                }
                else
                {
                    sb.Append($"<rect x='{c}' y='{r}' width='1' height='1' fill='{cell}'/>");
                }
            }

        sb.Append("</svg>");
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return $"data:image/svg+xml;base64,{Convert.ToBase64String(bytes)}";
    }
}
