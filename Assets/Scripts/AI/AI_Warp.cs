using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AI_Warp : StateMachineBehaviour
    {
        private string currentSystem;
        private GameObject ai;
        private GameObject ship;
        private bool warping;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ai = animator.gameObject;
            ship = ai.transform.GetChild(0).gameObject;
            warping = false;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!warping)
            {
                warping = true;
                ai.GetComponent<AIController>().StartCoroutine("Warp");
            }
        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }

    }
}
