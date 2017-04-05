using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PowerUpManager : MonoBehaviour {

	private PowerUpEffects pwE;

	public IEnumerator ProcessPowerUp(string nom_pw, float temps) {
		pwE = FindObjectOfType<PowerUpEffects> ();
		yield return new WaitForSeconds (temps);
		pwE.OnAfterDelay (nom_pw);
	}
}
