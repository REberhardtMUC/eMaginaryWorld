using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    TextMeshProUGUI message;
    private void Start()
    {
        message = this.GetComponent<TextMeshProUGUI>();
        message.enabled = false;
    }

    public void DisplayMessage(GameObject go)
    {
        message.text = "You picked up an item!!";
        message.enabled = true;
        Invoke("TurnOff", 2);
    }

    void TurnOff()
    {
        message.enabled=false;
    }
}
