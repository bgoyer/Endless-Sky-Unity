using Assets.Scripts.Player;
using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AI_Mine : StateMachineBehaviour
    {
        private GameObject ai;
        private GameObject currentSystem;
        private GameObject ship;
        private Inventory shipInv;
        private SteeringController steer;
        private WeaponController weapons;
        private ThrusterController thruster;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}