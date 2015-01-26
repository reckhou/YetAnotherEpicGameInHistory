using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {
	float lastShoot;
	public void Shoot() {
		float now = Time.time;
		float deltaTime = now - lastShoot;
		float speed = 1.0f;
		if (deltaTime <= 0.25f && deltaTime > 0) {
			speed = 0.25f / deltaTime * 2;
		} else if (deltaTime <= 0) {
			return;
		} else if (deltaTime > 0.25f) {
			speed = 0.25f / deltaTime * 2;
		}


		GetComponent<Animator>().speed = speed;

		GetComponent<Animator>().CrossFade("shoot", 0.01f);
		lastShoot = now;

		Vector3 pos = transform.parent.localPosition;
		pos.x = Tools.Random(-2.0f, 4f);
		transform.parent.localPosition = pos;
	}

	public void Idle() {
		GetComponent<Animator>().speed = 1.0f;
		GetComponent<Animator>().Play("idle");
	}
}
