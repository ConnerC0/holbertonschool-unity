using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoHandler : MonoBehaviour
{
    private GameObject manager;
    public List<GameObject> targets;
    private void Awake() {
        manager = GameObject.Find("GameManager");
        targets = manager.GetComponent<GameManager>().targets;

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target"){
            targets.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            var scoreKeeper = GameObject.Find("inGameOptions");
            scoreKeeper.GetComponent<SlingShot>().Score();
        }
    }
}
