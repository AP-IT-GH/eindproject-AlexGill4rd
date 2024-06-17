using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RoombaAgent : Agent
{
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5f;
    public Vector3 startingPosition;

    private List<GameObject> dustObjects;

    private int collisionCount;
    private void Start()
    {
        // Initialize agent at the starting position
        this.transform.localPosition = startingPosition;

        // Find all dust objects and store them in a list
        dustObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Dust"));
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
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.forward);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = actions.ContinuousActions[0];
        float turnAmount = actions.ContinuousActions[1];

        Vector3 controlSignal = transform.forward * forwardAmount;
        transform.Translate(controlSignal * speedMultiplier, Space.World);

        transform.Rotate(Vector3.up, turnAmount * rotationMultiplier);

        // Rewards and penalties
        // Penalize small amount for each step to encourage efficiency
        AddReward(-0.001f);

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
            Destroy(other.gameObject);
            SetReward(0.02f);
        }
        if (other.CompareTag("Bonus"))
        {
            Destroy(other.gameObject);
            SetReward(1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}