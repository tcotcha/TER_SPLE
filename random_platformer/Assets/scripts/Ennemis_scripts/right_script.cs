using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_script : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Ground" && GetComponentInParent<ennemis_script> ().getDirection () == 1) {
			GetComponentInParent<ennemis_script> ().setDirection (-1);
		} else if (trigger.gameObject.tag == "Player"){
			trigger.gameObject.GetComponent<Player> ().die ();
		}
	}
}
