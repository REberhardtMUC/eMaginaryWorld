using UnityEngine;

public class ArrivalMountain : MonoBehaviour
{
    [SerializeField] GameObject trigger_closeDoor;
    
    private void OnTriggerEnter(Collider other)
    {
        trigger_closeDoor.GetComponent<ParentCharacter>().GetOffLift();
    }
}
