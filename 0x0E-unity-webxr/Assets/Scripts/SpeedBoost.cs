using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostMultiplier = 6f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BowlingBall"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();
            Vector3 forwardVelocity = Vector3.forward * ballRigidbody.velocity.z * boostMultiplier;
            ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x, ballRigidbody.velocity.y, forwardVelocity.z);
        }
    }
}
