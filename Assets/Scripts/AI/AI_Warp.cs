using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AI_Warp : StateMachineBehaviour
    {
        private string currentSystem;
        private GameObject ai;
        private GameObject ship;
        private bool warping;
        private Rigidbody2D rd2;
        private GameObject sceneScripts;
        private string personality;
        private ThrusterController thrusters;
        private SteeringController steering;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ai = animator.gameObject;
            ship = ai.transform.GetChild(0).gameObject;
            rd2 = ship.GetComponent<Rigidbody2D>();
            sceneScripts = GameObject.Find("SceneScripts");
            currentSystem = ship.GetComponent<ShipVariables>().CurrentSystem.name;
        }
    }
}