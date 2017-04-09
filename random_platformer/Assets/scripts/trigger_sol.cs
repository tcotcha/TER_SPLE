using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_sol : MonoBehaviour {
	public GameObject player;
	public GameObject box;

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			player.GetComponent<Player> ().die ("trigger_sol");
		}
	}
}
