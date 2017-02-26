using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D trigger){

		if (trigger.gameObject.tag == "Ground")	{
			GetComponentInParent<Player> ().setGrounded (true);
		}
	}

	void OnTriggerStay2D(Collider2D trigger){
		
		if (trigger.gameObject.tag == "Ground")	{
			GetComponentInParent<Player> ().setGrounded (true);
		}
	}

	void OnTriggerExit2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Ground")	{
			GetComponentInParent<Player> ().setGrounded (false);
		}
	}
}
