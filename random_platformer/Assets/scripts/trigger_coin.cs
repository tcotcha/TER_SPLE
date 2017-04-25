using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_coin : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Player"){
			trigger.gameObject.GetComponent<Player> ().Score += 5;
			Destroy (transform.gameObject);
		}
	}
}
