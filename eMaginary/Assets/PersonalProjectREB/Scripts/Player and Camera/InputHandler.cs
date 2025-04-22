using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.Cinemachine;

namespace eMaginary
{
    public class InputHandler : MonoBehaviour
    {
        public GameObject actor;
        Transform followTarget;
        [SerializeField] List<GameObject> targets = new List<GameObject>();
        int keyCodeStart;

        void Start()
        {
            followTarget = Camera.main.GetComponent<CameraController>().followTarget;
            followTarget = actor.transform;//Initialisierung  
            keyCodeStart = 49;// entspricht KeyCode.Alpha1
        }

        void Update()
        {
            for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
            {
                if (Input.GetKeyDown((KeyCode)(49 + targetIndex)))
                {
                    SetFirstPerson(targetIndex);
                }
            }
        }

        public void SetFirstPerson(int targetIndex)
        {
            GameObject firstPerson = targets[targetIndex];
            Camera.main.GetComponent<CameraController>().followTarget = firstPerson.transform;

            for (int i = 0; i < targets.Count; i++)
            {
                if (firstPerson == targets[i])
                {
                    activate_PlayerController(targets[i]);
                }
                else
                {
                    deactivate_PlayerController(targets[i]);
                }
            }
        }

        private void activate_PlayerController(GameObject newPlayer)
        {
            newPlayer.GetComponent<PlayerController_switch>().enabled = true;
        }
        private void deactivate_PlayerController(GameObject otherPlayer)
        {
            otherPlayer.GetComponent<PlayerController_switch>().enabled = false;
        }


    }
}
