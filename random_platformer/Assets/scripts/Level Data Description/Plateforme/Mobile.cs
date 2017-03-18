using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mobile : Plateforme {

	public float positionFinX;
	public float positionFinY;

	public Mobile(int l,float x,float y,float finX,float finY):base(l,x,y){
		positionFinX = finX;
		positionFinY = finY;
	}

	public override bool getFriable() {
		throw new System.InvalidOperationException("Une plateforme mobile ne peut pas être friable");
	}

	public override float getPosFinX(){
		return positionFinX;
	}

	public override float getPosFinY(){
		return positionFinY;
	}

	public override string Affiche(){
		return base.Affiche() +", \"FinX\" : "+  getPosFinX () +", \"Finy\" : " + getPosFinY () + " }";
	}
		
}
