using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private int nb = 0;

	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Ground" && nb == 0)	{
			GetComponentInParent<Player> ().setGrounded (true);
		}
		nb++;
	}

	void OnTriggerExit2D(Collider2D trigger){
		nb--;
		if (trigger.gameObject.tag == "Ground" && nb == 0)	{
			GetComponentInParent<Player> ().setGrounded (false);
		}
	}
}
