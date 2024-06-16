using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RoombaAgent : Agent
{
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5f;
    public Vector3 startingPosition; // Define starting position in the Unity Editor
    public float detectionRadius = 2f; // Radius for detecting nearby obstacles
    public int maxCollisions = 3; // Maximum allowed collisions

    private List<GameObject> dustObjects;
    private List<Vector3> initialDustPositions;
    private int collisionCount;

    private void Start()
    {
        // Initialize agent at the starting position
        this.transform.localPosition = startingPosition;

        // Find all dust objects and store them in a list
        dustObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Dust"));

        // Store initial positions of dust objects for resetting
        initialDustPositions = new List<Vector3>();
        foreach (var dust in dustObjects)
        {
            initialDustPositions.Add(dust.transform.localPosition);
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position
        this.transform.localPosition = startingPosition;

        // Reset dust objects to their initial positions and activate them
        for (int i = 0; i < dustObjects.Count; i++)
        {
            dustObjects[i].transform.localPosition = initialDustPositions[i];
            dustObjects[i].SetActive(true);
        }

        // Reset collision count
        collisionCount = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations about the agent's position and orientation
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.forward);

        // Collect observations about nearby obstacles
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Obstacle"))
            {
                Vector3 directionToObstacle = hitCollider.transform.position - this.transform.position;
                sensor.AddObservation(directionToObstacle.normalized);
                sensor.AddObservation(directionToObstacle.magnitude / detectionRadius);
            }
        }
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
            other.gameObject.SetActive(false);
            AddReward(0.02f); // Increased reward for cleaning
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            collisionCount++;
            AddReward(-0.5f); // Larger penalty for collisions with obstacles

            if (collisionCount >= maxCollisions)
            {
                SetReward(-1.0f);
                EndEpisode();
            }
        }
    }
}
