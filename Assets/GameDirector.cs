using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class GameDirector : MonoBehaviour {

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

		TextAsset t = Resources.Load("event") as TextAsset ;
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
				}
			}
			de.count = AchievementController.Instance.Achievements[de.id].needCount;
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
		}
	}

	void event0() {
		// newbie
		ChatBoxController.Instance.ShowMessage(0, 0);
	}

	public void ChatEventDone(int eventID) {
		print("ChatEventDone:" + eventID.ToString());
	}

	public void CheckEvent() {
		// check kill
		int kill = GameController.Instance.Kill;
		foreach (int cur in KillList) {
			if (kill > cur) {
				KillList.Remove(cur);
			} else if (kill == cur) {
				// update achievement
				AchievementController.Instance.FinishAchievement(KillDict[kill].archID);
				// update event
				Event(KillDict[kill].id);
				KillList.Remove(cur);
				break;
			}
		}
		// check money
		// check exp
		// check level
	}

	public string GetNextGoal(string type) {
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
