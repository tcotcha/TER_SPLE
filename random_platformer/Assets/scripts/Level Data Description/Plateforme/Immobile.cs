using System.Collections;
using System.Collections.Generic;

public class Immobile : Plateforme {

	private bool friable;

	public Immobile(int l,float x,float y,bool f):base(l,x,y){
		friable = f;
	}

	public bool getFriable(){
		return friable;
	}
}
