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
    public Vector3 startingPosition;
    public int maxCollision = 3;
    private List<GameObject> dustObjects;

    public bool isTesting = true;

    private bool started = false;
    private int collisionCount;
    private void Start()
    {
        this.transform.localPosition = startingPosition;

        dustObjects = new List<GameObject>();

        GameObject[] dustArray = GameObject.FindGameObjectsWithTag("Dust");
        GameObject[] bonusArray = GameObject.FindGameObjectsWithTag("Bonus");
        GameObject[] mergedArray = dustArray.Concat(bonusArray).ToArray();
        dustObjects.AddRange(mergedArray);

        GameManager.instance.onRoundStarted.AddListener(OnRoundStarted);
        GameManager.instance.onRoundEnded.AddListener(OnRoundEnded);
    }

    private void Update()
    {
    }

    public void OnRoundStarted()
    {
        this.started = true;
    }
    public void OnRoundEnded()
    {
        this.started = false;
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
        if (!started) return;
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
            other.gameObject.SetActive(false);
            SetReward(0.05f);
        }
        if (other.CompareTag("Bonus"))
        {
            other.gameObject.SetActive(false);
            SetReward(1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            collisionCount++;
            AddReward(-1.5f);


            if (collisionCount >= maxCollision && isTesting) EndEpisode();
        }
    }
}
