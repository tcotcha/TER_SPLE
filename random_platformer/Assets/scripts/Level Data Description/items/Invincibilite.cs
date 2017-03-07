using System.Collections;
using System.Collections.Generic;

public class Invincibilite : Items {

	private float duree;

	public Invincibilite(float d,float _x,float _y):base(true,_x,_y){
		duree = d;
	}

	public float getDuree(){
		return duree;
	}
}
