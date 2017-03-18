using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Plateforme
{
	public int largeur;
	public float positionX;
	public float positionY;

	public Plateforme(int x, float y, float z) {
		largeur = x;
		positionX = y;
		positionY = z;
	}
	
	public abstract bool getFriable();
	public abstract float getPosFinX ();
	public abstract float getPosFinY ();

	public virtual string Affiche (){
		return "{ \"largeur\" : " + largeur +", \"x\" : " + positionX +", \"y\" : " + positionY;
	}

}
