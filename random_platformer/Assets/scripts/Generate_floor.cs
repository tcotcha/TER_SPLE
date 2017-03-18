using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_floor : MonoBehaviour {

	public GameObject summer_bottom;
	public GameObject summer_top;
	public GameObject winter_bottom;
	public GameObject winter_top;
	public GameObject murFin;

	public int season;
	public int width;

	public int heightMultiplier;
	public float smoothness;

	private int seed;

	void Start () {
		seed = Random.Range (-10000, 10000);
		Generate ();
		murFin.transform.position = new Vector3(width, transform.position.y,transform.position.z);
		Camera.main.GetComponent<Camera_follow> ().setMaxX (width-1);
	}

	public void Generate() {
		for (int i = 0; i < width; i++) {
			float h =Mathf.PerlinNoise(seed,i/smoothness)*heightMultiplier;
			int i_h = Mathf.RoundToInt (h);
			GameObject selected;
			if (h > 1) {
				for (int j = 0; j < i_h; j++) {
					selected = chooseTile (i_h, j);
					Instantiate (selected, new Vector2 (i, j), Quaternion.identity);
				}
			}
		}
	}

	private GameObject chooseTile(int h,int j){
		if (season == 0) {
			if (j < h - 1) {
				return winter_bottom;
			} else {
				return winter_top;
			}
		} else {
			if (j < h - 1) {
				return summer_bottom;
			} else {
				return summer_top;
			}
		}
	}
}
