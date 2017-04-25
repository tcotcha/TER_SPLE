using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeVisibilityCanvas : MonoBehaviour {

	private Canvas CanvasWin;
	private Canvas CanvasLoose;

	void Start() {
		CanvasWin = GameObject.Find("CanvasWin").GetComponent<Canvas>();
		CanvasLoose = GameObject.Find("CanvasLoose").GetComponent<Canvas>();
		CanvasWin.enabled = false;
		CanvasLoose.enabled = false;
	}

	public void OnRejouer() {
		SceneManager.LoadScene("main_scene");
		Time.timeScale = 1;
	}

	public void OnQuitter(){
		SceneManager.LoadScene("menu");
		Time.timeScale = 1;
	}
		
}
