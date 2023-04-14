using UnityEngine;
using System.IO;

public class OsmDataManager : MonoBehaviour
{
    public static OsmDataManager Instance;

    public TextAsset osmJSONFile;
    public OsmData osmData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadOsmData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadOsmData()
    {
        osmData = JsonUtility.FromJson<OsmData>(osmJSONFile.text);
    }
}