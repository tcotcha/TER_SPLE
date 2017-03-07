using System.Collections;
using System.Collections.Generic;

public class Items {

	private bool temporaire;
	private float x;
	private float y;

	public Items(bool temp,float _x,float _y){
		temporaire = temp;
		x = _x;
		y = _y;
	}

	public bool isTemp(){
		return temporaire;
	}

	public float getX(){
		return x;
	}

	public float getY(){
		return y;
	}
}
