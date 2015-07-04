using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public GameObject playscreenUI;
	public GameObject gameoverUI;
	public GameObject gameoverUI2;
	public GameObject gameoverUI4;
	public GameObject restart;
	

	public bool gameOver;

	// Use this for initialization
	void Start () {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Player").GetComponent<PlayerController> ().gameOver) {
			playscreenUI.GetComponent<Text>().enabled = false;
			gameoverUI.GetComponent<Text>().enabled = true;
			gameoverUI2.GetComponent<Text>().enabled = true;
			gameoverUI4.GetComponent<Text>().enabled = true;
			restart.GetComponent<Text>().enabled = true;

			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(0);
			}

		}
	}
}
