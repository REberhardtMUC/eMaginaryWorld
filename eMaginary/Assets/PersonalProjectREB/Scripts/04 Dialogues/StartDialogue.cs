using eMaginary.StateManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Dialogue
{
    public class StartDialogue : MonoBehaviour
    {
        [SerializeField] GameObject img_UIContainer;
        [SerializeField] GameObject angerOMeter;
        [SerializeField] GameObject cam_Lili;
        Component cmp_playerConversant;
        public static bool kickAss = true;
        public static bool showHint = true;

        private void Start()
        {
            InputHandler_onEnter.hasArrived_Ed = false;
            InputHandler_onEnter.isLevel4 = true;
        }

        private void OnTriggerEnter(Collider other)
        {
           
            if (this.gameObject.tag == "Lili")
            {
                if (other.gameObject.CompareTag("Ken"))
                {
                    SetUpDialogue.personStartingConversation = "Lili";
                    SetUpDialogue.personOppositeConversation = "Ken";
                    img_UIContainer.SetActive(true);
                    angerOMeter.SetActive(true);
                    cam_Lili.SetActive(true);
                }
            }
            else if (this.gameObject.CompareTag("Ed"))
            {
                if (other.gameObject.CompareTag("Lili"))
                {
                    SetUpDialogue.personStartingConversation = "Ed";
                    SetUpDialogue.personOppositeConversation = "Lili";
                    img_UIContainer.SetActive(true);
                    angerOMeter.SetActive(true);
                    cam_Lili.SetActive(true);
                }
            }
            else if (this.gameObject.CompareTag("Ken"))
            {
                if (other.gameObject.CompareTag("Ed"))
                {
                    SetUpDialogue.personStartingConversation = "Ken";
                    SetUpDialogue.personOppositeConversation = "Ed";
                    img_UIContainer.SetActive(true);

                }
            }                    
        }
    
        //public void OnTriggerExit(Collider other)
        //{
        //    if (this.gameObject.CompareTag("She_Ken") || this.gameObject.CompareTag("Ken") || this.gameObject.CompareTag("Ed"))
        //    {
        //        img_UIContainer.SetActive(false);
        //        angerOMeter.SetActive(false);
        //        cam_Lili.SetActive(false);                
        //    }
        //}
    }
}
