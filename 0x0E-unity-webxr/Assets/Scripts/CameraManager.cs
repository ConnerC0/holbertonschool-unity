using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    public Camera MKCamera;
    public GameObject WebXRCameraSet;
    private Transform activeCameraTransform;

    public BallInteraction ballInteraction;

    private bool isWebXRActive;

    void Start()
    {
        isWebXRActive = false;
        CheckForWebXRDevice();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // You can change this key to your preference
        {
            isWebXRActive = !isWebXRActive;
            SwitchCamera();
        }
    }

    private void CheckForWebXRDevice()
    {
        if (XRSettings.isDeviceActive)
        {
            isWebXRActive = true;
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        MKCamera.enabled = !isWebXRActive;
        WebXRCameraSet.SetActive(isWebXRActive);

        // Assign the active camera transform to your player script
        activeCameraTransform = isWebXRActive ? WebXRCameraSet.GetComponentInChildren<Camera>().transform : MKCamera.transform;
        // YourPlayerScriptInstance.cameraTransform = activeCameraTransform; // Replace with your player script instance and variable
        ballInteraction.cameraTransform = activeCameraTransform;
    }
}
