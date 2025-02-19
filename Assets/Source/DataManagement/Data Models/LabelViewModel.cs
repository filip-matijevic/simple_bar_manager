using System.Collections.Generic;
using UnityEngine;

public class LabelViewModel : MonoBehaviour{
    private const string Key = "LABELS";
    public List<Label> Labels {get; private set;}

    void Awake(){
        LoadLabels();
        AddLabel("a label", Random.ColorHSV());
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
        return ColorUtility.ToHtmlStringRGBA(color);
    }

    public Color GetColor(){
        if (ColorUtility.TryParseHtmlString(ColorHex, out Color color)){
            return color;
        }
        return Color.magenta;
    }

    public string ToDebugString(){
        return Name + " " + ColorHex;
    }
}

[System.Serializable]
public class LabelArray{
    public List<Label> Labels;
}




