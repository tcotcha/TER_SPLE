using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Piece {

	private static Piece instance;
	public float positionX{ get; set; }
	public float positionY { get; set; }

	public Piece(float _x,float _y){
		positionX = _x;
		positionY = _y;
	}

	public Piece(){
	}

	public static Piece getInstance(float _x,float _y){
		if (instance == null) {
			instance = new Piece (_x, _y);
		}
		return instance;
	}
}
