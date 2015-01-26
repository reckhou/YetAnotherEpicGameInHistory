using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	public bool autoPlay;
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
		allowInput = false;
	}

	public void AutoPlay() {
//		allowInput = false;
		autoPlay = true;
	}

	// Update is called once per frame
	void Update () {
		if (autoPlay) {
			GameController.Instance.Hit();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			GameController.Instance.Kill += 100;
			AchievementController.Instance.FinishAchievement(1000);
			return;
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			GameController.Instance.AttackPower += 10;
			AchievementController.Instance.FinishAchievement(1001);
			return;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			GameController.Instance.AttackPower -= 10;
			AchievementController.Instance.FinishAchievement(1002);
			if (GameController.Instance.AttackPower <= 0) {
				AchievementController.Instance.FinishAchievement(1003);
			}
			return;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			GameController.Instance.Kill -= 10;
			return;
		}

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

			if (keyHold) {
				ClearKeyHold();
				UIController.Instance.StopSwitching();
			}
			return;
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
