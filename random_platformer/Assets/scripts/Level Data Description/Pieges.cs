using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Pieges {

	public int longueur;
	public float positionX;

	public Pieges(int l, float x){
		longueur = l;
		positionX = x;
	}

	private int getLongeur(){
		return longueur;
	}

	private float getPosition(){
		return positionX;
	}
}
