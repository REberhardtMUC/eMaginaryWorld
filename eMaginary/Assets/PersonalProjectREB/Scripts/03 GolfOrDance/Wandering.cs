using System;
using eMaginary;
using UnityEngine;

namespace eMaginary.StateManagement
{
    public class Wandering : State
    {
        private float speed = 4;
        private Rigidbody rb_Following;
        public static Camera mainCam, lookWhoIsFollowing_Cam;

        string activeTag = "";
        bool readyToImitate = false;

        public Wandering(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.WANDERING;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            anim.SetTrigger("isWalking");
            base.Enter();
        }

        public override void Update()
        {
            charFollowed = WhoIsCloser();
            charNotFollowed = SetNotFollowed();
            charNotFollowed2 = SetNotFollowed2();

            Debug.Log("Wandering || hasArrived_Ed FALSE?: " + InputHandler_onEnter.hasArrived_Ed );

            if (CanSeeFollowedPerson(charFollowed) && InputHandler_onEnter.hasArrived_Ed == true)
            {
                Debug.Log("Wandering || hasArrived_Ed TRUE?: " + InputHandler_onEnter.hasArrived_Ed);
                nextState = new Waiting(charFollowing, anim, charFollowed, charNotFollowed, charNotFollowed2);
                stage = EVENT.EXIT;
            }
        }       

        public override void Exit()
        {
            rb_Following = charFollowing.GetComponent<Rigidbody>();
            mainCam = Camera.main.GetComponent<Camera>();
            activeTag = charFollowed.tag;//charFollowedA ist nun als charFollowed gesetzt
            lookWhoIsFollowing_Cam = charFollowed.GetComponentInChildren<Camera>();

            if (activeTag != "")
            {
                InputHandler_onEnter.SetTriggerAsFirstPerson(activeTag);
            }
            SplitCameraView();

            InputHandler_onEnter.isMovingFreely = false;
            base.Exit();
        }

        public void SplitCameraView()
        {
            lookWhoIsFollowing_Cam.enabled = true;
            mainCam.rect = new Rect(-0.5f, 0, 1, 1);
        }
    }
}
