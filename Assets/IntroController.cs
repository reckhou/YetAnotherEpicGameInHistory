using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroController : MonoBehaviour {

	public List<GameObject> panels;

	// Use this for initialization
	void Start () {
		foreach (GameObject cur in panels) {
			cur.gameObject.SetActive(false);
		}

		StartCoroutine("PlayPanel");
	}
	
	IEnumerator PlayPanel() {
		this.GetComponent<AudioSource>().Play();
		panels[0].SetActive(true);
		yield return new WaitForSeconds(4.0f);
		panels[0].SetActive(false);
		panels[1].SetActive(true);
		yield return new WaitForSeconds(4.0f);
		panels[1].SetActive(false);
		panels[2].SetActive(true);
		yield return new WaitForSeconds(9.0f);
		panels[2].SetActive(false);
		panels[3].SetActive(true);
		yield return new WaitForSeconds(9.0f);
		panels[3].SetActive(false);
		panels[4].SetActive(true);
		yield return new WaitForSeconds(9.0f);
		panels[4].SetActive(false);
		panels[5].SetActive(true);
		yield return new WaitForSeconds(11.0f);
		Application.LoadLevel("main");
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton9)) {
			Application.LoadLevel("main");
		}
	}
}
