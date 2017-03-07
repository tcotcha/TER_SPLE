using System.Collections;
using System.Collections.Generic;

public class Inversement : Items {

	private float duree;

	public Inversement(float d,float _x,float _y):base(true,_x,_y){
		duree = d;
	}

	public float getDuree(){
		return duree;
	}
}
