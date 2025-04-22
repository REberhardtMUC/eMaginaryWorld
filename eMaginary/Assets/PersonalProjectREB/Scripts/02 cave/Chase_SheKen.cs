using System;
using System.Collections.Generic;
using UnityEngine;

public class Chase_SheKen : MonoBehaviour
{
    public float speed = 0.1f;

    public bool is_busy = true;// Bedienung des Faxgerätes
    public bool is_close = false;// Intimacy Zone 1, einander im Blick
    public static bool has_met = false;// Intimacy Zone 2, direkt voreinander stehen

    private Rigidbody rb_chasing; // She_Ken
    private GameObject player_chased; // Ken

    void Start()
    {
        rb_chasing = GetComponent<Rigidbody>();
        player_chased = GameObject.Find("Ken_ArmatureMesh");
    }
    
    void Update()
    {
        if (is_close == false && is_busy == false && has_met == false)
        {
            Vector3 lookDirection = (player_chased.transform.position - transform.position).normalized * speed * Time.deltaTime;
            //rb_chasing.useGravity = false;
            //rb_chasing.isKinematic = true;
            rb_chasing.transform.LookAt(player_chased.transform.position);
            rb_chasing.MovePosition(transform.position + lookDirection);
            
        }
        else
        {
            //rb_chasing.useGravity = true;
            //rb_chasing.isKinematic = false;
        }

        /*Versuch, über die Distanz statt über die Steuerung von Variablen zu gehen.
         * klappt noch nicht
         */
        /*
        float distanceXabs = Math.Abs(player_chased.transform.position.x - transform.position.x);
        float distanceYabs = Math.Abs(player_chased.transform.position.y - transform.position.y);

        Debug.Log("Differenz der X Positionen: " + distanceXabs);
        Debug.Log("Differenz der Y Positionen: " + distanceYabs);

        if (distanceXabs > 2 || distanceYabs > 2)
        {            
            Vector3 lookDirection = (player_chased.transform.position - transform.position).normalized * speed * Time.deltaTime;
            rb_chasing.MovePosition(transform.position + lookDirection);
            rb_chasing.MoveRotation(Quaternion.LookRotation(lookDirection));
        }*/
    }
    public bool get_hasMet()
    { return has_met; }

    public void set_hasMet(bool hasMet)
    {  has_met = hasMet; }
}
