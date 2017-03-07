using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class menu_interaction : MonoBehaviour {

	public Button button_start;
	public Button button_exit;
	public Button button_easy;
	public Button button_medium;
	public Button button_hard;
	public GameObject cadre;
	private int positionNiveau;

	void Start(){
		choixNiveau (1);
		positionNiveau = 1;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			exitGame ();
		if (Input.GetKeyDown (KeyCode.Return))
			startGame ();
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (positionNiveau > 1){
				positionNiveau--;
				choixNiveau (positionNiveau);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (positionNiveau < 3){
				positionNiveau++;
				choixNiveau (positionNiveau);
			}
		}
	}

	public void startGame(){
		SceneManager.LoadScene("main_scene");
	}

	public void exitGame(){
		Application.Quit();
	}

	public void choixNiveau(int position){
		Vector3 positionCadre = cadre.transform.localPosition;
		switch(position){
		case 1:
			positionCadre.y = 42.5f;
			PlayerPrefs.SetString("Player Level", "easy_level");
			print (PlayerPrefs.GetString("Player Level"));
			break;
		case 2:
			positionCadre.y = -42.5f;
			PlayerPrefs.SetString("Player Level", "medium_level");
			print (PlayerPrefs.GetString("Player Level"));
			break;
		case 3:
			positionCadre.y = -122.4f;
			PlayerPrefs.SetString("Player Level", "hard_level");
			print (PlayerPrefs.GetString("Player Level"));
			break;
		default:
			break;
		}
		cadre.transform.localPosition = positionCadre;
	}
}
