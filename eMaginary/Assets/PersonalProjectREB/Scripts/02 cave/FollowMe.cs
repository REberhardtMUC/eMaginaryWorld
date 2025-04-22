using UnityEngine;

public class FollowMe : MonoBehaviour
{
    public float speed = 1;
    [SerializeField] Rigidbody rb_Following;//Ken
    [SerializeField] Rigidbody rb_Followed; // Lili

    public bool powerUp_isActive;
    public bool has_met;

    private void Start()
    {
        powerUp_isActive = false;
    }

    private void LateUpdate()
    {
        powerUp_isActive = GameObject.Find("PowerUp_FollowMe").GetComponent<ActivateFollowMe>().followMe_isActive;
        
        if (powerUp_isActive == true)
        {
            GetComponent<Animator>().applyRootMotion = false;
            //rb_Following.useGravity = false;
            //rb_Following.isKinematic = true;
            Vector3 lookDirection = (rb_Followed.transform.position - rb_Following.transform.position).normalized * speed * Time.deltaTime;
            rb_Following.transform.LookAt(rb_Followed.transform.position); 
            rb_Following.MovePosition(rb_Following.transform.position + lookDirection);
            //rb_Following.MoveRotation(Quaternion.LookRotation(lookDirection));
        }
    }

    public void setFalse()
    {
        powerUp_isActive = false;
    }
}
