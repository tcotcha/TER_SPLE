using System.Collections;
using System.Collections.Generic;

public class Ennemis {

	private float x;
	private float y;

	public Ennemis(float _x,float _y){
		x = _x;
		y = _y;
	}

	public float getPosX(){
		return x;
	}

	public float getPosY(){
		return y;
	}
}
