using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelListView : MonoBehaviour
{
    [SerializeField]
    private LabelView labelViewPrefab;

    [Header("Props")]
    [SerializeField]
    private Transform labelContainer;

    private LabelListViewModel viewModel;

    [SerializeField]
    private Button dropTableButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        viewModel = new LabelListViewModel();
        viewModel.OnLabelRefresh += OnLabelViewModelChange;
        dropTableButton.onClick.AddListener(viewModel.DropLabelTable);
        RebuildLabelList();
    }

    private void OnLabelViewModelChange()
    {
        Debug.Log("Labels got changed!");
        RebuildLabelList();
    }

    void RebuildLabelList(){
        foreach(Transform child in labelContainer){
            Destroy(child.gameObject);
        }

        foreach(LabelViewModel loadedLabel in viewModel.Labels){
            LabelView view = Instantiate(labelViewPrefab, labelContainer);
            view.Init(loadedLabel.Label, loadedLabel.Color);
        }
    }
}
