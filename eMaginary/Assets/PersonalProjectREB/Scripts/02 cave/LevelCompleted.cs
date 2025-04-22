using UnityEngine;


public class LevelCompleted : MonoBehaviour
{
    private GameObject m_Ken;
    private GameObject m_SheKen;
    [SerializeField] GameObject m_PowerUp_FollowMe;
    [SerializeField] GameObject m_img_cave;

    private void Start()
    {
        m_Ken = GameObject.FindWithTag("Ken");
        m_SheKen = GameObject.FindWithTag("She_Ken");
    }
    private void OnTriggerEnter(Collider other)
    {
        m_Ken.SetActive(false);
        m_SheKen.SetActive(false);
        m_PowerUp_FollowMe.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Draussen");
        m_img_cave.SetActive(true);
    }
}