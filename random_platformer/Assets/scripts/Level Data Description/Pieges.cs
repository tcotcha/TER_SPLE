using System.Collections;
using System.Collections.Generic;

public class Pieges {

	private int longeur;
	private float positionX;

	public Pieges(int l, float x){
		longeur = l;
		positionX = x;
	}

	private int getLongeur(){
		return longeur;
	}

	private float getPosition(){
		return positionX;
	}
}
