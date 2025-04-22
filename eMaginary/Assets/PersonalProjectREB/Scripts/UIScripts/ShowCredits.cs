using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetTrigger("show_credits");
        }
    }
}
