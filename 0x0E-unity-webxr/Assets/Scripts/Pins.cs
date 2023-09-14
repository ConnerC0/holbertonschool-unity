using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            ScoreManager.Instance.IncrementScore(); // Increment the score
            Destroy(gameObject); // Destroy the pin
        }
    }
}
