using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private int nb = 0;

	void OnTriggerEnter2D(Collider2D trigger){
		if (trigger.gameObject.tag == "Ground")	{
			GetComponentInParent<Player> ().setGrounded (true);
			nb++;
		}
	}

	void OnTriggerExit2D(Collider2D trigger){
		
		if (trigger.gameObject.tag == "Ground")	{
			nb--;
			if (nb <= 0) { 
				GetComponentInParent<Player> ().setGrounded (false);
				if (nb < 0) {
					nb = 0;
				}
			}
		}
	}

	public void setNb(int n){
		nb = n;
	}
}
