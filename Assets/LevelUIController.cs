using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIController : MonoBehaviour {
	public GameObject progressbar;
	public GameObject text;
	private static LevelUIController instance;
	public static LevelUIController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<LevelUIController>();
			}
			
			return instance;
		}
	}

	public void SetLevel(int level) {
		text.GetComponent<Text>().text = "LEVEL: " + level.ToString();
	}

	public void SetPercentage(float percentage) {
		progressbar.GetComponent<ProgressBar>().SetPercentage(percentage);
	}

	void Update() {
		SetLevel(GameController.Instance.Level);
		float percentage = 0;
		int Exp = GameController.Instance.Exp;
		int Level = GameController.Instance.Level;
		if (Level < 10) {
			percentage = Exp / 30.0f;
		} else if (Level < 20) {
			percentage = Exp / 60.0f;
		} else if (Level < 30) {
			percentage = Exp / 160.0f;
		} else if (Level < 50) {
			percentage = Exp / 580.0f;
		} else if (Level < 1000) {
			percentage = Exp / 1000.0f;
		}
		SetPercentage(percentage);
	}
}
