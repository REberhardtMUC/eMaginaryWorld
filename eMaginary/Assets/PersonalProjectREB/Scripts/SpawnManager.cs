using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;

    private void Awake()
    {
        Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
    }

}
