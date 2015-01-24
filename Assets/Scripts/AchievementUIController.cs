using UnityEngine;
using System.Collections;

public class AchievementUIController : MonoBehaviour {
	public GameObject CloseBtn;

	private static AchievementUIController instance;
	public static AchievementUIController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<AchievementUIController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {
		this.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Switching() {
		CloseBtn.GetComponent<ProgressController>().Switching();
	}

	public void StopSwitching() {
		CloseBtn.GetComponent<ProgressController>().StopSwitch();
	}

	public void Open() {
		this.gameObject.SetActive(true);
	}

	public void Close() {
		this.gameObject.SetActive(false);
	}
}
