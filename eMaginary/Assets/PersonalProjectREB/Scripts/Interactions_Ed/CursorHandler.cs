using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorHandler: MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void setCursorVisible()
    {
        Cursor.visible = true;
    }

}
