using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Bumper : Ennemis {

	private int direction;

	public Bumper(float _x,float _y):base(_x,_y){
		direction = -1;
	}

	public override int GetDirection(){
		return direction;
	}

	public override void ChangeDirection(){
		direction *= -1;
	}

	public override string Affiche(){
		return base.Affiche() + ", \"direction\" : " + direction +" }";
	}
}
