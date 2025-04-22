using UnityEngine;
using UnityEngine.UIElements;

public class ParentCharacter : MonoBehaviour
{
    public bool is_insideLift;

    public void OnTriggerEnter(Collider other)
    {
        is_insideLift = true;
    }

    public void GetOffLift()
    {
        is_insideLift = false;
    }
}
