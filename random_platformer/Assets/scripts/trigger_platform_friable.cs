using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_platform_friable : MonoBehaviour {

	public GameObject platform;

	IEnumerator OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "PlayerFoot" && (GameObject.Find("Player").GetComponent<Player>().getGrounded())){
			Animator anim = GetComponentInParent<Animator> ();
			anim.SetBool ("friableGrounded", true);
			if (transform.parent.name != "platform") {
				transform.parent.localScale = new Vector3 (1.4375f, 1.425f, 1);
			}
			yield return new WaitForSeconds (1);
			platform.SetActive (false);
			GameObject.Find("Player").GetComponentInChildren<GroundCheck>().setNb (0);
		}
	}
}
