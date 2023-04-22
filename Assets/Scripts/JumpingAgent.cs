using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class JumpingAgent : Agent
{
    public float jumpForce = 0;
    private Rigidbody rigidBody;
    public GameObject obstacle;

    public override void Initialize()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        obstacle.gameObject.transform.localPosition = new Vector3(-4.2f, 0.3f, 0);
        Obstacle.GenRandom();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.ContinuousActions[0] == 1)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
        }
    }
    private void FixedUpdate()
    {
        if (Obstacle.hit)
        {
            AddReward(1.0f);
            EndEpisode();
            Obstacle.hit = false;
        }
        if (rigidBody.gameObject.transform.localPosition.y <= 0.5f)
        {
            AddReward(0.01f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") == true)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
    }
}
