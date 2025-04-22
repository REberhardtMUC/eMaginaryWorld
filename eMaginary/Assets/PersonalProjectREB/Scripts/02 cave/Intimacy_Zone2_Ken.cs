using System;
using System.Collections;
using System.Collections.Generic;
using eMaginary.Energy;
using Unity.VisualScripting;
using UnityEngine;

public class Intimacy_Zone2_Ken : MonoBehaviour
{
    [SerializeField] Animator animatorSheKen;
    [SerializeField] Animator animatorKen;
    [SerializeField] GameObject chase_SheKen;
    [SerializeField] GameObject powerUp_FollowMe;
    [SerializeField] GameObject powerUp_Light;
    [SerializeField] GameObject[] img_heart;
    [SerializeField] GameObject energieEi;

    //private Energy_Management_Player manageMyEnergy;

    //[SerializeField] GameObject zone2_Collider;

    private GameObject playerChased;// Ken
    private Rigidbody rb_playerChased;

    private GameObject playerChasing;// She_Ken
    private Rigidbody rb_playerChasing;

    public int index_MeetingKen = 0;
    private bool has_met;

    void Start()
    {
        playerChased = GameObject.Find("Ken_ArmatureMesh");
        rb_playerChased = playerChased.GetComponent<Rigidbody>();

        playerChasing = GameObject.Find("SheKen_ArmatureMesh");
        rb_playerChasing = playerChasing.GetComponent<Rigidbody>();
      
    }


    private void OnTriggerEnter(Collider other)
    {
        if (playerChased.GetComponent<FollowMe>().powerUp_isActive == true)
        {
            playerChased.GetComponent<FollowMe>().setFalse();
        }

        has_met = chase_SheKen.GetComponent<Chase_SheKen>().get_hasMet();

        if (other.CompareTag("She_Ken") && has_met == false)
        {
            //if power_up was active, deactivate power_up                

            if (index_MeetingKen < 3)
            {
                img_heart[index_MeetingKen].SetActive(true);
            }

            // He turns towards her
            rb_playerChased.transform.LookAt(rb_playerChasing.transform.position);
            //Vector3 lookDirectionHe = (playerChasing.transform.position - playerChased.transform.position).normalized * Time.deltaTime;
            //rb_playerChased.MoveRotation(Quaternion.LookRotation(lookDirection - new Vector3(1, 0, 1)));
            //rb_playerChased.MoveRotation(Quaternion.LookRotation(lookDirectionHe));

            // She turns towards him
           // rb_playerChasing.transform.LookAt(rb_playerChased.transform.position);
            //Vector3 lookDirection = (playerChased.transform.position - playerChased.transform.position).normalized * Time.deltaTime;
            //rb_playerChasing.MoveRotation(Quaternion.LookRotation(lookDirection));

            switch (index_MeetingKen)
            { 
                case 0:
                    animatorSheKen.SetTrigger("beExcited");
                    animatorKen.SetTrigger("payAttention");
                    break;
                case 1:
                    animatorSheKen.SetTrigger("tryKiss");
                    animatorKen.SetTrigger("beExcited");
                    break;
                case > 1:
                    animatorSheKen.SetTrigger("tryKiss");
                    animatorKen.SetTrigger("tryKiss");
                    int newEnergyLevel = GameObject.Find("LoadEnergy").GetComponent<EnergyInAction>().SetNewEnergyLevel();
                    EnergyManagement.energyLevel = newEnergyLevel;
                    energieEi.SetActive(true);
                    break;
            }
            index_MeetingKen++;
            chase_SheKen.GetComponent<Chase_SheKen>().set_hasMet(true);
        }

        //Treffen Ken und Lili aufeinander, wird "Following Me" deaktiviert
        if (other.CompareTag("Player"))
        {
            powerUp_FollowMe.GetComponent<ActivateFollowMe>().SetBackPlayingMode();
        }
        //zone2_Collider.GetComponent<CapsuleCollider>().isTrigger = false;

    }
    private void OnTriggerExit(Collider other)
    {
        animatorSheKen.SetTrigger("backToWork");
        //zone2_Collider.GetComponent<CapsuleCollider>().isTrigger = true;
    }
    public void DeleteHeartIcons()
    {
        for (int j = 0; j < 3; j++)
            img_heart[j].SetActive(false);
    }

}
