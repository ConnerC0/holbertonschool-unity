using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    private Rect crosshairRect;
    public bool isVisible = true;

    void Start()
    {
        float crosshairSize = crosshairTexture.width / 2;
        crosshairRect = new Rect((Screen.width - crosshairSize) / 2, (Screen.height - crosshairSize) / 2, crosshairSize, crosshairSize);
    }

    void OnGUI()
    {
        if (isVisible)
        {
            GUI.DrawTexture(crosshairRect, crosshairTexture);
        }
    }
}
