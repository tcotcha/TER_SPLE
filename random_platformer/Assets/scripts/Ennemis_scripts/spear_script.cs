using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear_script : MonoBehaviour {

    private Rigidbody2D rb2d;

    private float speed = 150f;
    private float maxSpeed = 1.5f;
    private int direction;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z *= -1 * direction;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
	
	
	void Update () {
        //Movement
        rb2d.AddForce((Vector2.right * speed) * direction);
        transform.localScale = new Vector2 (-1*direction*Mathf.Abs(transform.localScale.x), transform.localScale.y);


        //Max  Speed
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public void init(int d) {
        direction = d;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().die();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Ground") {
            Destroy(gameObject);
        }
    }
}
