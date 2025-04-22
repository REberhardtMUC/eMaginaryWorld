using System;
using UnityEngine;

public class ShowHideHints : MonoBehaviour
{
    [SerializeField] GameObject img_background_hints;

    float posX;
    float posY;

    Boolean hint_is_visible = false;
    

    private void Start()
    {
        posX = img_background_hints.GetComponent<RectTransform>().anchoredPosition.x;
        posY = img_background_hints.GetComponent<RectTransform>().anchoredPosition.y;
    }

    public void OnButtonClick()
    {
        if (hint_is_visible == false)
        {
            img_background_hints.transform.position = new Vector3(posX, posY + 100, 0);

            hint_is_visible = true;
        }
        else
        {
            img_background_hints.transform.position = new Vector3(posX, posY, 0);

            hint_is_visible = false;
        }
    }
}
