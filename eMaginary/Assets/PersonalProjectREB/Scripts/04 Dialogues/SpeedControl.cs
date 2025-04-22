using UnityEngine;

namespace eMaginary.StateManagement
{
    public class SpeedControl : MonoBehaviour
    {
        public enum SPEED { FAST, SLOW }

        public SPEED speed;

        void OnTriggerEnter(Collider other)
        {
            if (speed == SPEED.FAST)
            {
                other.gameObject.GetComponent<PlayerController_switch>().moveSpeed_fast = 10;
            }
            else if (speed == SPEED.SLOW)
            {
                other.gameObject.GetComponent<PlayerController_switch>().moveSpeed_fast = 4;
                if(other.CompareTag("Ed"))
                {   
                    InputHandler_onEnter.hasArrived_Ed = true;
                    Debug.Log("SpeedControl || hasArrived_Ed TRUE?: " + InputHandler_onEnter.hasArrived_Ed);
                }
               
            }
        }
    }
}
