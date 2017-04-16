using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat {

	[SerializeField]
	private PowerUpUI pwUI;

	private float maxVal = 10;
	private float currentVal = 10;

	public float CurrentVal
	{
		get { return currentVal;}
		set
		{ 
			this.currentVal = value; 
			pwUI.Value = currentVal;
		}
	}

	public float MaxVal
	{
		get{ return maxVal; }
		set
		{
			this.maxVal = value;
			pwUI.MaxValue = maxVal;
		}
	}

	public void Initialize() {		
		this.CurrentVal = 10;
		this.MaxVal = 10;
	}


}
