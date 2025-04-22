using System;
using System.Collections;
using System.Collections.Generic;
using eMaginary;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace eMaginary.StateManagement
{

    public class Following : State
    {
        public static Camera mainCam, lookWhoIsFollowing_Cam;

        string activeTag = "";
        //bool lookTwice = false;
        bool readyToImitate = false;

        public Following(GameObject _charFollowing, Animator _anim, GameObject _charFollowed_A, GameObject _charFollowed_B, GameObject _charFollowed_C)
                    : base(_charFollowing, _anim, _charFollowed_A, _charFollowed_B, _charFollowed_C)
        {
            state = STATE.FOLLOWING;
            Debug.Log("Zustand: " + state.ToString());
        }

        public override void Enter()
        {
            rb_Following = charFollowing.GetComponent<Rigidbody>();
            mainCam = Camera.main.GetComponent<Camera>();
            activeTag = charFollowed_A.tag;//charFollowedA ist nun als charFollowed gesetzt
            lookWhoIsFollowing_Cam = charFollowed_A.GetComponentInChildren<Camera>();
            if (activeTag == "Lili")
            {
                EditorUtility.DisplayDialog("Was nun?", "Laufe als Lili den Bachlauf entlang. Ken wird dir folgen. Steige nicht am Magic Lift aus, sondern laufe weiter bis zum Ende des Wasserlaufes." +
                  "Wenn du Ken abh‰ngst, kann er mit der Taste 1 selbst hinterherlaufen. Mit der Taste 4 wechselst du wieder zu Lili.", "Schlieﬂen");
                InputHandler_onEnter.canSetKen = true;

            }

            base.Enter();
        }

        public override void Update()
        {
            if (!CanSeeFollowedPerson(charFollowed_A))
            {
                FollowPerson();
                Debug.Log("Following aktiviert");
            }
            else if (CanSeeFollowedPerson(charFollowed_A))
            {
                anim.applyRootMotion = true;
                if (activeTag == "She_Ken")
                {
                    nextState = new Dancing(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                    stage = EVENT.EXIT;
                }
                else if (activeTag == "Ed")
                {
                    nextState = new Golfing(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                    stage = EVENT.EXIT;
                }
                else if (activeTag == "Lili" && InputHandler_onEnter.mountainReached == true)
                {
                    nextState = new Discussing(charFollowing, anim, charFollowed_A, charFollowed_B, charFollowed_C);
                    stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            ResetCameras();
            base.Exit();
        }

        public static void ResetCameras()
        {
            lookWhoIsFollowing_Cam.enabled = false;
            mainCam.rect = new Rect(0, 0, 1, 1);
        }
    }
}