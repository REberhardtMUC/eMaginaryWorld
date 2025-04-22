using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.Cinemachine;
using UnityEngine.Rendering;

namespace eMaginary.StateManagement
{
    public class InputHandler_onEnter
        : MonoBehaviour
    {
        GameObject firstPerson;
        Transform cam_followTarget;
        [SerializeField] List<GameObject> characterInput = new List<GameObject>();
        //public static List<GameObject> charactersInGame = new List<GameObject>();
        int keyCodeStart = 49;// entspricht KeyCode.Alpha1
        public static int charIndex;
        static string _newFirstPerson;
        static bool keyPressed = false;
        static bool setTriggerAsFirstPerson = false;
        public static bool canSetKen = true;
        public static bool isMovingFreely = true;
        public static bool playerIsSet = false;
        public static bool mountainReached = false;
        public static bool hasArrived_Ed = true;
        public static bool isLevel4 = false;

        void Start()
        {
            cam_followTarget = Camera.main.GetComponent<CameraController>().followTarget;
            //foreach (var character in characterInput)
            //{   
            //    charactersInGame.Add(character);
            //}
        }

        void Update()
        {
            for (int _charIndex = 0; _charIndex < characterInput.Count; _charIndex++)
            {
                if (Input.GetKeyDown((KeyCode)(keyCodeStart + _charIndex)))
                {
                    keyPressed = true;
                    SetFirstPersonByKey(_charIndex);         
                }
            }
            if (setTriggerAsFirstPerson)
            {
                SetFirstPersonByTrigger();
                playerIsSet = true;
            }            
        }       

        public static void SetTriggerAsFirstPerson(string newFirstPerson)
        {
            _newFirstPerson = newFirstPerson;
            keyPressed = false;
            setTriggerAsFirstPerson = true;
        }

        public void SetFirstPersonByTrigger()
        {
            for (int i = 0; i < characterInput.Count; i++)
            {
                if (characterInput[i].CompareTag(_newFirstPerson))
                {
                    firstPerson = characterInput[i];
                }
            }
            Debug.Log("First Person umgestellt auf: " + firstPerson);
           
            SetControllersAndCam();
            setTriggerAsFirstPerson = false;
        }        

        public void SetFirstPersonByKey(int charIndex)
        {
            firstPerson = characterInput[charIndex];

            if (firstPerson.CompareTag("Ken"))                
            {
                if (canSetKen)
                {
                    firstPerson.GetComponent<Animator>().SetTrigger("isWalking");
                    isMovingFreely = true;
                    SetControllersAndCam();
                }               
            }
            else
            {
                SetControllersAndCam();
            }
               
        }
        private void SetControllersAndCam()
        {
            Camera.main.GetComponent<CameraController>().followTarget = firstPerson.transform;

            for (int i = 0; i < characterInput.Count; i++)
            {
                if (firstPerson == characterInput[i])
                {
                    characterInput[i].GetComponent<PlayerController_switch>().enabled = true;
                }
                else
                {
                    characterInput[i].GetComponent<PlayerController_switch>().enabled = false;
                }
            }
        }
        public void SetMountainReached(Boolean isReached)
        {
            mountainReached = isReached;
        }

    }
}
