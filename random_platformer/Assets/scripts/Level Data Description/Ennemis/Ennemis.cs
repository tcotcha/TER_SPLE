using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Ennemis {

	public float x { get; set; }
	public float y { get; set; }
	public abstract int GetDirection();
	public abstract void ChangeDirection ();

	public Ennemis(float _x,float _y){
		x = _x;
		y = _y;
	}

	public virtual string Affiche() {
		return "{ \"x\" : " + x + ", \"y\" : " + y;
	}
}
