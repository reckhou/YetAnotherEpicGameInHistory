﻿using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void Flip() {
		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Idle() {
		this.GetComponentInChildren<CharAnimController>().Idle();
	}

	public void Kill() {
		this.GetComponentInChildren<CharAnimController>().Kill();
	}

	public void Remove() {
		this.GetComponentInChildren<CharAnimController>().Remove();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
