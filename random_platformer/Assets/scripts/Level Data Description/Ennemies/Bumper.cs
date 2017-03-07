using System.Collections;
using System.Collections.Generic;

public class Bumper : Ennemis {

	private int direction;

	public Bumper(float _x,float _y):base(_x,_y){
		direction = -1;
	}

	public int getDirection(){
		return direction;
	}

	public void changeDirection(){
		direction *= -1;
	}
}
