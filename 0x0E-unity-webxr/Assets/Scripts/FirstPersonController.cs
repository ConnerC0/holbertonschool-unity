using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public bool canMove = true;
    public BallInteraction ballInteraction;

    private float xAxisClamp = 0.0f;
    public Camera playerCamera;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateCamera();
        MovePlayer();
    }

    public bool IsMKInputActive() 
    {
        // Check for mouse movement or any key pressed
        return Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.anyKey;
    }

    void RotateCamera()
    {
        if (ballInteraction.canControlCamera)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the player object around the Y-axis for horizontal mouse movement
            transform.Rotate(0, mouseX, 0); 

            // Clamp the up and down camera rotation
            xAxisClamp += mouseY;
            xAxisClamp = Mathf.Clamp(xAxisClamp, -90.0f, 90.0f);
    
            // Rotate the playerCamera for vertical mouse movement
            playerCamera.transform.localRotation = Quaternion.Euler(-xAxisClamp, 0, 0);
        }
    }

    void MovePlayer()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            Vector3 moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveZ));  // Adjusted this line to use the transform of the Player
            transform.position += moveDirection; // Adjusted this line to use the transform of the Player
        }
    }
}