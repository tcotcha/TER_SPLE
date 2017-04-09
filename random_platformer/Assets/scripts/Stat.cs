using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat {

	[SerializeField]
	private PowerUpUI pwUI;

	//[SerializeField]
	private float maxVal = 10;

	//[SerializeField]
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
		this.CurrentVal = currentVal;
		this.MaxVal = maxVal;
	}

	/*public void Reset(int current, int max) {
		this.CurrentVal = current;
		this.MaxVal = max;
	}*/


}
