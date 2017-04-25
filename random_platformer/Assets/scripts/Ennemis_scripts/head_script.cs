using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_script : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Player"){
			trigger.gameObject.GetComponent<Player> ().Score += 20;
			Destroy (transform.parent.gameObject);
		}
	}
}
