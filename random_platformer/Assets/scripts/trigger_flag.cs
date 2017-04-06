using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_flag : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D other) {
			Animator anim = GetComponent<Animator> ();
			anim.SetBool ("flagActived", true);
			yield return new WaitForSeconds (0);
	}
}
