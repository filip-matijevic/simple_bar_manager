using UnityEngine;

public class LabelListView : MonoBehaviour
{
    [SerializeField]
    private LabelViewModel viewModel;
    [SerializeField]
    private LabelView labelViewPrefab;

    [Header("Props")]
    [SerializeField]
    private Transform labelContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayLabels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayLabels(){
        foreach(Transform child in labelContainer){
            Destroy(child.gameObject);
        }

        foreach(Label label in viewModel.Labels){
            LabelView labelInstance = Instantiate(labelViewPrefab, labelContainer);
            labelInstance.Init(label.Name, label.GetColor());
        }
    }
}
