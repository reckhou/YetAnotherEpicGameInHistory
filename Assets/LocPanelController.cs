using UnityEngine;
using System.Collections;

public class LocPanelController : MonoBehaviour {
	private static LocPanelController instance;
	public static LocPanelController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<LocPanelController>();
			}
			
			return instance;
		}
	}

	float holdTime;
	bool LangSelected;
	public string Language;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update() {
		if (LangSelected) {
			return;
		}

		if (Input.GetKey(KeyCode.Z)) {
			holdTime += Time.deltaTime;
		}

		if (holdTime > 2.0f) {
			LangSelected = true;
			Language = "CN";
			InputController.Instance.AllowInput(true);
			GameController.Instance.Init();
			this.gameObject.SetActive(false);
			return;
		}

		if (Input.GetKeyUp(KeyCode.Z)) {
			LangSelected = true;
			Language = "EN";
			InputController.Instance.AllowInput(true);
			GameController.Instance.Init();
			this.gameObject.SetActive(false);
			return;
		}
	}
}
