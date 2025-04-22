using System;
//using Mono.Cecil.Cil;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -10;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float invertXVal;
    float invertYVal;

    float rotationX;
    float rotationY;

    private void Start()
    {
        //Cursor.visible = false;

        //Confined bleibt im Fenster, Locked bleibt in der Mitte
        //Cursor.lockState = CursorLockMode.Confined; 
        Cursor.lockState = CursorLockMode.None;
    }

    //private void LateUpdate()
    private void Update()
    {
        /*if (invertX == true)
        {
            invertXVal = -1;
        }
        else
        {
            invertXVal = 1;
        }*/

        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        // Mouse Y erfasst nur Tastatur
        //rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;

        // ursprünglich aus dem Video
        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        //rotationX += Input.GetAxis("Horizontal") * invertYVal * rotationSpeed;


        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // Mouse X erfasst nur Tastatur
        //rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;
        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;
        //rotationY += Input.GetAxis("Vertical") * invertXVal * rotationSpeed;


        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0,0,distance); 
        transform.rotation = targetRotation;

    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
