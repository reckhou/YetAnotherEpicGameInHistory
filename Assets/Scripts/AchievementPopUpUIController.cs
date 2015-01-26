using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementPopUpUIController : MonoBehaviour {
	public GameObject Title;
	public GameObject Text;
	public GameObject Point;
	public bool Finished;

	public GameObject bgFinished;
	public GameObject bgUnfinished;

	public float popupTime;
	float popupLasts;
	bool delayHide;
	// Use this for initialization
	void Start () {
		popupTime = 5;
		popupLasts = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (delayHide) {
			popupLasts += Time.deltaTime;
			if (popupLasts > popupTime) {
				Hide();
			}
		}
	}

	public void Pop(AchievementController.Achievement archData) {
		// TODO: play a sound
		popupLasts = 0;
		delayHide = true;
		archData.finished = true;
		Init(archData);
		Show();
	}

	public void Show() {
		gameObject.SetActive(true);
		if (Finished) {
			bgFinished.SetActive(true);
			bgUnfinished.SetActive(false);
		} else {
			bgFinished.SetActive(false);
			bgUnfinished.SetActive(true);
		}
	}

	public void Hide() {
		delayHide = false;
		popupLasts = 0;
		gameObject.SetActive(false);
	}

	public void Init(AchievementController.Achievement archData) {
		Title.GetComponent<Text>().text = archData.title;
		Text.GetComponent<Text>().text = archData.text;
		Point.GetComponent<Text>().text = archData.point.ToString();
		Finished = archData.finished;
	}
}
