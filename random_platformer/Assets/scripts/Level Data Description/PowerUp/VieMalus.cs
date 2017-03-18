using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VieMalus : PowerUp{

	public VieMalus(float _x,float _y):base(false,_x,_y){

	}
	public override float getDuree(){
		throw new System.InvalidOperationException("VieMalus n'a pas de duree !");
	}
}
