using UnityEditor;
using UnityEngine;
using TMPro;

//TO DO: PR�fen, ob diese Funktion aufgerufen/ ben�tigt wird!!
public class SetHint : MonoBehaviour
{
    public GameObject txt_hintsAndHelp;
   
    void Start()
    {
        txt_hintsAndHelp = GameObject.Find("txt_hintsAndHelp");
        txt_hintsAndHelp.GetComponent<TextMeshProUGUI>().text = "BLa bla bla W�hle die Steuerung von Ed mit Taste 3 und lass ihn zur Diskussionsplattform laufen.";
    }

}
