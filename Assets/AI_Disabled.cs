using Assets.Scripts.AI;
using Assets.Scripts.Ship;
using UnityEngine;

public class AI_Disabled : StateMachineBehaviour
{
    private GameObject ai;
    private GameObject ship;
    private Rigidbody2D rd2;
    private string personality;

    /// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject;
        ship = ai.transform.GetChild(0).gameObject;
        rd2 = ship.GetComponent<Rigidbody2D>();
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ship.GetComponent<ShipVariables>().HullHP < 1)
        {
            animator.SetInteger("State", 9);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}