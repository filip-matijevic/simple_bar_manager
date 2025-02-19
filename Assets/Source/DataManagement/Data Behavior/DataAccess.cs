using UnityEngine;

public class DataAccess : MonoBehaviour
{
    [SerializeField]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void LoadItems(){
        Debug.Log(PlayerPrefs.GetString("test", "- n/a -"));
    }
}
