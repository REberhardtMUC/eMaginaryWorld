using UnityEditor;
using UnityEngine;

public class SetHint : MonoBehaviour
{
    string dialogueTitel = "Was nun?";
    string dialogueText = "W‰hle die Steuerung von Ed mit Taste 3 und lass ihn zur Diskussionsplattform laufen.";
   
    void Start()
    {
        EditorUtility.DisplayDialog(dialogueTitel, dialogueText, "Schlieﬂen");
    }

    void Update()
    {
        
    }
}
