using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tireur : Ennemis{

	public Tireur(float _x,float _y):base(_x,_y){

	}

	public override int GetDirection(){
		throw new System.InvalidOperationException ("Un tireur n'a pas de direction");
	}

	public override void ChangeDirection(){
			throw new System.InvalidOperationException("On ne peut pas changer la direction d'un tireur");
	}

	public string toString(){
		return base.Affiche() + " }";
	}
}
