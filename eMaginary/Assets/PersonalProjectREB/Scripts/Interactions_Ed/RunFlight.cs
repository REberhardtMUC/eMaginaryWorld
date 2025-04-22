using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class RunFlight : MonoBehaviour
{
    [SerializeField] GameObject txt_flight;

    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    [SerializeField] Camera mainCamera;

    [SerializeField] SphereCollider TriggerColliderEd;
    [SerializeField] GameObject character_Ed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt_flight.SetActive(true);
    }
    public void OnButtonClick()
    {
        player.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        player.transform.position = character_Ed.transform.position - new Vector3(1, 0, 3);
        player.transform.rotation = character_Ed.transform.rotation;
       // player.transform.rotation = Quaternion.Euler(0, 190, 0);

        ResetEdInteractions();
    }

    public void ResetEdInteractions()
    {
        TriggerColliderEd.enabled = true;
    }


}
