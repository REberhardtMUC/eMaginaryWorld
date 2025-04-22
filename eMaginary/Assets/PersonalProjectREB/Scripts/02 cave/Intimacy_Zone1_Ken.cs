using System.Collections;
using UnityEngine;

public class Intimacy_Zone1_Ken : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject chase_SheKen;
    private string triggerParameter;
    private bool has_met = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("She_Ken"))
        {
            chase_SheKen.GetComponent<Chase_SheKen>().is_close = true;

            int animationIndex = Random.Range(0, 2);
            switch(animationIndex)
            {
                case 0:
                    triggerParameter = "strikePoseA";
                    break;
                case 1:
                    triggerParameter = "strikePoseB";
                    break;
            }
            animator.SetTrigger(triggerParameter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("She_Ken"))
        {
            chase_SheKen.GetComponent<Chase_SheKen>().set_hasMet(false);
            chase_SheKen.GetComponent<Chase_SheKen>().is_close = false;
            animator.SetTrigger("stopPosing");
        }
    }
}
