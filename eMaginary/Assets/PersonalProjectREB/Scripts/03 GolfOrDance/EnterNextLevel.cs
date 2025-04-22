using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextLevel : MonoBehaviour
{
    public int actualLevel;

    public void OnTriggerEnter()
    {
        LoadNextLevel(actualLevel);
    }

    private void LoadNextLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
