using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Invincibilite : PowerUp {

	private float duree;

	public Invincibilite(float d,float _x,float _y):base(true,_x,_y){
		duree = d;
	}

	public override float getDuree(){
		return duree;
	}
}
