using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class GameDirector : MonoBehaviour {
	public GameObject CreditUI;

	Dictionary <int, DirectorEvent> KillDict; // killcount-data
	List<int> KillList; // killcounts
	Dictionary <int, DirectorEvent> EventDict; // eventID-data

	public struct DirectorEvent {
		public int id;
		public string type;
		public int archID;
		public int count;
		public string reward;
		public int up;
	}

	private static GameDirector instance;
	public static GameDirector Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameDirector>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {
		KillDict = new Dictionary<int, DirectorEvent>();
		KillList = new List<int>();

		EventDict = new Dictionary<int, DirectorEvent>();
		TextAsset t;
		if (LocPanelController.Instance.Language == "CN") {
			t = Resources.Load("event") as TextAsset ;
		} else {
			t = Resources.Load("event-en") as TextAsset ;
	    }

		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(t.ToString()); // load the file.
		XmlNodeList xmlList = xmlDoc.GetElementsByTagName("event"); // array of the level nodes.
		foreach (XmlNode msg in xmlList) {
			XmlNodeList content = msg.ChildNodes;
			DirectorEvent de = new DirectorEvent();

			de.id = -1;
			de.archID = -1;
			de.count = -1;
			de.type = "";
			foreach (XmlNode info in content) {
				//				print (msgInfo.InnerText);
				if (info.Name == "id") {
					de.id = int.Parse(info.InnerText);
				} else if (info.Name == "type") {
					de.type = info.InnerText;
				} else if (info.Name == "achID") {
					de.archID = int.Parse(info.InnerText);
				} else if (info.Name == "reward") {
					de.reward = info.InnerText;
				} else if (info.Name == "up") {
					de.up = int.Parse(info.InnerText);
				} else if (info.Name == "count") {
					de.count = int.Parse(info.InnerText);
				}
			}

			if (de.count < 0) {
				de.count = AchievementController.Instance.Achievements[de.id].needCount;
			}

			if (de.type == "kill") {
				KillDict[de.count] = de;
				KillList.Add(de.count);
			} else if (de.type == "money") {
			} else if (de.type == "level") {
			} else if (de.type == "exp") {
			}
			EventDict.Add(de.id, de);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Event(int eventID) {
		// update attack
		int up = EventDict[eventID].up;
		GameController.Instance.AttackPower += up;

		if (eventID == 0) {
			event0();
		} else if (eventID == 8) {
			GameController.Instance.AttackPower = 1;
		} else if (eventID >= 20 && eventID <= 33) {
			ChatBoxController.Instance.ShowMessage(eventID, eventID);
		} else if (eventID == 34) {
			AchievementController.Instance.FinishAchievement(10);
		} else if (eventID == 35) {
			print ("game finish!");
			StartCoroutine("ShowCredit");
			AchievementController.Instance.SetPopupTime(30);
			AchievementController.Instance.FinishAchievement(998);
			for (int i = 0; i < 100; i ++) {
				GameController.Instance.SpawnEnemy();
			}
			GameController.Instance.AttackPower = 51;
			InputController.Instance.AutoPlay();
		}
	}

	IEnumerator ShowCredit() {
		yield return new WaitForSeconds(35.0f);
		CreditUI.SetActive(true);
		AchievementController.Instance.FinishAchievement(999);
		yield return new WaitForSeconds(35.0f);
		CreditUI.SetActive(false);
	}

	void event0() {
		GameController.Instance.EnableSpawn(true);
		InputController.Instance.AllowInput(true);
	}

	public void ChatEventDone(int eventID) {
		print("ChatEventDone:" + eventID.ToString());
		if (eventID == 31) {
			AchievementController.Instance.FinishAchievement(1004);
			GameController.Instance.Kill = 999900;
		} else if (eventID == 32) {
			GameController.Instance.Level = 100;
		} else if (eventID == 33) {
			AchievementController.Instance.FinishAchievement(1005);
		}
	}

	public void CheckEvent() {
		// check kill
		int kill = GameController.Instance.Kill;
		for (int i = 0; i < KillList.Count; i++) {
			int cur = KillList[i];
			if (kill >= cur) {
//				if (kill > cur) {
//					//				for(int i = 0; i < KillList.Count; i++) {
//					//					int tmp = KillList[i];
//					//
//					//					KillList.Remove(tmp);
//					//				}
//					if (KillDict[cur].archID >= 0) {
//						AchievementController.Instance.FinishAchievement(KillDict[cur].archID);
//					}
//					KillList.Remove(cur);
//					i--;
//				}
				// update achievement
				if (KillDict[cur].archID >= 0) {
					if (KillDict[cur].archID == 0) {
						SoundEffectContoller.Instance.PlayMusic1();
					} else if (KillDict[cur].archID == 8) {
						SoundEffectContoller.Instance.PlayMusic2();
					}

					AchievementController.Instance.FinishAchievement(KillDict[cur].archID);
				}
				// update event
				Event(KillDict[cur].id);
				KillList.Remove(cur);
				break;
			}
		}
		// check money
		// check exp
		// check level
		if (GameController.Instance.Level >= 100) {
			AchievementController.Instance.FinishAchievement(51);
		}
	}

	public string GetNextGoal(string type) {
		if (KillList == null) {
			return "";
		}

		if (type == "kill" && KillList.Count > 0) {
			string reward = KillDict[KillList[0]].reward;
			if (reward != "") {
				return KillList[0].ToString() + " " + reward;
			} else {
				return KillList[0].ToString();
			}
		}
		return "-4294967296";
	}
}
