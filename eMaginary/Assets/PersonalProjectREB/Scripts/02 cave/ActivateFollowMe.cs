using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateFollowMe : MonoBehaviour
{
    [SerializeField] Rigidbody rb_Following;//Ken
    [SerializeField] Rigidbody rb_Followed; // Lili
    [SerializeField] GameObject lili; // Lili
    [SerializeField] GameObject Ken_cam;
    [SerializeField] GameObject powerUp_FollowMe;
    [SerializeField] GameObject powerUp_Light;

    public bool followMe_isActive = false;
    private GameObject attention_powerUp;
    private Animator anim_Following;
    private float spawnPosX, spawnPosZ;
    //bool RotateLili = false;

    public bool turnAround = false;


    private void Start()
    {
        attention_powerUp = GameObject.Find("attention_PowerUp");
        anim_Following = rb_Following.GetComponent<Animator>();
    }

    public bool get_turnAround()
    {  return turnAround; }

    public void set_turnAround(bool turnAround)
        { this.turnAround = turnAround; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Sie (mit PowerUp) dreht sich einmalig in die Richtung ihres Verfolgers
            turnAround = true;
            //rb_Followed.transform.LookAt(rb_Following.transform.position);// this makes her turn around

            //Vector3 lookDirection = (rb_Following.transform.position - rb_Followed.transform.position).normalized;
            //rb_Followed.transform.rotation = Quaternion.Euler(lookDirection.x, 0, 0);

            attention_powerUp.SetActive(false);
            followMe_isActive = true;
            powerUp_Light.SetActive(true);
            Ken_cam.SetActive(true);

            StartCoroutine(KeepPowerUp());
        }
    }
    IEnumerator KeepPowerUp()
    {
        yield return new WaitForSeconds(10);
        SetBackPlayingMode();
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(WaitForWalkingOn());     
    }
    IEnumerator WaitForWalkingOn()
    {
        yield return new WaitForSeconds(1);
        turnAround = false;
    }   

    public void SetBackPlayingMode()
    {
        if (followMe_isActive)
        {
            followMe_isActive = false;
            powerUp_Light.SetActive(false);

            // Animationen Ken zurücksetzen
            Ken_cam.SetActive(false);
            anim_Following.SetTrigger("busyOnPhone");
            anim_Following.applyRootMotion = true;
            
            //rb_Following.useGravity = true;
            //rb_Following.isKinematic = false;

            // Neue Lichtsäule setzen und aktivieren
            spawnPosX = Random.Range(-10, 8);
            spawnPosZ = Random.Range(1, 11);
            powerUp_FollowMe.transform.position = new Vector3(spawnPosX, -7.1f, spawnPosZ);
            attention_powerUp.SetActive(true);

            
        }
       
    }

}
