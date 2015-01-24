using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AchievementUIController : MonoBehaviour {
	public GameObject CloseBtn;
	public GameObject SumText;
	public List<GameObject> Achievements;

	int curPage;

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
		NextPage();
	}

	public void Open() {
		this.gameObject.SetActive(true);
		ShowPage(0);
	}

	public void Close() {
		this.gameObject.SetActive(false);
	}

	public void NextPage() {
		int pageID = curPage+1;
		ShowPage(pageID);
	}

	public void ShowPage(int page) {
		// 1. get achievement list sorted
		List<AchievementController.Achievement> achievementList = AchievementController.Instance.GetAchievements();
		int achievementCount = achievementList.Count;
		int maxPage = achievementCount / Achievements.Count + 1;
		// 2. calc 1st achievement next page, if out of bound, go to 1st page
		if (page+1>maxPage) {
			page = 0;
		}
		curPage = page;
		int firstAchievement = curPage * Achievements.Count;
		// 3. display achievement
		for (int i = 0; i < Achievements.Count; i++) {
			if (firstAchievement + i >= achievementList.Count) {
				Achievements[i].SetActive(false);
				continue;
			}
			Achievements[i].SetActive(true);
			AchievementController.Achievement archData = achievementList[firstAchievement + i];
			Achievements[i].GetComponent<AchievementPopUpUIController>().Init(archData);
			Achievements[i].GetComponent<AchievementPopUpUIController>().Show();
		}

		SumText.GetComponent<Text>().text = AchievementController.Instance.getPoints.ToString() + "/" + AchievementController.Instance.allPoints.ToString();
	}
}
