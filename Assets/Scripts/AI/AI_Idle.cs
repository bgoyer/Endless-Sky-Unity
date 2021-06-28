using UnityEngine;

namespace Assets.Scripts.AI
{
    public class Ai_Idle : StateMachineBehaviour
    {
        private GameObject ai;
        private GameObject ship;
        private Rigidbody2D rd2;

        /// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ai = animator.gameObject;
            ship = ai.transform.GetChild(0).gameObject;
            rd2 = ship.GetComponent<Rigidbody2D>();
            //animator.SetInteger("State", 4);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}