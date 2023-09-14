using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public BallInteraction ballInteraction;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            ballInteraction.RespawnBall();
        }
    }
}
