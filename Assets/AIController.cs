using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts.Ship;
using Unity.Mathematics;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private bool doingSomething = false;
    private static GameObject SceneScripts;
    private static GameObject Systems;
    string currentSystem;

    void Start()
    {
        SceneScripts = GameObject.Find("/SceneScripts");
        Systems = GameObject.Find("/Systems");
        currentSystem = "";
    }
    void Update()
    {
        followTarget();
    }

    private void followTarget()
    {
        SceneScripts.GetComponent<ThrusterController>().Accelerate(this.transform.GetChild(0).gameObject);
        SceneScripts.GetComponent<SteeringController>().RotateTowards(this.transform.GetChild(0).gameObject, (-this.transform.GetChild(0).position - GameObject.Find("/Player").transform.GetChild(0).position).normalized);
        print((this.transform.GetChild(0).position + GameObject.Find("/Player").transform.GetChild(0).position).normalized);
    }
    private IEnumerator warp()
    {
        GameObject chosenSystem = chooseSystem();
        currentSystem = chosenSystem.name;
        if (chosenSystem.name != currentSystem)
        {
            print(chosenSystem.name +", "+ currentSystem);
            SceneScripts.GetComponent<HyperDriveController>().AutoPilot(chosenSystem.transform, this.transform.GetChild(0).gameObject);
            yield return new WaitUntil(() => this.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude > 20);
            yield return new WaitUntil(() => this.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude == 0);
            doingSomething = false;
        }
        else
        {
            
        }
    }

    private GameObject chooseSystem()
    {
        int cluster = UnityEngine.Random.Range(0, Systems.transform.childCount);
        int system = UnityEngine.Random.Range(0, Systems.transform.GetChild(cluster).childCount);
        return Systems.transform.GetChild(cluster).GetChild(system).gameObject;
    }
}
