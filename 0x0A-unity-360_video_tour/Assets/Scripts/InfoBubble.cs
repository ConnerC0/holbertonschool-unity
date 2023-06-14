using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBubble : MonoBehaviour
{
    public GameObject InfoTextBG;
    
    public void InfoBubbleClick()
    {
        if (InfoTextBG.activeSelf == false)
            InfoTextBG.SetActive(true);
        else
        {
            InfoTextBG.SetActive(false);
        }
    }
}
