using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_flag : MonoBehaviour {

	public bool actif = false;
	IEnumerator OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Animator anim = GetComponent<Animator> ();
			actif = true;
			anim.SetBool ("flagActived", true);
			yield return new WaitForSeconds (0);
		}
	}
}
