using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabelView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI labelName;
    [SerializeField]
    private Image backdrop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init(string name, Color color){
        this.labelName.text = name;
        this.backdrop.color = color;
    }
}
