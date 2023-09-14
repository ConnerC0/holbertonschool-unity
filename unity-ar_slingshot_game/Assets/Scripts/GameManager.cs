using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Plane selection variables
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public ARPlane selectedPlane = null;
    private TrackableId storedId;
    private Vector2 touchPos;
    public Material selected;
    public Material unselected;
    
    //target variables
    public int totalTargets = 7;
    public GameObject targetPrefab;
    public List<GameObject> targets;
    
    //UI variables
    public GameObject inGameOptions;
    public GameObject confirmButton;
    public GameObject againButton;
    public GameObject startButton;
    public GameObject findingPlane;
    
    //ammo
    public GameObject slingShot;
    public int ammo = 7;
    
    //UI text
    int score = 0;
    public TMP_Text scoreText;
    public TMP_Text ammoText;

    void Start()
    {

    }

    void Update()
    {
        //plane selection logic gate
        if(planeManager.enabled){
            if(!getTouchPosition(out Vector2 touchPos)){
                return;
            }
            if(raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon)){
                if(selectedPlane != null)
                    selectedPlane.GetComponent<MeshRenderer>().material = unselected;
                storedId = hits[0].trackableId;
                selectedPlane = planeManager.GetPlane(storedId);
                selectedPlane.GetComponent<MeshRenderer>().material = selected;
            }
        }
    }
    
    bool getTouchPosition(out Vector2 touchPos){
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch((0)).position;
            return true;
        }
        touchPos = default;
        return false;
    }

    public void confirmPlane(){
        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackableId != storedId)
                Destroy(plane.gameObject);
        }
        planeManager.enabled = false;
        raycastManager.enabled = false;
        confirmButton.SetActive(false);
        findingPlane.SetActive(false);
        inGameOptions.SetActive(true);
        selectedPlane.GetComponent<MeshRenderer>().enabled = false;
        selectedPlane.GetComponent<LineRenderer>().enabled = false;
        spawnTargets();
        startButton.SetActive(true);
    }

    void spawnTargets()
    {
        for (int i = 0; i < totalTargets; i++)
        {
            GameObject newTarget = Instantiate(targetPrefab, selectedPlane.center, Quaternion.identity);
            var X = selectedPlane.size.x;
            var Z = selectedPlane.size.y;
            var x = Random.Range(-(X / 3 - 0.1f), X / 3 - 0.1f);
            var z = Random.Range(-(Z / 3 - 0.1f), Z / 3 - 0.1f);

            newTarget.transform.position = new Vector3(x + selectedPlane.center.x,
                newTarget.transform.position.y + 0.1f, z + selectedPlane.center.z);
            targets.Add(newTarget);
        }
    }
    public void playGame(){
        startButton.SetActive(false);
        slingShot.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
        ammoText.text = "Ammo: " + ammo.ToString() + "/7";
    }
    
    public void ResetGame(){
        score = 0;
        scoreText.text = score.ToString();
        ammo = 7;
        ammoText.text = "Ammo: " + ammo.ToString() + "/7";
        slingShot.GetComponent<SlingShot>().enabled = true;
    }

    public void gameEnd()
    {
        againButton.SetActive(true);
    }

    public void playAgain()
    {
        foreach (var target in targets)
            Destroy(target);
        targets.Clear();
        spawnTargets();
        ResetGame();
        againButton.SetActive(false);
    }
    
    public void gameRestart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void quit()
    {
        Application.Quit();
    }
}