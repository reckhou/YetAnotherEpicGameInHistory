using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private float holdTime;
	bool keyHold;
	public float StartSwitchTime;
	private static InputController instance;
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
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z)) {
			if (!UIController.Instance.AnyUIOpened()) {
				GameController.Instance.Hit();
			}

			keyHold = true;
			print("z down!");
		}

		if (Input.GetKey(KeyCode.Z) && keyHold) {
			holdTime += Time.deltaTime;
		}

		if (Input.GetKeyUp(KeyCode.Z)) {
			ClearKeyHold();
			UIController.Instance.StopSwitching();
			print ("z up!");
		}

		if (holdTime > StartSwitchTime && keyHold) {
			print("switchiing!");
			UIController.Instance.Switching();
		}
	}

	public void ClearKeyHold() {
		holdTime = 0;
		keyHold = false;
	}
}
