using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	UIType openedUI;

	public GameObject AchievementIcon;
	public GameObject AchievementPopup;

	private static UIController instance;
	public static UIController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<UIController>();
			}
			
			return instance;
		}
	}

	public enum UIType {
		None = -1,
		Chat = 0,
		Achievement
	};

	// Use this for initialization
	public void Init () {
		openedUI = UIType.None;
		AchievementUIController.Instance.Init();
		ChatBoxController.Instance.Init();
		KillCountController.Instance.Init();
		AchievementPopup.GetComponent<AchievementPopUpUIController>().Hide();
	}

	public void DoSwitch() {
		if (openedUI == UIType.None) {
			// open achievement ui when none other ui showed up
			Open(UIType.Achievement);
		} else if (openedUI == UIType.Achievement) {
			Close();
		} else if (openedUI == UIType.Chat) {
			ChatBoxController.Instance.PlayNextMessage();
		}
	}

	public void Switching() {
		if (openedUI == UIType.Chat) {
			ChatBoxController.Instance.Switching();
		} else if (openedUI == UIType.None) {
			AchievementIcon.GetComponent<ProgressController>().Switching();
		} else if (openedUI == UIType.Achievement) {
			AchievementUIController.Instance.Switching();
		}
	}

	public void StopSwitching() {
		if (openedUI == UIType.Chat) {
			ChatBoxController.Instance.StopSwitch();
		} else if (openedUI == UIType.None) {
			AchievementIcon.GetComponent<ProgressController>().StopSwitch();
		} else if (openedUI == UIType.Achievement) {
			AchievementUIController.Instance.StopSwitching();
		}
	}
	
	public void Open(UIType type) {
		if (openedUI != UIType.None) {
			return;
		}

		if (openedUI == UIType.Chat) {
			ChatBoxController.Instance.SetVisible(true);
		} else if (type == UIType.Achievement) {
			AchievementUIController.Instance.Open();
		}

		openedUI = type;
		InputController.Instance.ClearKeyHold();
	}

	public void Close() {
		if (openedUI == UIType.None) {
			return;
		}

		if (openedUI == UIType.Chat) {
			ChatBoxController.Instance.SetVisible(false);
		} else if (openedUI == UIType.Achievement) {
			AchievementUIController.Instance.Close();
		}

		openedUI = UIType.None;
	}

	public bool AnyUIOpened() {
		if (openedUI != UIType.None) {
			return true;
		}

		return false;
	}
}
