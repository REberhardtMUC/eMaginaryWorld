using TMPro;
using UnityEngine;

public class SetHintsAndHelp : MonoBehaviour
{
    [SerializeField] GameObject txt_hintsAndHelp;
    [SerializeField] string txt_message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = txt_message;
        }
    }
    private void OnTriggerExit(Collider other)
    {
       // txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = "";
    }
}
