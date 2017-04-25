using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_fin_jeu : MonoBehaviour {

	[SerializeField]
	private Canvas CanvasGagne;
	private ChangeVisibilityCanvas changeCanvas;

	void Start() {
		CanvasGagne = GameObject.Find ("CanvasWin").GetComponent<Canvas> ();

	}
	void OnTriggerEnter2D(Collider2D other) {
		CanvasGagne.enabled = true;
		Time.timeScale = 0;
	}
}
