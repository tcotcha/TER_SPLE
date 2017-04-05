using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour {

	[SerializeField]
	private float fillAmount;
	
	[SerializeField]
	private Image Mask_Inversement;
	[SerializeField]
	private Image Mask_JumpBoost;
	[SerializeField]
	private Image Mask_Invincibilite; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleUI ();
	}

	private void HandleUI() {
		Mask_Inversement.fillAmount = Map(13,0,60,0,1);
		Mask_JumpBoost.fillAmount = Map(30,0,60,0,1);
		Mask_Invincibilite.fillAmount = Map(53,0,60,0,1);
	}
	
	private float Map(float value, float inMin, float inMax, float outMin, float outMax) {
		return ((value - inMin) * (outMax - outMin)) / ((inMax - inMin) + outMin); 
	}
}
