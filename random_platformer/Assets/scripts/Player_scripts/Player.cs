﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public float maxSpeed = 2f;
	public float speed = 150f;
	public float jmpHeight = 4f;

	private bool grounded;//True if the player is on the ground

	private Rigidbody2D rg2d;
	private Animator anim;


	void Start () {

		//Init Component
		rg2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		//setup animations
		anim.SetBool ("grounded", grounded);
		anim.SetFloat("speed",Mathf.Abs(rg2d.velocity.x));

		//Rotate player
		if (Input.GetAxis ("Horizontal") != 0) {
			transform.localScale = new Vector2 (Mathf.Sign(Input.GetAxis ("Horizontal"))*Mathf.Abs(transform.localScale.x), transform.localScale.y);
		}
	}

	void FixedUpdate () {

		Vector3 friction = new Vector3 (rg2d.velocity.x * 0.7f, rg2d.velocity.y, 0.0f);

		//Get Input
		float h = Input.GetAxis ("Horizontal");

		//Adding Friction
		if (grounded) {
			rg2d.velocity = friction;
		}
		//Movement
		rg2d.AddForce ((Vector2.right * speed) * h);

		//Max  Speed
		if (rg2d.velocity.x > maxSpeed) {
			rg2d.velocity = new Vector2(maxSpeed, rg2d.velocity.y);
		}
		if (rg2d.velocity.x < -maxSpeed) {
			rg2d.velocity = new Vector2(-maxSpeed, rg2d.velocity.y);
		}

		//Jumping
		if(Input.GetButtonDown("Jump") &&  grounded){
			rg2d.velocity = new Vector3 (0,jmpHeight,0);
		}
	}

	public void setGrounded(bool g){
		grounded = g;
	}
}
