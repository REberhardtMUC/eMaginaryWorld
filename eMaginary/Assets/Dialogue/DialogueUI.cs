using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI txt_WhoIsSpeaking;
        [SerializeField] TextMeshProUGUI txt_heSays_sheSays;
        [SerializeField] Button btn_Next;
        [SerializeField] Slider sld_angerOMeter;
        [SerializeField] Animator anim_Lili;
        string _personStartingConversation;
        string _personOppositeConversation;
        public GameObject txt_hintsAndHelp;

        private void Start()
        {            
            txt_hintsAndHelp = GameObject.Find("txt_hintsAndHelp");
            btn_Next.onClick.AddListener(Next);// not calling the function, just adding it!!!
            Update_FirstPerson();
        }
        void Update()
        {
            Update_FirstPerson();
            UpdateUI();
        }

        private void Update_FirstPerson()
        {
            _personStartingConversation = SetUpDialogue.personStartingConversation;
            playerConversant = GameObject.FindGameObjectWithTag(_personStartingConversation).GetComponent<PlayerConversant>();
            _personOppositeConversation = SetUpDialogue.personOppositeConversation;
        }

        private void Next()
        {            
            playerConversant.Next();
            UpdateUI();
        }
        private void UpdateUI()
        {
            txt_heSays_sheSays.text = playerConversant.GetText();
            if (playerConversant.GetWhoIsSpeaking())
            {
                txt_WhoIsSpeaking.text = _personStartingConversation;
            }
            else
            {
                txt_WhoIsSpeaking.text = _personOppositeConversation;
            }
            sld_angerOMeter.value = playerConversant.GetAngerLevel();


            if (sld_angerOMeter.value == 100 && StartDialogue.kickAss == true) 
            {              
                anim_Lili.SetTrigger("kickAssKen");
                StartDialogue.kickAss = false;                
            }

            // Next-Button verschwindet, wenn es keine weiteren ChildNodes gibt
            btn_Next.gameObject.SetActive(playerConversant.HasNext());

            if (!playerConversant.HasNext()== true && StartDialogue.showHint == true)
            {
                if (SetUpDialogue.personStartingConversation == "Lili")
                {
                    txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = "W�hle die Steuerung von Ed mit Taste 3 und lass ihn zur Diskussionsplattform laufen.";
                }
                else if (SetUpDialogue.personStartingConversation == "Ed")
                {
                    txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = "Hole Ken ab und verlasse mit ihm die Diskussionsplattform. Ken wird dir folgen und unterwegs k�nnt ihr plaudern.";
                    playerConversant.currentDialogue = playerConversant.followUpDialogue;
                }
                StartDialogue.showHint = false;
            }


        }

        public void ResetDialogue()
        {
            playerConversant.SetFirst();
        }

    }
}

