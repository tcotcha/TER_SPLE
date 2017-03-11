using System.Collections;
using System.Collections.Generic;

public class Tireur : Ennemis{

	public Tireur(float _x,float _y):base(_x,_y){

	}
	public string toString(){
		return "{ \"x\" : " + getPosX () + ", \"y\" : " + getPosY () + " }";
	}
}
