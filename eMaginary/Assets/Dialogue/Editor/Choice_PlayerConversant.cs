using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
//using UnityEditor.Experimental.GraphView;

namespace RPG.Dialogue
{
    public class Choice_PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();        
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetDialoguePart();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);            
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            isChoosing = false;
            Next();
        }

        public bool GetWhoIsSpeaking()
        {
            return currentNode.IsPlayerSpeaking();
        }

        public int GetAngerLevel()
        {
            return currentNode.GetAngerLevel();
        }
        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                return;
            }
            if(currentDialogue.GetRandomChildren(currentNode).Count() > 0)
            {
                DialogueNode[] childNodes = currentDialogue.GetRandomChildren(currentNode).ToArray();
                int randomIndex = Random.Range(0, childNodes.Count());
                Debug.Log("Name des Nodes: " + currentNode.name + "| Länge des Arrays: " + childNodes.Count());
                currentNode = childNodes[randomIndex];
            }
          
        }

        public void SetFirst()
        {
            //DialogueNode[] allNodes = currentDialogue.GetAllNodes().ToArray();
            //currentNode = allNodes[0];
            currentNode = currentDialogue.GetRootNode();
        }
        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}


