using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] public Dialogue currentDialogue;
        [SerializeField] public Dialogue followUpDialogue;
        DialogueNode currentNode = null;

        private void Awake()
        {
        //    DialogueNode[] allNodes = currentDialogue.GetAllNodes().ToArray();
        //    currentNode = allNodes[0];
            currentNode = currentDialogue.GetRootNode();

            Debug.Log(currentNode.GetDialoguePart());            
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetDialoguePart();
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
            DialogueNode[] childNodes = currentDialogue.GetAllChildren(currentNode).ToArray();
            int index = Random.Range(0, childNodes.Count());
            currentNode = childNodes[index];
        }

        public void SetFirst()
        {
            currentNode = currentDialogue.GetRootNode();
        }
        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}


