using UnityEngine;

public class SetParam_Trigger : MonoBehaviour
{
    public Animator anim;
    public string TriggerParameterEnter;
    public string TriggerParameterExit;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Player") && TriggerParameterEnter != "") {
           anim.SetTrigger(TriggerParameterEnter);
            //Debug.Log("Trigger Active: " + TriggerParameter);
        }
    }

   private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && TriggerParameterExit != "")
        {
            anim.SetTrigger(TriggerParameterExit);
            //Debug.Log("Trigger Active: " + TriggerParameter);
        }
    }
}
