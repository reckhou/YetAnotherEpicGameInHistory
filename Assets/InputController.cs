﻿using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	float holdTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z)) {
			GameController.Instance.Hit();
		}
	}
}
