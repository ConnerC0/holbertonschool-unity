using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource footStepsGrass;
    public AudioSource footStepsRock;
    public AudioSource landingGrass;
    RaycastHit hit;


    public void FootstepsSound()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {

            if (hit.collider.tag == "grass")
            {
                print("walking");
                footStepsGrass.Play();
            }
            if (hit.collider.gameObject.CompareTag("rock"))
            {
                footStepsRock.Play();
            }
        }
    }

    public void FallingSound()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {

            if (hit.collider.gameObject.CompareTag("grass"))
            {
                print("landed");
                landingGrass.Play();
            }
        }
    }

}
