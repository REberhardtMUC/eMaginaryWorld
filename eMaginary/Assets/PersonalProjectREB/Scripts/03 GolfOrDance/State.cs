using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace eMaginary.StateManagement
{
    public class State : MonoBehaviour
    {
        public enum STATE
        {
            IDLE, WANDERING, WAITING, FOLLOWING, GOLFING, DANCING, DISCUSSING
        };

        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };

        public STATE state;
        protected EVENT stage;

        protected GameObject charFollowing;
        protected Animator anim;
        //protected IEnumerator<GameObject> charactersInGame;
        protected GameObject charFollowed, charNotFollowed, charNotFollowed2, charFollowed_A, charFollowed_B, charFollowed_C;
        protected Rigidbody rb_Following;
        protected State nextState;
        protected float speed = 4;
       // public static bool hasArrived_Ed = true;
        public static bool hasArrived_Ken = false;
        public static bool hasArrived_Lili = false;

        float visDist = 5.0f;
        float distance = 6.0f;
        float visAngle = 30.0f;


        //public State(GameObject _charFollowing, Animator _anim, IEnumerator<GameObject> _charactersInGame)
        public State(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
        {
            charFollowing = _charFollowing;
            //charactersInGame = _charactersInGame;
            charFollowed_A = _charFollowed_A;
            charFollowed_B = _charFollowed_B;
            charFollowed_C = _charFollowed_C;
            anim = _anim;

            stage = EVENT.ENTER;
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public State Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }

        public bool CanSeeFollowedPerson(GameObject charFollowed)
        {
            Vector3 direction = charFollowing.transform.position - charFollowed.transform.position;
            float angle = Vector3.Angle(direction, charFollowed.transform.forward);

            if (direction.magnitude < visDist && angle < visAngle)
            {
                return true;
            }
            return false;
        }
        public bool IsCloseToFollowedPerson(GameObject charFollowed)
        {
            Vector3 direction = charFollowing.transform.position - charFollowed.transform.position;

            if (direction.magnitude < distance)
            {
                return true;
            }
            return false;
        }
        protected void FollowPerson()
        {
            float distanceXabs = Math.Abs(charFollowed_A.transform.position.x - charFollowing.transform.position.x);
            float distanceYabs = Math.Abs(charFollowed_A.transform.position.y - charFollowing.transform.position.y);

            if (distanceXabs > 1 || distanceYabs > 1)
            {

                Vector3 lookDirection = (charFollowed_A.transform.position - charFollowing.transform.position).normalized * speed * Time.deltaTime;
                charFollowing.transform.LookAt(charFollowed_A.transform.position);

                rb_Following.MovePosition(charFollowing.transform.position + lookDirection);
            }
        }
        protected GameObject WhoIsCloser()
        {
            Vector3 direction_A = charFollowing.transform.position - charFollowed_A.transform.position;
            Vector3 direction_B = charFollowing.transform.position - charFollowed_B.transform.position;
            Vector3 direction_C = charFollowing.transform.position - charFollowed_C.transform.position;

            if (direction_A.magnitude < direction_B.magnitude)
            {
                if (direction_A.magnitude < direction_C.magnitude)
                {
                    charFollowed = charFollowed_A;
                }
                else
                {
                    charFollowed = charFollowed_C;
                }
            }
            else //B < A
            {
                if (direction_B.magnitude < direction_C.magnitude)
                {
                    charFollowed = charFollowed_B;
                }
                else
                {
                    charFollowed = charFollowed_C;
                }
            }
            return charFollowed;
        }
        protected GameObject SetNotFollowed()
        {
            if (charFollowed == charFollowed_A)
            {
                return charFollowed_B;
            }
            else if (charFollowed == charFollowed_B)
            {
                return charFollowed_A;
            }
            else// if (charFollowed == charFollowed_C)
            {
                return charFollowed_A;
            }
        }
        protected GameObject SetNotFollowed2()
        {
            if (charFollowed == charFollowed_A)
            {
                return charFollowed_C;
            }
            else if (charFollowed == charFollowed_B)
            {
                return charFollowed_C;
            }
            else //if (charFollowed == charFollowed_C)
            {
                return charFollowed_B;
            }
        }

    }
}

