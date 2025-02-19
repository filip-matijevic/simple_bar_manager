using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Labels labels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        labels = DataAccess.LoadLabels();
        labels.AddLabel(new LabelModel("test"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
