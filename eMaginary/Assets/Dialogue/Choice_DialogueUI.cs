using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RPG.UI
{
    public class Choice_DialogueUI : MonoBehaviour
    {
        Choice_PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI txt_WhoIsSpeaking;
        [SerializeField] TextMeshProUGUI txt_heSays_sheSays;
        [SerializeField] Button btn_Next;

        [SerializeField] GameObject randomResponse;
        [SerializeField] Slider sld_angerOMeter;
        [SerializeField] Animator anim_SheKen;

        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject btn_choice_Prefab;

        string _personStartingConversation;
        string _personOppositeConversation;

        private void Start()
        {
            btn_Next.onClick.AddListener(Next);// not calling the function, just adding it!!! (Delegate)
            Update_FirstPerson();

            UpdateUI();
        }
        private void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }
        void Update()
        {
            Update_FirstPerson();
        }

        private void Update_FirstPerson()
        {
            _personStartingConversation = SetUpDialogue.personStartingConversation;
            playerConversant = GameObject.FindGameObjectWithTag(_personStartingConversation).GetComponent<Choice_PlayerConversant>();
            _personOppositeConversation = SetUpDialogue.personOppositeConversation;
        }

       
        private void UpdateUI()
        {
            if (playerConversant.GetWhoIsSpeaking())
            {
                txt_WhoIsSpeaking.text = _personStartingConversation;
            }
            else
            {
                txt_WhoIsSpeaking.text = _personOppositeConversation;
            }
            sld_angerOMeter.value = playerConversant.GetAngerLevel();

            if (sld_angerOMeter.value == 100)
            {
                anim_SheKen.SetTrigger("kickAss");
            }

            //randomResponse oder Choice einblenden
            randomResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {                
                txt_heSays_sheSays.text = playerConversant.GetText();

                // Next-Button verschwindet, wenn es keine weiteren ChildNodes gibt
                btn_Next.gameObject.SetActive(playerConversant.HasNext());
            }           
        }
        int i = 0;
        private void BuildChoiceList()
        {
            //choiceRoot.DetachChildren();
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                // mit dieser Überladung wird ein GameObject als Child eines Parents angelegt                
                GameObject choiceInstance = Instantiate(btn_choice_Prefab, choiceRoot);
                choiceInstance.name = "Button Nr. " + Random.Range(0,100);

                // mit GetComponentInChildren können Komponenten von ChildNodes editiert werden
                TextMeshProUGUI textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetDialoguePart();

                Button button = choiceInstance.GetComponentInChildren<Button>();
                //Debug.Log("Ist die ButtonKomponente aktiv?" +  button.isActiveAndEnabled);

                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                    UpdateUI();
                });                
            }
        }

        public void ResetDialogue()
        {
            playerConversant.SetFirst();
        }

    }
}

