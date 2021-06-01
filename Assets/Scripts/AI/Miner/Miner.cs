using Assets.Scripts.Ship;
using Assets.Scripts.System;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

/// <summary>
/// A hummingbird Machine Learning Agent
/// </summary>
public class Miner : Agent
{
    private GameObject ship;
    private GameObject sceneScripts;
    private GameObject thrusterA;
    private GameObject thrusterB;
    private GameObject weapon;
    private SteeringController stearing;
    private ThrusterController thruster;
    private WeaponController weapons;
    private bool canControl;
    private GameObject target;
    private TrainAreaController TrainingArea;



    [Tooltip("Whether this is training mode or gameplay mode")]
    public bool trainingMode;

    

    /// <summary>
    /// The amount of mats the agent has obtained this episode
    /// </summary>
    public float MatsCollected { get; private set; }

    /// <summary>
    /// Initialize the agent
    /// </summary>

    private void Start()
    {
        TrainingArea = transform.parent.GetComponent<TrainAreaController>();
    }


    public override void Initialize()
    {
        sceneScripts = GameObject.Find("/SceneScripts");
        stearing = sceneScripts.GetComponent<SteeringController>();
        weapons = sceneScripts.GetComponent<WeaponController>();
        thruster = sceneScripts.GetComponent<ThrusterController>();
        ship = this.transform.GetChild(0).gameObject;
        canControl = ship.GetComponent<ShipVariables>().CanControl;
        thrusterA = ship.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        thrusterB = ship.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
        
        // If not training mode, no max step, play forever
        if (!trainingMode) MaxStep = 0;
    }

    /// <summary>
    /// Reset the agent when an episode begins
    /// </summary>
    public override void OnEpisodeBegin()
    {
        if (trainingMode)
        {
            // Only reset flowers in training when there is one agent per area
            TrainingArea.Reset();
        }

        // Reset nectar obtained
        MatsCollected = 0f;

        // Zero out velocities so that movement stops before a new episode begins
        ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ship.GetComponent<Rigidbody2D>().angularVelocity = 0;

        // Default to spawning in front of a flower
        bool inFrontOfFlower = true;
        if (trainingMode)
        {
            // Spawn in front of flower 50% of the time during training
            inFrontOfFlower = UnityEngine.Random.value > .5f;
        }
    }

    /// <summary>
    /// Called when and action is received from either the player input or the neural network
    /// 
    /// vectorAction[i] represents:
    /// Index 0: move vector x (+1 = right, -1 = left)
    /// Index 1: move vector y (+1 = up, -1 = down)
    /// Index 2: move vector z (+1 = forward, -1 = backward)
    /// Index 3: pitch angle (+1 = pitch up, -1 = pitch down)
    /// Index 4: yaw angle (+1 = turn right, -1 = turn left)
    /// </summary>
    /// <param name="vectorAction">The actions to take</param>
    public void OnActionReceived(int[] vectorAction)
    {


        // Calculate movement vector
        if (vectorAction[0] > 0)
        {
            stearing.TurnLeft(ship);
        }
        else
        {
            stearing.TurnRight(ship);
        }

        if (vectorAction[1] > 0)
        {
            Transform weaponSlots = ship.transform.GetChild(0).GetChild(0);
            foreach (Transform weapon in weaponSlots)
            {
                if (weapon.childCount > 0)
                {
                    weapons.Shoot(ship, weapon.GetChild(0).gameObject);
                }
            }
        }
        if (vectorAction[2] > 0)
        {
            thruster.Accelerate(ship);
        }
    }

    /// <summary>
    /// Collect vector observations from the environment
    /// </summary>
    /// <param name="sensor">The vector sensor</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // If nearestFlower is null, observe an empty array and return early
        if (target == null)
        {
            sensor.AddObservation(new float[10]);
            return;
        }

        // Observe the agent's local rotation (4 observations)
        sensor.AddObservation(transform.localRotation.normalized);

        // Get a vector from the beak tip to the nearest flower
        Vector3 toTarget = target.transform.position - ship.transform.position;

        // Observe a normalized vector pointing to the nearest flower (3 observations)
        sensor.AddObservation(toTarget.normalized);

        // Observe a dot product that indicates whether the beak tip is in front of the flower (1 observation)
        // (+1 means that the beak tip is directly in front of the flower, -1 means directly behind)
        sensor.AddObservation(Vector3.Dot(toTarget.normalized, -target.transform.forward.normalized));


        // Observe the relative distance from the beak tip to the flower (1 observation)
        sensor.AddObservation(toTarget.magnitude);

        // 10 total observations

    }


    /// <summary>
    /// Prevent the agent from moving and taking actions
    /// </summary>
