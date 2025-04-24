using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        //To do: enum for more speakers
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] private string dialoguePart;
        [SerializeField] private List<string> childNodes = new List<string>();
        [SerializeField] static float height = 150;
        [SerializeField] private Rect rect = new Rect(0,0,200, height);

        [SerializeField] int angerLevel = 0;
        [SerializeField] bool isNonViolentAnswer = false;


        public Rect GetRect()
        {  
            return rect; 
        }
        public void SetRectHeight(float _height)
        {
           height = _height;
        }
        
        public string GetDialoguePart()
        {
            return dialoguePart;
        }
     
        public List<string> GetChildNodes()
        {
            return childNodes;
        }
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void SetDialoguePart(string newText)
        {
            if (dialoguePart != newText)
            {
                Undo.RecordObject(this, "Update Dialogue Data");
                dialoguePart = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChildNode(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            childNodes.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChildNode(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            childNodes.Remove(childID);
            EditorUtility.SetDirty(this);
        }
        public void SetPlayerSpeaking(bool _isPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = _isPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public int GetAngerLevel()
        {
            return angerLevel;
        }
       

#endif
    }

}
