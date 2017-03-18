using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Immobile : Plateforme {

	public bool friable;

	public Immobile(int l,float x,float y,bool f):base(l,x,y){
		friable = f;
	}

	public override bool getFriable(){
		return friable;
	}

	public override float getPosFinX(){
		throw new System.InvalidOperationException("Une plateforme immobile n'a pas de positionFinX");
	}

	public override float getPosFinY(){
		throw new System.InvalidOperationException("Une plateforme immobile n'a pas de positionFinY");
	}

	
	public override string Affiche(){
		return base.Affiche() + ", friable : "+ friable +" }";
	}
}
