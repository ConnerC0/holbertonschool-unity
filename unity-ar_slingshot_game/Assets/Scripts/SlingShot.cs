using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class SlingShot : MonoBehaviour
{

    private Vector2 startTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 posDifference;
    public GameObject prefab;
    public float force;
    private GameObject ball;
    private float ammoFlyTime = 1f;
    private bool ammoInFlight = false;
    public int remainingAmmo = 7;
    public GameObject manager;
    public TMP_Text ammoText;
    public GameObject slingShot;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GrabStartTouch();
            remainingAmmo--;
            ammoText.text = "Ammo: " + remainingAmmo.ToString() + "/7";
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Launch();
        }

        if (remainingAmmo <= 0)
        {
            manager.GetComponent<GameManager>().gameEnd();
            slingShot.GetComponent<SlingShot>().enabled = false;
            remainingAmmo = 7;
        }
    }
    void GrabStartTouch()
    {
        ball = Instantiate(prefab, (transform.position), transform.rotation);
        var rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        startTouchPos = Input.GetTouch(0).position;
    }

    void Launch()
    {
        currentTouchPos = Input.GetTouch(0).position;
        posDifference = currentTouchPos - startTouchPos;
        var rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(ball.transform.forward * 10, ForceMode.Impulse);
        StartCoroutine("AmmoInFlight");
    }

    IEnumerator AmmoInFlight()
    {
        yield return new WaitForSeconds(ammoFlyTime);
        Destroy(ball);
        ammoInFlight = false;
    }
}

