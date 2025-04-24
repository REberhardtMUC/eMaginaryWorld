using UnityEditor;
using UnityEngine;
using TMPro;

//TO DO: PRüfen, ob diese Funktion aufgerufen/ benötigt wird!!
public class SetHint : MonoBehaviour
{
    public GameObject txt_hintsAndHelp;
   
    void Start()
    {
        txt_hintsAndHelp = GameObject.Find("txt_hintsAndHelp");
        txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = "BLa bla bla Wähle die Steuerung von Ed mit Taste 3 und lass ihn zur Diskussionsplattform laufen.";
    }

}
