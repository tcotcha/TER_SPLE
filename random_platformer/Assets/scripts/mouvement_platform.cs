using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement_platform : MonoBehaviour {

	public float x;
	public float y;
	public float fin_x;
	public float fin_y;
	public bool friable;
	public static bool platformFriableGrounded;

	public List<GameObject> trigger;

	private Vector3 posDeb;
	private Vector3 posFin;
	private Vector3 posSuivante; 

	private float speed;

	private Transform t;
	private Transform tFin;

	private Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		platformFriableGrounded = false;
		speed = 1;
		t = new GameObject ().transform;
		tFin = new GameObject ().transform;
		t.localPosition = new Vector3 (x,y,0);
		tFin.localPosition = new Vector3 (fin_x,fin_y,0);
		posDeb = t.localPosition;
		posFin = tFin.localPosition;
		posSuivante = posFin;
		/*
		 * saison
		if(transform.name.Contains("platform_1")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison"));
		}
		if(transform.name.Contains("platform_2")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison")+"Left");
			transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison")+"Right");
		}
		if(transform.name.Contains("platform_3")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison")+"Left");
			transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison")+"Mid");
			transform.GetChild(2).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("saison")+"Right");
		}
		*/
		if (friable) {
			/*foreach (Transform child in transform)
			{
				if(child.name.Contains("platform")){
					GameObject c = Instantiate (g, child.transform.position, Quaternion.identity);
					c.transform.parent = child.transform;
					c.transform.localScale = new Vector3 (1, 1, 1);
				}
			}*/
			trigger.ForEach (delegate(GameObject obj) {
				obj.SetActive (true);
			});
		}
	}
	
	void Update () {
		move ();
	}

	private void move(){
		transform.position = Vector3.MoveTowards (transform.position, posSuivante, speed * Time.deltaTime);

		if (Vector3.Distance (transform.position, posSuivante) < 0.1f) {
			changeDestination ();
		}
	}

	private void changeDestination(){
		if (posSuivante.Equals (posFin)) {
			posSuivante = posDeb;
		} else {
			posSuivante = posFin;
		}
	}
}
