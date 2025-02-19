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

    public static Labels LoadLabels(){
        LabelCollection collection = JsonUtility.FromJson<LabelCollection>(PlayerPrefs.GetString("LABEL_COLLECTION", ""));
        if (collection == null){
            collection = new LabelCollection();
        }
        Debug.Log("Loaded the collection with " + collection.labels.Length + " entries");
        return new Labels(collection);
    }

    public static void StoreLabelCollection(LabelCollection collection){
        PlayerPrefs.SetString("LABEL_COLLECTION", JsonUtility.ToJson(collection));
    }
}
