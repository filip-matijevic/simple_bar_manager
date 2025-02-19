using System.Collections.Generic;
using UnityEngine;

public class LabelViewModel : MonoBehaviour{
    private const string Key = "LABELS";
    public List<Label> Labels {get; private set;}

    void Awake(){
        LoadLabels();
    }

    private void LoadLabels(){
        Labels = new List<Label>();
        string labelsData = PlayerPrefs.GetString(Key, "");

        if (!string.IsNullOrEmpty(labelsData)){
            LabelArray labelArray = JsonUtility.FromJson<LabelArray>(labelsData);
            Labels = new List<Label>(labelArray.Labels);
        }

        Debug.Log("Loaded " + Labels.Count + " labels");
        foreach(Label label in Labels){
            Debug.Log(label.ToString());
        }
    }

    private void SaveToPlayerPrefs(){
        LabelArray labelArray = new LabelArray();
        labelArray.Labels = Labels;
        PlayerPrefs.SetString(Key, JsonUtility.ToJson(labelArray));
        PlayerPrefs.Save();
    }

    public void AddLabel(string name, Color color)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Labels.Add(new Label(name, color));
            SaveToPlayerPrefs();
        }
    }

    public void RemoveLabel(string name){
        Labels.RemoveAll(label => label.Name == name);
        SaveToPlayerPrefs();
    }
}


[System.Serializable]
public class Label{
    public string Name;
    public string ColorHex;
    public Label(string name, Color colorHex){
        this.Name = name;
        ColorHex = ColorToHex(colorHex);
    }

    private string ColorToHex(Color color){
        Color32 color32= color;
        return $"{color32.r:X2}{color32.g:X2}{color32.b:X2}{color32.a:X2}";
    }

    public Color GetColor(){
        Debug.Log(ColorHex);

        if (ColorHex.Length != 8){
            return Color.magenta;
        }        
        byte r = byte.Parse(ColorHex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(ColorHex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(ColorHex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(ColorHex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

        Debug.Log(r + " " + g + " " + b + " " + a);

        return new Color32(r, g, b, a);
    }

    public string ToDebugString(){
        return Name + " " + ColorHex;
    }
}

[System.Serializable]
public class LabelArray{
    public List<Label> Labels;
}




