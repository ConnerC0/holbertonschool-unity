using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutter : MonoBehaviour
{
    public BallInteraction ballInteraction;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BowlingBall"))
        {
            ballInteraction.OnBallCollidedWithGutter(other.gameObject);
        }
    }
}
