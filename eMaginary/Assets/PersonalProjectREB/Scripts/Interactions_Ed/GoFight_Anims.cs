using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoFight_Anims : MonoBehaviour
{

    [SerializeField] GameObject txt_fight;

    [SerializeField] GameObject player;
    [SerializeField] Animator animator_Lili;
    [SerializeField] Animator animator_Ed;

    public void OnTriggerEnter(Collider other)
    {         
        animator_Lili.SetTrigger("kickAss");
        //edd anim abspielen
        animator_Ed.SetTrigger("kickOutch");
        this.GetComponent<BoxCollider>().enabled = false;
    }

}
