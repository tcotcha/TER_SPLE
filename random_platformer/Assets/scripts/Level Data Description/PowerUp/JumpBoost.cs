using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class JumpBoost : PowerUp{

	private float duree;

	public JumpBoost(float d,float _x,float _y):base(true,_x,_y){
		duree = d;
	}

	public override float getDuree(){
		return duree;
	}
}
