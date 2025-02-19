using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
[System.Serializable]
public class LabelModel
{
    public string name;
    public LabelModel(string name){
        this.name = name;
    }
}

[System.Serializable]
public class LabelCollection{
    public string name;
    public LabelModel[] labels;

    public LabelCollection(){
        labels = new LabelModel[0];
    }
}

public class Labels{
    public List<LabelModel> labels;
    private LabelCollection labelCollection;

    public Labels(LabelCollection labelCollection){
        this.labelCollection = labelCollection;
        labels = new List<LabelModel>(labelCollection.labels);
    }

    public void AddLabel(LabelModel label){
        labels.Add(label);
        labelCollection.labels = labels.ToArray();
        DataAccess.StoreLabelCollection(labelCollection);
    }
}



