using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CheckPoint {

	private static CheckPoint instance;
	public float x { get; set; }
	public float y { get; set; }
	public bool actif { get; set; }

	public CheckPoint(float _x,float _y){
		x = _x;
		y = _y;
		actif = false;
	}

	public CheckPoint(){
	}

	public static CheckPoint getInstance(float _x,float _y){
		if (instance == null) {
			instance = new CheckPoint (_x, _y);
		}
		return instance;
	}

}
