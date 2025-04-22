using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GoFight : MonoBehaviour
{

    [SerializeField] GameObject txt_fight;

    [SerializeField] GameObject player;
    [SerializeField] Animator animator_Lili;

    [SerializeField] GameObject character_Ed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt_fight.SetActive(true);
    }

    public void OnButtonClick()
    {
        player.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;

        player.transform.position = character_Ed.transform.position - new Vector3(0,0,1);
        player.transform.rotation = character_Ed.transform.rotation;

    }
    
}
