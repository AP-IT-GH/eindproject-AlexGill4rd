using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RoombaAgentE1 : Agent
{
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5f;
    public Vector3 startingPosition; // Define starting position in the Unity Editor
    public int maxCollision = 3;
    private List<GameObject> dustObjects;

    public bool isTesting = true;

    private int collisionCount;
    private void Start()
    {
        // Initialize agent at the starting position
        this.transform.localPosition = startingPosition;

        dustObjects = new List<GameObject>();

        GameObject[] dustArray = GameObject.FindGameObjectsWithTag("Dust");
        GameObject[] bonusArray = GameObject.FindGameObjectsWithTag("Bonus");
        GameObject[] mergedArray = dustArray.Concat(bonusArray).ToArray();
        dustObjects.AddRange(mergedArray);
    }

    private void Update()
    {
        // Any update logic if needed
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = startingPosition;
        this.collisionCount = 0;
        foreach (var dust in dustObjects)
        {
            dust.SetActive(true);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.forward);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Process actions
        float forwardAmount = actions.ContinuousActions[0];
        float turnAmount = actions.ContinuousActions[1];

        // Move forward
        Vector3 controlSignal = transform.forward * forwardAmount;
        transform.Translate(controlSignal * speedMultiplier, Space.World);

        // Rotate
        transform.Rotate(Vector3.up, turnAmount * rotationMultiplier);

        // Rewards and penalties
        // Penalize small amount for each step to encourage efficiency
        AddReward(-0.001f);

        // Check for falling off platform
        if (this.transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dust"))
        {
            if (isTesting)
                other.gameObject.SetActive(false);
            SetReward(0.02f);
        }
        if (other.CompareTag("Bonus"))
        {
            if (isTesting)
                other.gameObject.SetActive(false);
            SetReward(0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            collisionCount++;
            AddReward(-1.5f); // Larger penalty for collisions with obstacles

            if (collisionCount >= maxCollision) EndEpisode();
        }
    }
}