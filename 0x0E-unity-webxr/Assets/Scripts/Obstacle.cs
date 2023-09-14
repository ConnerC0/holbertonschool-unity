using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BallInteraction ballInteraction;

    public void OnCollisionEnter(Collision collision)
    {
        if (ballInteraction != null && collision.gameObject.CompareTag("BowlingBall"))
        {
            Destroy(gameObject);
            ballInteraction.RespawnBall();
        }
    }
}
