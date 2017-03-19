using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemis_script : MonoBehaviour {

	public float speed = 150f;

	public bool isBumper;
	private int direction;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start(){
		//Init Component
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		if (!isBumper) {
			CapsuleCollider2D cc2d = GetComponent<CapsuleCollider2D> ();
			cc2d.offset = new Vector2(0.57f,-1.4f);
			cc2d.size = new Vector2(3.9f,5.4f);
		}
	}

	void Update () {
		//Setup animation
		anim.SetBool("isBumper",isBumper);

	}

	void FixedUpdate(){

	}

	public void init(bool b){
		isBumper = b;
		if (b) {
			direction = -1;
		}
	}

	public int getDirection(){
		return direction;
	}

	public void setDirection(int d){
		direction = d;
		if (!isBumper) {
			CapsuleCollider2D cc2d = GetComponent<CapsuleCollider2D> ();
			cc2d.offset = new Vector2(0.57f,-1.4f);
			cc2d.size = new Vector2(3.9f,5.4f);
		}
	}
}
