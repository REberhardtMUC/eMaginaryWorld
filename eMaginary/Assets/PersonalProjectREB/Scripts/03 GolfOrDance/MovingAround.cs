using System.Threading.Tasks;
using eMaginary;
using UnityEngine;
using UnityEngine.AI;

namespace eMaginary.StateManagement
{
    public class MovingAround : MonoBehaviour
    {
        [SerializeField] GameObject nextLevel;
        Animator anim;
        public GameObject charFollowed_A, charFollowed_B, charFollowed_C;
        State currentState;
        public static bool[] taskAccomplished = new bool[2];
        private bool setTeaserNextLevel = false;

        void Start()
        {
            taskAccomplished[0] = false;
            taskAccomplished[1] = false;
            anim = this.GetComponent<Animator>();
            currentState = new Idle(this.gameObject, anim, charFollowed_A, charFollowed_B, charFollowed_C);
            //foreach (GameObject character in InputHandler_onEnter.charactersInGame)
            //{
            //    Debug.Log("Eintrag: " + character.tag);
            //}
        }

        void Update()
        {
            currentState = currentState.Process();

            foreach (bool task in taskAccomplished)
            {
                if (!task)
                {
                    setTeaserNextLevel = false;
                    break;
                }
                else
                {
                    setTeaserNextLevel = true;
                }
            }

            if (setTeaserNextLevel)
            {
                nextLevel.SetActive(true);
                //anim.SetTrigger("isFollowing");
                //neue Animation suchen? Suchend? Unsicher?
            }
        }
    }
}
