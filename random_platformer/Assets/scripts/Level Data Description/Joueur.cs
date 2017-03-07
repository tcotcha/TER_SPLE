using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur {

	private static Joueur instance;
	int nbVies;
	private float hauteurSaut;
	private float vitesse;
	private float x;
	private float y;

	private Joueur(float _x,float _y){
		nbVies = 3;
		hauteurSaut = 1;
		vitesse = 2;
		x = _x;
		y = _y;
	}

	public static Joueur getInstance(float _x,float _y){
		if (instance == null) {
			instance = new Joueur (_x, _y);
		}
		return instance;
	}


}
