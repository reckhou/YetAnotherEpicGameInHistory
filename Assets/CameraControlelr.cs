using UnityEngine;
using System.Collections;

public class CameraControlelr : MonoBehaviour {
	float lastShake;
	private static CameraControlelr instance;
	public static CameraControlelr Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<CameraControlelr>();
			}
			
			return instance;
		}
	}

	public void Shake() {
		if (Time.time - lastShake < 0.15f) {
			return;
		}
		lastShake = Time.time;
		this.GetComponent<Animator>().CrossFade("shake", 0.01f);
	}

	public void ShakeBig() {
		this.GetComponent<Animator>().Play("shakeBig");
	}

	public void Still() {
//		this.GetComponent<Animator>().StopPlayback();
		this.GetComponent<Animator>().CrossFade("normal", 0.01f);
	}
}
