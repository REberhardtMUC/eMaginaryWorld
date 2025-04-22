using System;
using System.Collections;
using System.Collections.Generic;
using eMaginary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace eMaginary.StateManagement
{
    public class Waiting : State
    {
        private float speed = 4;
        public static Camera mainCam, lookWhoIsFollowing_Cam;

        string activeTag = "";
        bool lookTwice = false;
        bool readyToImitate = false;

        public Waiting(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.WAITING;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            rb_Following = charFollowing.GetComponent<Rigidbody>();
            mainCam = Camera.main.GetComponent<Camera>();
            activeTag = charFollowed_A.tag;//charFollowedA ist nun als charFollowed gesetzt
            lookWhoIsFollowing_Cam = charFollowed_A.GetComponentInChildren<Camera>();

            anim.SetTrigger("isFollowing");
            anim.applyRootMotion = false;

            base.Enter();
        }

        public override void Update()
        {
            if (!IsCloseToFollowedPerson(charFollowed_A))
            {
                FollowPerson();
                readyToImitate = true;
            }
            else if(readyToImitate)
            {
                Debug.Log("im Level 4?" + InputHandler_onEnter.isLevel4);
                //Debug.Log("charFollowed_A.tag?" + charFollowed_A.tag);
                if (!InputHandler_onEnter.isLevel4 || (InputHandler_onEnter.isLevel4 && charFollowed_A.tag == "Ed"))
                {
                    InputHandler_onEnter.canSetKen = true;
                    nextState = new Following(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                    stage = EVENT.EXIT;
                }
            }            
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}
