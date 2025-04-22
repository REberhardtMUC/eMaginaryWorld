using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class ChangeVinyl : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] GameObject obj_Vinyl;

    [SerializeField] GameObject obj_Fight;
    [SerializeField] GameObject obj_Flight;
    [SerializeField] GameObject obj_Uebersprung;

    [SerializeField] GameObject txt_Vinyl;
    [SerializeField] Sprite[] vinylSprites;

    [SerializeField] GameObject player;

    private int indexPlatte = 0;


    public void OnPointerEnter(PointerEventData eventData)
    {
        txt_Vinyl.SetActive(true);
    }

    public void OnButtonClick()
    {
        //player.SetActive(true);
        

        indexPlatte++;
        if (indexPlatte < vinylSprites.Length)
        {
            txt_Vinyl.SetActive(false);
            audioSource.resource = audioClips[indexPlatte];
            audioSource.Play();
            obj_Vinyl.GetComponent<UnityEngine.UI.Image>().sprite = vinylSprites[indexPlatte];
        }
        else
        {
            obj_Fight.SetActive(true);
            obj_Flight.SetActive(true);
            obj_Uebersprung.SetActive(true);
            this.gameObject.SetActive(false);
            indexPlatte = -1;
        }
    }

}
