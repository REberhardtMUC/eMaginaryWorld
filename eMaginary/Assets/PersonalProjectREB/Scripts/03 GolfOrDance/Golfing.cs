using System;
using System.Collections;
using System.Collections.Generic;
using eMaginary;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace eMaginary.StateManagement
{
    public class Golfing : State
    {
        public Golfing(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.GOLFING;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            anim.SetTrigger("startGolfing");
            anim.applyRootMotion = true;
            InputHandler_onEnter.canSetKen = true;
            base.Enter();
        }

        public override void Update()
        {
            if (InputHandler_onEnter.isMovingFreely)
            {
                nextState = new Idle(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                stage = EVENT.EXIT;
                InputHandler_onEnter.isMovingFreely = false;
            }
        }

        public override void Exit()
        {
            anim.ResetTrigger("startGolfing");
            anim.SetTrigger("isWalking");
            MovingAround.taskAccomplished[1] = true;
            anim.applyRootMotion = false;
            base.Exit();
        }
    }
}
