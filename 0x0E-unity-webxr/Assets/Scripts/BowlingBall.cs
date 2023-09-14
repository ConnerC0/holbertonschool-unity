using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public BallInteraction ballInteraction;

    void OnCollisionEnter(Collision collision)
    {
        ballInteraction.OnBallCollision(collision);
    }
}
