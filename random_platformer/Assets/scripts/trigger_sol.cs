using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_sol : MonoBehaviour {
	public GameObject player;
	public GameObject box;
	IEnumerator OnTriggerEnter2D(Collider2D other) {
		box.SetActive (true);
		player.transform.localPosition = new Vector3 (0, 8, -0.01f);
		yield return new WaitForSeconds (0.6f);
		box.SetActive (false);
		player.GetComponentInChildren<GroundCheck>().setNb (0);
	}
}
