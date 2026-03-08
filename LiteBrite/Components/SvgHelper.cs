using System.Text;

namespace LiteBrite.Components;

public static class SvgHelper
{
    public static string ToDataUrl(SaveEntry stamp)
    {
        var sb = new StringBuilder();
        sb.Append($"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {stamp.Cols} {stamp.Rows}'>");
        sb.Append($"<rect width='{stamp.Cols}' height='{stamp.Rows}' fill='#000'/>");
        for (int r = 0; r < stamp.Rows; r++)
            for (int c = 0; c < stamp.Cols; c++)
            {
                var color = stamp.Cells[r * stamp.Cols + c];
                if (!string.IsNullOrEmpty(color))
                    sb.Append($"<rect x='{c}' y='{r}' width='1' height='1' fill='{color}'/>");
            }
        sb.Append("</svg>");
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return $"data:image/svg+xml;base64,{Convert.ToBase64String(bytes)}";
    }
}
