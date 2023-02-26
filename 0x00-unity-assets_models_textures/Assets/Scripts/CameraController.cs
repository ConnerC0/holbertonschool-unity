using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cam;

    void Start()
    {
        cam.m_XAxis.m_MaxSpeed = 0;
        cam.m_YAxis.m_MaxSpeed = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cam.m_XAxis.m_MaxSpeed = 300;
            cam.m_YAxis.m_MaxSpeed = 2;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cam.m_XAxis.m_MaxSpeed = 0;
            cam.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
