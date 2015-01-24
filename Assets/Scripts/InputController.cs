using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private float holdTime;
	bool keyHold;
	public float StartSwitchTime;
	private static InputController instance;
	bool allowInput;
	public static InputController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<InputController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		allowInput = true;
	}

	// Update is called once per frame
	void Update () {
		if (!allowInput) {
			ClearKeyHold();
			return;
		}

		if (Input.GetKeyDown(KeyCode.Z)) {
			keyHold = true;
		}

		if (Input.GetKey(KeyCode.Z) && keyHold) {
			holdTime += Time.deltaTime;
		}

		if (Input.GetKeyUp(KeyCode.Z)) {
			if (!UIController.Instance.AnyUIOpened() && keyHold) {
				GameController.Instance.Hit();
      		}

			ClearKeyHold();
			UIController.Instance.StopSwitching();
		}

		if (holdTime > StartSwitchTime && keyHold) {
			UIController.Instance.Switching();
		}
	}

	public void ClearKeyHold() {
		holdTime = 0;
		keyHold = false;
	}

	public void AllowInput(bool allow) {
		allowInput = allow;
	}
}
