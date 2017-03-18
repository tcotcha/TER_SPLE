using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class PowerUp {

	private bool temporaire;
	private float x;
	private float y;
	private int duree;

	public PowerUp(bool temp,float _x,float _y){
		temporaire = temp;
		x = _x;
		y = _y;
	}

	public virtual bool isTemp () {return temporaire;}
	public virtual float getX () {return x;}
	public virtual float getY () {return y;}
	public abstract float getDuree ();
}
