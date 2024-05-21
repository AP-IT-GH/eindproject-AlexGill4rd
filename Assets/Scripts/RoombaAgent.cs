using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RoombaAgent : Agent

{
    // Start is called before the first frame update

    public Transform Target;
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnEpisodeBegin()
    {
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Acties, size = 2
        Vector3 controlSignal = Vector3.zero; controlSignal.z = actions.ContinuousActions[0]; transform.Translate(controlSignal * speedMultiplier);


        transform.Rotate(0.0f, rotationMultiplier * actions.ContinuousActions[1], 0.0f);

        // Beloningen
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // target bereikt
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Van het platform gevallen?
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }


}
