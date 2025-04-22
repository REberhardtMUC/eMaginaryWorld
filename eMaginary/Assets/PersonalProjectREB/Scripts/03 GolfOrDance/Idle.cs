using System;
using System.Collections.Generic;
using eMaginary;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace eMaginary.StateManagement
{
    public class Idle : State
    {

        public Idle(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.IDLE;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            anim.SetTrigger("isWalking");
            InputHandler_onEnter.canSetKen = false;
            base.Enter();
        }

        public override void Update()
        {
            charFollowed = WhoIsCloser();
            charNotFollowed = SetNotFollowed();
            charNotFollowed2 = SetNotFollowed2();

            //if (!CanSeeFollowedPerson(charFollowed))
            if (!IsCloseToFollowedPerson(charFollowed))
            {
                nextState = new Wandering(charFollowing, anim, charFollowed, charNotFollowed, charNotFollowed2);
                stage = EVENT.EXIT;
            }
            else
            {
                Debug.Log("Zu nah an ihr dran.");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
