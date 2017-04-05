using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpEffects : MonoBehaviour {

	private int tempsEffet = 10;
	private string nomPowerUp;
	private PowerUpManager powerUpManager;
	private Player thePlayer;

	void Start() {
		powerUpManager = FindObjectOfType<PowerUpManager> ();
		thePlayer = FindObjectOfType<Player> ();
	}

	public void OnPickUp(string nom_pw) {
		OnBeforeDelay (nom_pw);
		StartCoroutine(powerUpManager.ProcessPowerUp(nom_pw,tempsEffet));
	}

	private void Disable() {
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	private void Remove(string nom) {
		Destroy (GameObject.Find (nom));
	}

	public void OnTriggerEnter2D(Collider2D other) {
		nomPowerUp = this.name;
		int nbVie = GameObject.Find ("Player").GetComponent<Player> ().getNbVie ();
		if (other.name.Equals ("Player")) {
			switch (nomPowerUp) {
			case "VieBonus":
				GameObject.Find ("Player").GetComponent<Player> ().setNbVie (nbVie + 1);
				Remove (nomPowerUp);
				break;
			case "VieMalus":
				GameObject.Find ("Player").GetComponent<Player> ().setNbVie (nbVie - 1);
				Remove (nomPowerUp);
				break;
			default:
				break;
			}
			OnPickUp (nomPowerUp);
			Disable ();
		}
	}

	public void OnBeforeDelay(string nomPowerUp) {		
		switch (nomPowerUp) {
		case "JumpBoost":
			GameObject.Find ("Player").GetComponent<Player> ().setJmpHeight (9);
			break;
		case "Inversement":
			thePlayer.powerUpInversementActif = true;
			break;
		case "Invincibilite":
			thePlayer.powerUpInvincibleActif = true;
			break;
		default:
			break;
		}
	}

	public void OnAfterDelay(string nomPowerUp) {
		switch (nomPowerUp) {
		case "JumpBoost":
			GameObject.Find ("Player").GetComponent<Player> ().setJmpHeight (7);
			break;
		case "Inversement":
			thePlayer.powerUpInversementActif = false;
			break;
		case "Invincibilite":
			thePlayer.powerUpInvincibleActif = false;
			break;
		default:
			break;
		}
		Remove (nomPowerUp);
	}

}

