using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] scales;

	private Transform cam;
	private Vector3 previousPos;

	private float min;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		previousPos = cam.position;
		float height = 2f * Camera.main.orthographicSize;
		float width = height * Camera.main.aspect;

		scales = new float[backgrounds.Length];

		for(int i = 0;i<backgrounds.Length;i++){
			scales [i] = backgrounds[i].position.z *-1;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (cam.transform.position.x == min) {
			previousPos = cam.transform.position;
			for (int i = 0; i < backgrounds.Length; i++) {
				Vector3 tmp = new Vector3 (-0.5f, backgrounds[i].position.y, backgrounds [i].position.z);
				backgrounds [i].position = tmp;
			}
		}
		if (previousPos != null && (previousPos.x != cam.position.x || previousPos.y != cam.position.y)) {

			for (int i = 0; i < backgrounds.Length; i++) {
				Vector3 tmp = (previousPos - cam.position) * (scales[i]/10);
				backgrounds [i].position = new Vector3(backgrounds[i].position.x + tmp.x,backgrounds[i].position.y + tmp.y,backgrounds[i].position.z);
			}
			previousPos = cam.position;
		} else if(previousPos == null){
			previousPos = cam.transform.position;
		}
	}
}
