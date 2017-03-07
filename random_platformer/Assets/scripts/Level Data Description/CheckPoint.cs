using System.Collections;
using System.Collections.Generic;

public class CheckPoint {

	private static CheckPoint instance;
	private float x;
	private float y;
	private bool actif;

	private CheckPoint(float _x,float _y){
		x = _x;
		y = _y;
		actif = false;
	}

	public static CheckPoint getInstance(float _x,float _y){
		if (instance == null) {
			instance = new CheckPoint (_x, _y);
		}
		return instance;
	}

}
