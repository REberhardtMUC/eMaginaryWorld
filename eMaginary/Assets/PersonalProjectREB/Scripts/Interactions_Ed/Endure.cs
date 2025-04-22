using UnityEngine;
using UnityEngine.EventSystems;

public class Endure : MonoBehaviour
{
    [SerializeField] GameObject txt_pick;
    [SerializeField] GameObject player;

    [SerializeField] GameObject character_Ed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt_pick.SetActive(true);
    }
    public void OnButtonClick()
    {
        player.SetActive(true);
        player.transform.position = character_Ed.transform.position - new Vector3(0, 0, 2);
        player.transform.rotation = character_Ed.transform.rotation;

        player.GetComponent<PlayerController>().enabled = false;
    }


}
