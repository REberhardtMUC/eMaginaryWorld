using UnityEngine;

public class SetParam_Trigger_switch : MonoBehaviour
{
    [SerializeField] GameObject affected_player;
    private string playerTag;
    private Animator anim;
    
    public string TriggerParameterEnter;
    public string TriggerParameterExit;

    private void Start()
    {
        anim = affected_player.GetComponent<Animator>();
        playerTag = affected_player.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag(playerTag) && TriggerParameterEnter != "") {
           anim.SetTrigger(TriggerParameterEnter);
            //Debug.Log("Trigger Active: " + TriggerParameter);
        }
    }

   private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && TriggerParameterExit != "")
        {
            anim.SetTrigger(TriggerParameterExit);
            //Debug.Log("Trigger Active: " + TriggerParameter);
        }
    }
}
