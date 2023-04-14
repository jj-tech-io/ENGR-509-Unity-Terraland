using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class OsmData
{
    public List<NodeData> nodes;
    public List<WayData> ways;
    public List<RelationData> relations;
}

[System.Serializable]
public class NodeData
{
    public long id;
    public double lat;
    public double lon;
    public Dictionary<string, string> tags;
}

[System.Serializable]
public class WayData
{
    public long id;
    public List<long> nodes;
    public Dictionary<string, string> tags;
}

[System.Serializable]
public class RelationData
{
    public long id;
    public List<MemberData> members;
    public Dictionary<string, string> tags;
}

[System.Serializable]
public class MemberData
{
    public string type;
    public long ref_id;
    public string role;
}
// Create a list to store the cube objects

public class OsmLoader : MonoBehaviour
{
    public TextAsset osmJSONFile;
    [SerializeField]
    public List<GameObject> cubeList = new List<GameObject>();
    void Start()
    {
        OsmData osmData = JsonUtility.FromJson<OsmData>(osmJSONFile.text);

        // Print nodes, ways, and relations
        foreach (NodeData node in osmData.nodes)
        {
            Debug.Log("Node ID: " + node.id);
            string coords = "Lat: " + node.lat + ", Lon: " + node.lon;
            Debug.Log("Coordinates: " +coords);
            //unity coordinates
            Vector3 worldPos = TerrainUtils.LatLonToWorld(node.lat, node.lon);
            Debug.Log("World Position: " + worldPos);
            //place a red cube at the node
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = worldPos;
            cube.GetComponent<Renderer>().material.color = Color.red;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            cubeList.Add(cube);
        }

        foreach (WayData way in osmData.ways)
        {
            Debug.Log("Way ID: " + way.id);
        }

        foreach (RelationData relation in osmData.relations)
        {
            Debug.Log("Relation ID: " + relation.id);
        }

        // Create game objects based on the OSM data
    }
    
}