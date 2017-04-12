using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpEffects : MonoBehaviour {

	private int tempsEffet = 10;
	private string nomPowerUp;
	private Player thePlayer;
	/*private static IEnumerator COInversement;
	private static IEnumerator COJumpBoost;
	private static IEnumerator COInvincibilite;*/

	private static Coroutine COInversement;
	private static Coroutine COJumpBoost;
	private static Coroutine COInvincibilite;

	private bool _triggered = false;

	void Start() {
		thePlayer = FindObjectOfType<Player> ();
		/*COJumpBoost = ProcessPowerUpJumpBoost ();
		COInversement = ProcessPowerUpInversement ();
		COInvincibilite = ProcessPowerUpInvincibilite ();*/
	}


	/*
	 * Je laisse les commentaires car on ne sait jamais, mais la solution que je propose fonctionne
	 * Unity lève juste une exeption, mais c'est innofenssif, c'est un bug unity qui est réglé en 5.6
	 * Unity 5.6 :  Web: Fixed case of harmless 'Coroutine continue failure' error being thrown when stopping a WWW coroutine.
	 */
	public void OnPickUp(string nom_pw) {
		OnBeforeDelay (nom_pw);
		thePlayer.ResetEffect(nom_pw);
		switch (nom_pw) {
		case "JumpBoost":			
			/*StopCoroutine ("COJumpBoost");
			StartCoroutine (COJumpBoost);*/
			if (COJumpBoost != null) { 
				StopCoroutine (COJumpBoost);
			}
			COJumpBoost = StartCoroutine (ProcessPowerUpJumpBoost ());
			break;
		case "Inversement":			
			/*StopCoroutine ("COInversement");
			StartCoroutine (COInversement);*/
			if (COInversement != null) { 
				StopCoroutine(COInversement); 
			}
			COInversement = StartCoroutine(ProcessPowerUpInversement());
			break;
		case "Invincibilite":
			/*StopCoroutine ("COInvincibilite");
			StartCoroutine (COInvincibilite);*/
			if (COInvincibilite != null) {
				StopCoroutine (COInvincibilite);
			}
			COInvincibilite = StartCoroutine (ProcessPowerUpInvincibilite ());
			break;
		default:
			break;
		}
	}

	private void Disable() {
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	private void Remove(string nom) {
		Destroy (GameObject.Find (nom));
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (!_triggered) {
			_triggered = true;
			nomPowerUp = this.name;
			if (other.name.Equals ("Player")) {
				switch (nomPowerUp) {
				case "VieBonus":
					thePlayer.setNbVie (thePlayer.getNbVie () + 1);
					Remove (nomPowerUp);
					break;
				case "VieMalus":
					thePlayer.setNbVie (thePlayer.getNbVie () - 1);
					Remove (nomPowerUp);
					break;
				default:
					OnPickUp (nomPowerUp);
					break;
				}
				Disable ();
			}
		}
		_triggered = false;
	}

	public void OnBeforeDelay(string nomPowerUp) {		
		switch (nomPowerUp) {
		case "JumpBoost":
			thePlayer.setPowerUpJumpBoostActif (true);
			break;
		case "Inversement":
			thePlayer.setPowerUpInversementActif (true);
			break;
		case "Invincibilite":
			thePlayer.setPowerUpInvincibleActif (true);
			break;
		default:
			break;
		}
	}

	public void OnAfterDelay(string nomPowerUp) {
		switch (nomPowerUp) {
		case "JumpBoost":
			thePlayer.setPowerUpJumpBoostActif (false);
			break;
		case "Inversement":
			thePlayer.setPowerUpInversementActif (false);
			break;
		case "Invincibilite":
			thePlayer.setPowerUpInvincibleActif (false);
			break;
		default:
			break;
		}
		Remove (nomPowerUp);
	}

	void ProcessPowerUpWhile(string nom_pw, float temps) {
		switch (nom_pw) {
		case "JumpBoost":
			//while(
			break;
		case "Inversement":
			thePlayer.setPowerUpInversementActif (false);
			break;
		case "Invincibilite":
			thePlayer.setPowerUpInvincibleActif (false);
			break;
		default:
			break;
		}
	}

	public IEnumerator ProcessPowerUp(string nom_pw, float temps) {
		yield return new WaitForSeconds (temps);
		OnAfterDelay (nom_pw);
	}
}

