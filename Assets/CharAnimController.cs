using UnityEngine;
using System.Collections;

public class CharAnimController : MonoBehaviour {

	public void Idle() {
		this.GetComponent<Animator>().Play("idle");
	}
	
	public void Kill() {
		this.GetComponent<Animator>().Play("dead");
	}

	public void Remove() {
		Destroy(gameObject.transform.parent.gameObject);
	}
}
