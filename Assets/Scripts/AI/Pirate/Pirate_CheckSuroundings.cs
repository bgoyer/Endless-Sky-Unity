using Assets.Scripts.AI;
using Assets.Scripts.Ship;
using System.Collections.Generic;
using UnityEngine;

public class Pirate_CheckSuroundings : StateMachineBehaviour
{
    private GameObject ai;
    private GameObject ship;
    private GameObject sceneScripts;
    private string personality;
    private GameObject target;
    private ThrusterController thrusters;
    private WeaponController weapons;
    private SteeringController steering;
    private List<GameObject> targetList = new List<GameObject> { };
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject;
        ship = ai.transform.GetChild(0).gameObject;
        sceneScripts = GameObject.Find("SceneScripts");
        target = ai.GetComponent<AIController>().Target;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target == null && targetList.Count == 0)
        {
            foreach (Transform SelectedShip in ship.transform.parent.parent.transform)
            {
                Debug.Log(SelectedShip.transform.parent.name);
                if (SelectedShip.transform.parent.GetComponent<AI_Variables>() == null || !SelectedShip.transform.parent.CompareTag("Player")) return;
                {
                    targetList.Add(SelectedShip.gameObject);
                    if (SelectedShip.transform.parent.GetComponent<AI_Variables>().Class != "Pirate")
                    {
                        targetList.Add(SelectedShip.gameObject);
                    }
                }
                if (SelectedShip.transform.parent.CompareTag("Player")){ targetList.Add(SelectedShip.gameObject); } 

            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}