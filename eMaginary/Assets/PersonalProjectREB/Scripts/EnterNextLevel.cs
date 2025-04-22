using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCave : MonoBehaviour
{
    public int actualLevel;
    public void OnTriggerEnter()
    {
        LoadScene(actualLevel);//Level[x] = inde[x+1]
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
