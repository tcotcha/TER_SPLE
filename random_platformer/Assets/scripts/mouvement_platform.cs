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

	void Start () {
		platformFriableGrounded = false;
		speed = 1;
		t = new GameObject ().transform;
		tFin = new GameObject ().transform;
		t.localPosition = new Vector3 (x,y,0);
		tFin.localPosition = new Vector3 (fin_x,fin_y,0);
		posDeb = t.localPosition;
		posFin = tFin.localPosition;
		posSuivante = posFin;

		if(transform.name.Contains("platform_1")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison"));
			transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"Friable"));
		}
		if(transform.name.Contains("platform_2")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison")+"Left");
			transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"FriableLeft"));
			transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison")+"Right");
			transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"FriableRight"));
		}
		if(transform.name.Contains("platform_3")){
			transform.GetChild(0).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison")+"Left");
			transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"FriableLeft"));
			transform.GetChild(1).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison")+"Mid");
			transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"FriableMid"));
			transform.GetChild(2).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("Saison")+"Right");
			transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(PlayerPrefs.GetString("Saison")+"FriableRight"));
		}
		if (friable) {
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
