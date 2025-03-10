using System;
using System.Collections.Generic;
using UnityEngine;

public class LabelListViewModel
{
    public List<LabelViewModel> Labels = new List<LabelViewModel>();
    public Action OnLabelRefresh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LabelListViewModel(){

        LabelTable.OnInsert += OnLabelInsert;

    }

    private void OnLabelInsert()
    {
        Debug.Log("Something got inserted!");
        LoadLabels();
    }

    private void LoadLabels(){
        List<Label> dbLabels = SQLiteManager.Labels.GetLabels();
        Debug.Log("Number of labels : " + dbLabels.Count);
        Labels.Clear();
        foreach (Label dbLabel in dbLabels){
            Debug.Log(dbLabel.ToString());
            Labels.Add(new LabelViewModel(dbLabel));
        }
        OnLabelRefresh?.Invoke();
    }

    public void DropLabelTable(){
        SQLiteManager.Labels.DropLabelTable();
        OnLabelRefresh?.Invoke();

    }
}
