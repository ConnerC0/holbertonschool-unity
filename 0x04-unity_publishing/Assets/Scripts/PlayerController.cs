using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private int score = 0;
	public int health = 5;
	
	public Rigidbody rb;
	public float speed = 10f;

	public Text scoreText;
	public Text healthText;
	public Image winloseBG;
	public Text winloseText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ( Input.GetKey("a"))
		{
			rb.AddForce(-speed * Time.deltaTime, 0, 0);
		}
		if ( Input.GetKey("d"))
		{
			rb.AddForce(speed * Time.deltaTime, 0, 0);
		}
		if ( Input.GetKey("w"))
		{
			rb.AddForce(0, 0, speed * Time.deltaTime);
		}
		if ( Input.GetKey("s"))
		{
			rb.AddForce(0, 0, -speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Pickup")
		{
			score += 1;
			SetScoreText();
			//Debug.Log("Score: " + score);
			Destroy(other.gameObject);
		}
		if (other.tag == "Trap")
		{
			health -= 1;
			SetHealthText();
			//Debug.Log("Health: " + health);
		}
		if (other.tag == "Goal")
		{
			YouWinText();
			StartCoroutine(LoadScene(3.0f));
			//Debug.Log("You Win!");
		}
	}

	void Update () {
		if (health == 0)
		{
			YouLoseText();
			StartCoroutine(LoadScene(3.0f));
			//Debug.Log("Game Over!");
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	
	void SetScoreText(){
		scoreText.text = "Score: " + score.ToString();
	}

	void SetHealthText(){
		healthText.text = "Health: " + health.ToString();
	}

	void YouWinText(){
		winloseBG.gameObject.SetActive(true);
		winloseBG.color = Color.green;
		winloseText.text = "You Win!";
		winloseText.color = Color.black;
	}

	void YouLoseText(){
		winloseBG.gameObject.SetActive(true);
		winloseBG.color = Color.red;
		winloseText.text = "You Lose!";
		winloseText.color = Color.black;
	}

	IEnumerator LoadScene(float seconds){
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
