using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement_platform : MonoBehaviour {

	public float x;
	public float y;
	public float fin_x;
	public float fin_y;

	private Vector3 posDeb;
	private Vector3 posFin;
	private Vector3 posSuivante; 

	private float speed;

	private Transform t;
	private Transform tFin;

	void Start () {
		speed = 1;
		t = new GameObject ().transform;
		tFin = new GameObject ().transform;
		t.localPosition = new Vector3 (x,y,0);
		tFin.localPosition = new Vector3 (fin_x,fin_y,0);
		print (t.localPosition);
		print (tFin.localPosition);
		posDeb = t.localPosition;
		posFin = tFin.localPosition;
		posSuivante = posFin;
	}
	
	void Update () {
		move ();
	}

	private void move(){
		transform.position = Vector3.MoveTowards (transform.position, posSuivante, speed * Time.deltaTime);

		if (Vector3.Distance (transform.position, posSuivante) < 0.1f) {
			changeDestination ();
		}
	}

	private void changeDestination(){
		if (posSuivante.Equals (posFin)) {
			posSuivante = posDeb;
		} else {
			posSuivante = posFin;
		}
	}
}
