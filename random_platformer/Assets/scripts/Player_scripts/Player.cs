using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public float maxSpeed = 2f;
	public float speed = 150f;
	public float jmpHeight;
	private int nbVie = 3;

	private Canvas CanvasLoose;

	private bool grounded;//True if the player is on the ground
	private bool powerUpReset;
	private bool powerUpInvincibleActif = false;
	private bool powerUpInversementActif = false;
	private bool powerUpJumpBoostActif = false;

	private Rigidbody2D rg2d;
	private Animator anim;

	[SerializeField]
	private Stat inversement;
	[SerializeField]
	private Stat jumpboost;
	[SerializeField]
	private Stat invincibilite;
	[SerializeField]
	private Image Image_nbvie;

	private float h;

	void Awake() {
		inversement.Initialize ();
		jumpboost.Initialize ();
		invincibilite.Initialize ();
	}
	
	void Start () {
		rg2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		CanvasLoose = GameObject.Find("CanvasLoose").GetComponent<Canvas>();
	}

	void Update(){
		//setup animations
		anim.SetBool ("grounded", grounded);
		anim.SetFloat("speed",Mathf.Abs(rg2d.velocity.x));

		//Rotate player
		if (Input.GetAxis ("Horizontal") != 0) {
			transform.localScale = new Vector2 (Mathf.Sign(Input.GetAxis ("Horizontal"))*Mathf.Abs(transform.localScale.x), transform.localScale.y);
		}
		//Revenir au menu en appuyant sur "echap"
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene("menu");

		CheckPowerUpActif ();
		CheckVieEtUpdateImage ();
		CheckDirection ();
		CheckSaut ();

		if (getGrounded ()) {
			rg2d.gravityScale = 1;
		}
	}

	void CheckVieEtUpdateImage() {
		if (nbVie <= 0) {
			Time.timeScale = 0;
			CanvasLoose.enabled = true;
		}
		Image_nbvie.sprite = Resources.Load<Sprite> ("numeros/hud_" + getNbVie ());
	}

	void CheckPowerUpActif () {
		if (powerUpInversementActif && !powerUpReset) {
			inversement.CurrentVal -= Time.deltaTime;
		} else {
			inversement.CurrentVal = 10;
		}
		if (powerUpInvincibleActif && !powerUpReset) {
			invincibilite.CurrentVal -= Time.deltaTime;
		} else {
			invincibilite.CurrentVal = 10;
		}
		if (powerUpJumpBoostActif && !powerUpReset) {
			jumpboost.CurrentVal -= Time.deltaTime;
		} else {
			jumpboost.CurrentVal = 10;
		}
		if (powerUpReset) {
			jumpboost.Initialize();
			invincibilite.Initialize ();
			inversement.Initialize ();
			setPowerUpInversementActif (false);
			setPowerUpInvincibleActif (false);
			setPowerUpJumpBoostActif (false);
			powerUpReset = false;
		}

	}

	// Met a jour le sens directionnel du joueur, si le powerUp Inversement est actif, alors il change le sens
	void CheckDirection() {
		//Get Input
		h = Input.GetAxis ("Horizontal");
		if (powerUpInversementActif) {
			h = h * (-1);
		}
	}

	void CheckSaut() {
		if (powerUpJumpBoostActif) {
			setJmpHeight (9);
		} else {
			setJmpHeight (7);
		}
	}

	void FixedUpdate () {

        Vector3 friction;
        if (GameObject.Find("Handler").GetComponent<GenerationNiveau>().getNiveau().saison != 0)
        {
            friction = new Vector3(rg2d.velocity.x * 0.7f, rg2d.velocity.y, 0.0f);
        }else { 
            friction = new Vector3(rg2d.velocity.x * 0.95f, rg2d.velocity.y, 0.0f);
        }

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
		if((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))  &&  grounded){
			Vector2 tmp = new Vector3 (0,jmpHeight);
			rg2d.velocity = rg2d.velocity+tmp;
		}

		//Fast falling
		if (rg2d.velocity.y < 0) {
			rg2d.gravityScale = 3;
		} else {
			rg2d.gravityScale = 1;
		}
	}

	public void setGrounded(bool g){
		grounded = g;
	}
	public bool getGrounded(){
		return grounded;
	}

	public void die(string mort){
		GenerationNiveau.regenererPlateformes ();
		rg2d.gravityScale = 1;

		Vector3 restart;
		if(GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<trigger_flag>().actif){
			restart = new Vector3 (GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<trigger_flag>().transform.localPosition.x, GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<trigger_flag>().transform.localPosition.y, 0);
		}else {
			restart = new Vector3 (0, PlayerPrefs.GetInt("Y0"), 0);
		}

		if (mort.Equals("trigger_sol") || !powerUpInvincibleActif) {
			transform.localPosition = restart;
			GetComponentInChildren<GroundCheck> ().setNb (0);
			setNbVie (getNbVie () - 1);
			powerUpReset = true;
		}
	}

	public void ResetEffect(string nom) {		
		switch (nom) {
		case "JumpBoost":
			jumpboost.Initialize ();
			break;
		case "Inversement":
			inversement.Initialize ();
			break;
		case "Invincibilite":
			invincibilite.Initialize ();
			break;
		default:
			break;
		}
	}

	public void setJmpHeight(float jmp) {
		this.jmpHeight = jmp;
	}

	public float getJmpHeight() {
		return this.jmpHeight;
	}

	public void setNbVie(int vie) {
		this.nbVie = vie;
	}

	public int getNbVie(){
		return nbVie;
	}

	public void setPowerUpInvincibleActif(bool activite) {
		this.powerUpInvincibleActif  = activite;
	}
	public void setPowerUpInversementActif(bool activite) {
		this.powerUpInversementActif  = activite;
	}
	public void setPowerUpJumpBoostActif(bool activite) {
		this.powerUpJumpBoostActif  = activite;
	}
}
