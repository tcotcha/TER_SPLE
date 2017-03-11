using System.Collections;
using System.Collections.Generic;

public class Mobile : Plateforme {

	private float positionFinX;
	private float positionFinY;

	public Mobile(int l,float x,float y,float finX,float finY):base(l,x,y){
		positionFinX = finX;
		positionFinY = finY;
	}

	public float getPosFinX(){
		return positionFinX;
	}

	public float getPosFinY(){
		return positionFinY;
	}

	public string toString(){
		return "{ \"largeur\" : " + getLargeur () +", \"x\" : " + getPosX () +", \"y\" : " + getPosY () +", \"Finx\" : " + getPosFinX () +", \"Finy\" : " + getPosFinY () + " }";
	}
}
