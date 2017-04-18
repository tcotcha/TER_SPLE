using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	private GameObject player;

	private int maxX;
	private float height;
	private float width; 

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		height = 2f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
	}

	void FixedUpdate () {		
		if (player != null) {
			float _x = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
			float _y = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

			float minY = height / 2f - 1f;
			float minX = width / 2f;
			float max_X = maxX - (width / 2f);
			transform.position = new Vector3 (Mathf.Clamp (_x, minX, max_X), Mathf.Clamp (_y, minY, 100), transform.position.z);
		} else {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

	}

	public void setMaxX(int _x){
		maxX = _x;
	}
}
