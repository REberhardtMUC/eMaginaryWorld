using System;
using System.Collections;
using System.Collections.Generic;
using eMaginary;
//using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace eMaginary.StateManagement
{
    public class Discussing : State
    {
        public Discussing(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.DISCUSSING;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            if (InputHandler_onEnter.isMovingFreely)
            {
                nextState = new Idle(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
