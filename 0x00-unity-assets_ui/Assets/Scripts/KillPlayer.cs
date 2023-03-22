using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    public CharacterController controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controller.enabled = false;
            player.transform.position = respawnPoint.position;
            controller.enabled = true;
        }
    }
}
