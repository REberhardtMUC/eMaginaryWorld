using System.Collections;
using UnityEngine;

public class Intimacy_Zone_SheKen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject chase_SheKen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ken"))
        {
            chase_SheKen.GetComponent<Chase_SheKen>().is_busy = false;
            animator.SetTrigger("goChase");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ken"))
        {
            chase_SheKen.GetComponent<Chase_SheKen>().is_busy = true;
            animator.SetTrigger("backToWork");
        }
    }
}
