using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToWaypoint : MonoBehaviour {

	public List<GameObject> wayPoints;
	public GameObject pirate, mapMenu, encounterMessage;
	public bool moveByName;
	public float speed;
	public string nextSceneName;

	void Start(){
		encounterMessage.SetActive(false);
		mapMenu.SetActive(false);
	}
	void FixedUpdate()
	{
		if(mapMenu.activeInHierarchy){
			if(wayPoints.Count != 0){
				pirate.transform.position = Vector2.MoveTowards(pirate.transform.position, wayPoints[0].transform.position, speed * Time.deltaTime);
				if(pirate.transform.position == wayPoints[0].transform.position){
					wayPoints.RemoveAt(0);
				}
			}
			else{
				FindObjectOfType<AudioManager>().Play("Event");
				encounterMessage.SetActive(true);
				mapMenu.SetActive(false);
				Invoke("MoveScene", 2.5f);
			}
		}
	}
	public void MoveScene(){
		if(moveByName){
			SceneManager.LoadScene(nextSceneName);
		}
		else if(GlobalVars.difficulty == "Easy"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

	}
	public void StartMap(){
        mapMenu.SetActive(true);
    }
}
