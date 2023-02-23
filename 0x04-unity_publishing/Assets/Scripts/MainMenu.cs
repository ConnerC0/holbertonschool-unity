using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Material trapMat;
	public Material goalMat;
	public Toggle colorblindMode;
	
	public void PlayMaze(){
		colorBlindChecker();
		SceneManager.LoadScene("Maze");
	}

	public void QuitMaze(){
		Debug.Log("Quit Game");  
        Application.Quit();
	}

	void colorBlindChecker(){
		if (colorblindMode != null && colorblindMode.isOn)
    	{
			trapMat.color = new Color32(255, 112, 0, 1);
			goalMat.color = Color.blue;
    	}
		else if (!colorblindMode.isOn)
		{
			trapMat.color = Color.red;
			goalMat.color = Color.green;
		}
	}
}
