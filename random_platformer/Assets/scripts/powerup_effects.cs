﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup_effects : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D other) {
		yield return true;
	}

}
