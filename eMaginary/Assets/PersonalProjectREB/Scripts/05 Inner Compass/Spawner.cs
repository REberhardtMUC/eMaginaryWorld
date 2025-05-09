using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject eggPrefab;
    public GameObject boxPrefab;
    public Terrain terrain;
    TerrainData terrainData;


    void Start()
    {
        terrainData = terrain.terrainData;
        //InvokeRepeating("CreateBox", 1, 10);
        InvokeRepeating("CreateBox1", 1, 1f);
    }

    void CreateBox()
    {
        //int x = (int)Random.Range(0, terrainData.size.x);
        //int z = (int)Random.Range(0, terrainData.size.z);
        //Vector3 pos = new Vector3(x, 0, z);
        //pos.y = terrain.SampleHeight(pos) + 10;
        //GameObject egg = Instantiate(eggPrefab , pos, Quaternion.identity);
        
    }
    void CreateBox1()
    {
        int x = (int)Random.Range(0, terrainData.size.x);
        int z = (int)Random.Range(0, terrainData.size.z);
        Vector3 pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos) + 10;
        GameObject box = Instantiate(boxPrefab, pos, Quaternion.identity);
    }
}
