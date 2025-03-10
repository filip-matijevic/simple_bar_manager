using UnityEngine;

public class LabelViewModel
{
    public string Label { get; set; }
    public Color Color { get; set; }

    public LabelViewModel(string label, Color color)
    {
        Label = label;
        Color = color;
    }

    public LabelViewModel(Label model){
        Label = model.Name;
        Color = HexToColor(model.HexColor);
    }

    public void Store()
    {
        Debug.Log(Color.ToString());
        Label labelModel = new Label(-1, Label, ColorToHex(Color));
        Debug.Log(labelModel.ToString());
        SQLiteManager.Labels.Insert(labelModel);
    }

    private string ColorToHex(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);
        Debug.Log(r + "," + g + "," + b);
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    private Color HexToColor(string hex)
    {
        if (hex.StartsWith('#')){
            hex.TrimStart('#');
        }
        Debug.Log(hex);
        byte r = byte.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color(r / 255f, g / 255f, b / 255f, 1f);
    }
}


