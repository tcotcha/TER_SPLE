using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VieBonus : PowerUp{
	public VieBonus(float _x,float _y):base(false,_x,_y){

	}
	public override float getDuree(){
		throw new System.InvalidOperationException("VieBonus n'a pas de duree !");
	}
}
