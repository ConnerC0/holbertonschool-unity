using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform ballParent;
    public Transform targetView;
    public float throwForce = 10.0f;
    public FirstPersonController firstPersonController;
    public Crosshair crosshair;
    public float gutterSpeedMultiplier = 2.0f;
    private float panProgress = -1f;
    public GameObject obstaclePrefab;
    public Transform leftLaneBoundary;
    public Transform rightLaneBoundary;
    public Transform obstacleEndBoundary;
    public GameObject endOfLane;
    public GameObject[] ballPrefabs;
    public int maxObstacles = 10;

    public GameObject heldBall;
    private bool isHoldingBall = false;
    public bool canControlCamera = true;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private Vector3 initialBallPosition;
    private Vector3 initialPlayerPosition;
    private bool canControlThrownBall = true;
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    private bool forceCameraReset = false;

    void Start()
    {
        initialCameraPosition = cameraTransform.position;
        initialCameraRotation = cameraTransform.rotation;
        initialPlayerPosition = firstPersonController.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("BowlingBall"))
                {
                    heldBall = hit.transform.gameObject;
                    initialBallPosition = heldBall.transform.position;
                    heldBall.GetComponent<Rigidbody>().isKinematic = true;
                    heldBall.transform.SetParent(cameraTransform);
                    isHoldingBall = true;
                }
            }
        }

        if (isHoldingBall)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            heldBall.transform.localPosition += new Vector3(horizontalInput * Time.deltaTime, 0, 0);

            if (Input.GetMouseButtonUp(0))
            {
                heldBall.GetComponent<Rigidbody>().isKinematic = false;
                heldBall.GetComponent<Rigidbody>().velocity = cameraTransform.forward * throwForce;
                heldBall.transform.SetParent(ballParent);
                isHoldingBall = false;
                crosshair.isVisible = false;
                panProgress = 0f;
                canControlThrownBall = true;
            }
        }
        
        if (!isHoldingBall)
        {
            MoveBall();
        }
        
        if (panProgress >= 0f)
        {
            PanCamera();
        }
    }

    void PanCamera()
    {
        if (forceCameraReset && panProgress < 1f)
        {
            panProgress = 0.99f; // Nearly complete the pan to get to the final state
        }

        firstPersonController.canMove = false;
        canControlCamera = false;
        panProgress += Time.deltaTime;
        float panDuration = 1f;

        if (panProgress < panDuration)
        {
            cameraTransform.position = Vector3.Lerp(initialCameraPosition, targetView.position, panProgress / panDuration);
            cameraTransform.rotation = Quaternion.Slerp(initialCameraRotation, targetView.rotation, panProgress / panDuration);
        }
        else
        {
            panProgress = -1f;
            forceCameraReset = false; // Reset the flag at the end of the pan.
        }
    }

    void ResetCamera()
    {
        firstPersonController.transform.position = initialPlayerPosition;
        cameraTransform.position = initialCameraPosition;
        cameraTransform.rotation = initialCameraRotation;
        canControlCamera = true;
        firstPersonController.canMove = true;
        panProgress = -1f;
        forceCameraReset = false;
    }

    public void OnBallReachedEndOfLane()
    {
        if (!isHoldingBall)
        {
            RespawnBall();
        }
    }

    void MoveBall()
    {
        if (heldBall != null && canControlThrownBall)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            heldBall.transform.position += new Vector3(horizontalInput * Time.deltaTime, 0, 0);
        }
    }

    public void OnBallCollidedWithGutter(GameObject ball)
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.velocity = new Vector3(0, 0, ballRigidbody.velocity.z * gutterSpeedMultiplier);
        ballRigidbody.angularVelocity = Vector3.zero;
        canControlThrownBall = false;
    }

    public void OnBallCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lane"))
        {
            SpawnObstacle();
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            RespawnBall();
        }
    }

    void SpawnObstacle()
    {
        if (spawnedObstacles.Count >= maxObstacles)
        {
            return;
        }

        float laneWidth = rightLaneBoundary.position.x - leftLaneBoundary.position.x;
        Vector3 spawnPosition;
        bool validPosition = false;
        int maxAttempts = 100;
        int attempts = 0;

        do
        {
            float spawnX = Random.Range(leftLaneBoundary.position.x + laneWidth * 0.25f, rightLaneBoundary.position.x - laneWidth * 0.25f);
            float spawnZ = Random.Range(heldBall.transform.position.z + 1.0f, obstacleEndBoundary.transform.position.z);
            spawnPosition = new Vector3(spawnX, heldBall.transform.position.y, spawnZ);

            Vector3 halfExtents = obstaclePrefab.transform.localScale / 2;
            Collider[] colliders = Physics.OverlapBox(spawnPosition, halfExtents, Quaternion.identity);
            validPosition = true;

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Obstacle"))
                {
                    validPosition = false;
                    break;
                }
            }

            attempts++;
        } while (!validPosition && attempts < maxAttempts);

        if (validPosition)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            newObstacle.tag = "Obstacle";
            
            Obstacle obstacleComponent = newObstacle.GetComponent<Obstacle>();
            if (obstacleComponent != null)
            {
                obstacleComponent.ballInteraction = this;
            }
            spawnedObstacles.Add(newObstacle);
        }
    }

    public void RespawnBall()
    {
        if (heldBall != null)
        {
            Destroy(heldBall);
        }

        // Select a ball prefab from the ballPrefabs array
        GameObject selectedBallPrefab = heldBall;

        heldBall = Instantiate(selectedBallPrefab, initialBallPosition, Quaternion.identity);
        crosshair.isVisible = true;
        forceCameraReset = true;
        heldBall = null;
        ResetCamera();
    }
}
