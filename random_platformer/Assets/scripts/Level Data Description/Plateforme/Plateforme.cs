using System.Collections;
using System.Collections.Generic;

public class Plateforme {

	private int largeur;
	private float positionX;
	private float positionY;

	public Plateforme(int l,float x,float y){
		largeur = l;
		positionX = x;
		positionY = y;
	}

	public int getLargeur(){
		return largeur;
	}

	public float getPosX(){
		return positionX;
	}

	public float getPosY(){
		return positionY;
	}
}