/*    public void FreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        frozen = true;
        rigidbody.Sleep();
    }*/

    /// <summary>
    /// Resume agent movement and actions
    /// </summary>

    private void UpdateTarget()
    {
        target = GameObject.Find("Alunimum Asteroid");
    }

    /// <summary>
    /// Move the agent to a safe random position (i.e. does not collide with anything)
    /// If in front of flower, also point the beak at the flower
    /// </summary>
    /// <param name="inFrontOfFlower">Whether to choose a spot in front of a flower</param>
  /*  private void MoveToSafeRandomPosition(bool inFrontOfFlower)
    {
        bool safePositionFound = false;
        int attemptsRemaining = 100; // Prevent an infinite loop
        Vector3 potentialPosition = Vector3.zero;
        Quaternion potentialRotation = new Quaternion();

        // Loop until a safe position is found or we run out of attempts
        while (!safePositionFound && attemptsRemaining > 0)
        {
            attemptsRemaining--;
            if (inFrontOfFlower)
            {
                // Pick a random flower
                Flower randomFlower = flowerArea.Flowers[UnityEngine.Random.Range(0, flowerArea.Flowers.Count)];

                // Position 10 to 20 cm in front of the flower
                float distanceFromFlower = UnityEngine.Random.Range(.1f, .2f);
                potentialPosition = randomFlower.transform.position + randomFlower.FlowerUpVector * distanceFromFlower;

                // Point beak at flower (bird's head is center of transform)
                Vector3 toFlower = randomFlower.FlowerCenterPosition - potentialPosition;
                potentialRotation = Quaternion.LookRotation(toFlower, Vector3.up);
            }
            else
            {
                // Pick a random height from the ground
                float height = UnityEngine.Random.Range(1.2f, 2.5f);

                // Pick a random radius from the center of the area
                float radius = UnityEngine.Random.Range(2f, 7f);

                // Pick a random direction rotated around the y axis
                Quaternion direction = Quaternion.Euler(0f, UnityEngine.Random.Range(-180f, 180f), 0f);

                // Combine height, radius, and direction to pick a potential position
                potentialPosition = flowerArea.transform.position + Vector3.up * height + direction * Vector3.forward * radius;

                // Choose and set random starting pitch and yaw
                float pitch = UnityEngine.Random.Range(-60f, 60f);
                float yaw = UnityEngine.Random.Range(-180f, 180f);
                potentialRotation = Quaternion.Euler(pitch, yaw, 0f);
            }

            // Check to see if the agent will collide with anything
            Collider[] colliders = Physics.OverlapSphere(potentialPosition, 0.05f);

            // Safe position has been found if no colliders are overlapped
            safePositionFound = colliders.Length == 0;
        }

        Debug.Assert(safePositionFound, "Could not find a safe position to spawn");

        // Set the position and rotation
        transform.position = potentialPosition;
        transform.rotation = potentialRotation;
    }*/

    /// <summary>
    /// Update the nearest flower to the agent
    /// </summary>
/*    private void UpdateNearestFlower()
    {
        foreach (Flower flower in flowerArea.Flowers)
        {
            if (nearestFlower == null && flower.HasNectar)
            {
                // No current nearest flower and this flower has nectar, so set to this flower
                nearestFlower = flower;
            }
            else if (flower.HasNectar)
            {
                // Calculate distance to this flower and distance to the current nearest flower
                float distanceToFlower = Vector3.Distance(flower.transform.position, beakTip.position);
                float distanceToCurrentNearestFlower = Vector3.Distance(nearestFlower.transform.position, beakTip.position);

                // If current nearest flower is empty OR this flower is closer, update the nearest flower
                if (!nearestFlower.HasNectar || distanceToFlower < distanceToCurrentNearestFlower)
                {
                    nearestFlower = flower;
                }
            }
        }
    }*/

    /// <summary>
    /// Called when the agent collides with something solid
    /// </summary>
    /// <param name="collision">The collision info</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (trainingMode && collision.collider.CompareTag("boundry"))
        {
            // Collided with the area boundary, give a negative reward
            AddReward(-.5f);
        }
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        // Draw a line from the beak tip to the nearest flower
        if (target != null)
            Debug.DrawLine(ship.transform.position, target.transform.position, Color.green);
        if (target != null)
        {
            AddReward(Vector3.Distance(ship.transform.position, target.transform.position) * -.01f);

        }
    }

    /// <summary>
    /// Called every .02 seconds
    /// </summary>
    private void FixedUpdate()
    {
        if (target == null || target.GetComponent<AsteroidController>().Health > 0)
            UpdateTarget();
    }
}
