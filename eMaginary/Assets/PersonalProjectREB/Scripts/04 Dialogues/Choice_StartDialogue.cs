using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Dialogue
{
    public class Choice_StartDialogue : MonoBehaviour
    {
        [SerializeField] GameObject pnl_Dialogue;
        [SerializeField] GameObject angerOMeter;
        [SerializeField] GameObject cam_SheKen;
        Component cmp_playerConversant;
           
        private void OnTriggerEnter(Collider other)
        {
            if (this.gameObject.tag == "She_Ken")
            {
                if (other.gameObject.CompareTag("Ken"))
                {
                    //Debug.Log("if THIS Weiche: She_Ken trifft auf .../ THIS = " + this.gameObject.tag);
                    //Debug.Log("if OTHER Weiche: She_Ken trifft auf Ken/ OTHER =  " + other.gameObject.tag);
                    SetUpDialogue.personStartingConversation = "She_Ken";
                    SetUpDialogue.personOppositeConversation = "Ken";
                    pnl_Dialogue.SetActive(true);
                    angerOMeter.SetActive(true);
                    cam_SheKen.SetActive(true);
                }
            }
            else if (this.gameObject.CompareTag("Ed"))
            {
                if (other.gameObject.CompareTag("She_Ken"))
                {
                    Debug.Log("if THIS Weiche: Ed trifft auf .../ THIS = " + this.gameObject.tag);
                    Debug.Log("if OTHER Weiche: Ed trifft auf She_Ken/ OTHER =  " + other.gameObject.tag);
                    SetUpDialogue.personStartingConversation = "Ed";
                    SetUpDialogue.personOppositeConversation = "She_Ken";
                    pnl_Dialogue.SetActive(true);
                    angerOMeter.SetActive(true);
                    cam_SheKen.SetActive(true);
                }
            }
            else if (this.gameObject.CompareTag("Ken"))
            {
                if (other.gameObject.CompareTag("Ed"))
                {
                    //Debug.Log("if THIS Weiche: Ken trifft auf .../ THIS = " + this.gameObject.tag);
                    //Debug.Log("if OTHER Weiche: Ken trifft auf Ed/  OTHER =  " + other.gameObject.tag);
                    SetUpDialogue.personStartingConversation = "Ken";
                    SetUpDialogue.personOppositeConversation = "Ed";
                    pnl_Dialogue.SetActive(true);

                }
            }
            else if (this.gameObject.CompareTag("She_Ken_Not"))
            {
                if (other.gameObject.CompareTag("She_Ken"))
                {
                    //Debug.Log("if THIS Weiche: She_Ken_Not trifft auf .../ THIS = " + this.gameObject.tag);
                    //Debug.Log("if OTHER Weiche: She_Ken trifft auf Ed/  OTHER =  " + other.gameObject.tag);
                    SetUpDialogue.personStartingConversation = "She_Ken_Not";
                    SetUpDialogue.personOppositeConversation = "She_Ken";
                    pnl_Dialogue.SetActive(true);
                }
            }
        }
        //public void OnTriggerExit(Collider other)
        //{
        //    if (this.gameObject.CompareTag("She_Ken") || this.gameObject.CompareTag("Ken") || this.gameObject.CompareTag("Ed"))
        //    {
        //        pnl_Dialogue.SetActive(false);
        //        angerOMeter.SetActive(false);
        //        cam_Lili.SetActive(false);                
        //    }
        //}
    }
}
