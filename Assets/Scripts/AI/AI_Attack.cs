using Assets.Scripts.Ship;
using Assets.Scripts.System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AI_Attack : StateMachineBehaviour
    {
        private GameObject ai;
        private GameObject ship;
        private Rigidbody2D rd2;
        private GameObject sceneScripts;
        private string personality;
        private GameObject target;
        private ThrusterController thrusters;
        private WeaponController weapons;
        private SteeringController steering;
        private List<GameObject> targetList = new List<GameObject> { };

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ai = animator.gameObject;
            ship = ai.transform.GetChild(0).gameObject;
            rd2 = ship.GetComponent<Rigidbody2D>();
            sceneScripts = GameObject.Find("SceneScripts");
            thrusters = sceneScripts.GetComponent<ThrusterController>();
            weapons = sceneScripts.GetComponent<WeaponController>();
            steering = sceneScripts.GetComponent<SteeringController>();
            target = ai.GetComponent<AIController>().Target;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            attack(animator);
        }

        private void attack(Animator animator)
        {
            var weaponSlots = ship.transform.GetChild(0).GetChild(0);
            if (ship.GetComponent<ShipVariables>().HullHp < ship.GetComponent<ShipVariables>().MaxHullHp * .2f)
            {
                animator.SetInteger("State", 8);
            }
            if (Vector3.Distance(ship.transform.position, target.transform.position) < 5 && target.GetComponent<Rigidbody2D>().velocity.magnitude < 3)
            {
                steering.RotateTowards(ship, -(ship.transform.position - target.transform.position).normalized);
                rd2.drag = .3f;
                thrusters.StopThruster(ship);
            }
            else
            {
                thrusters.Accelerate(ship);
                steering.RotateTowards(ship, -(ship.transform.position - target.transform.position).normalized);
                rd2.drag = 0f;
            }
            foreach (Transform weapon in weaponSlots)
            {
                if (weapon.childCount > 0 & Vector3.Distance(ship.transform.position, target.transform.position) < 10)
                {
                    weapons.Shoot(ship, weapon.transform.GetChild(0).gameObject);
                }
            }

            if (target.tag == "AIShip" || target.tag == "PlayerShip")
            {
                if (target.GetComponent<ShipVariables>().HullHp < target.GetComponent<ShipVariables>().MaxHullHp * .1f)
                {
                    animator.SetInteger("State", 0);
                }
            }

            if (target.tag == "Asteroid")
            {
                if (target.GetComponent<AsteroidController>().Health <= 0)
                {
                    animator.SetInteger("State", 7);
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            target = null;
        }
    }
}