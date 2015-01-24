using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class AchievementController : MonoBehaviour {
	public GameObject AchievementUIPopup;
	public int getPoints;
	public int allPoints;

	public struct Achievement {
		public int id;
		public string type;
		public int needCount;
		public bool finished;
		public string text;
		public string title;
		public int point;
	}

	public Dictionary<int, Achievement> Achievements; // id-achievement

	private static AchievementController instance;
	public static AchievementController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<AchievementController>();
			}
			
			return instance;
		}
	}

	public List<Achievement> GetAchievements() {
		// sort achievements by this order:
		// Kill-Exp-Money (finished) Ascend by ID
		// Kill-Exp-Money (unfinished) Ascend by ID
		List<Achievement> ret = new List<Achievement>();

		List<Achievement> killFinished = new List<Achievement>();
		List<Achievement> killUnfinished = new List<Achievement>();
		List<Achievement> expFinished = new List<Achievement>();
		List<Achievement> expUnfinished = new List<Achievement>();
		List<Achievement> moneyFinished = new List<Achievement>();
		List<Achievement> moneyUnfinished = new List<Achievement>();

		foreach (KeyValuePair<int, Achievement> tmpPair in Achievements) {
			Achievement tmp = tmpPair.Value;
			if (tmp.type == "kill" && tmp.finished) {
				killFinished.Add(tmp);
			} else if (tmp.type == "kill" && !tmp.finished) {
				killUnfinished.Add(tmp);
			} else if (tmp.type == "exp" && tmp.finished) {
				expFinished.Add(tmp);
			} else if (tmp.type == "exp" && !tmp.finished) {
				expUnfinished.Add(tmp);
			} else if (tmp.type == "money" && tmp.finished) {
				moneyFinished.Add(tmp);
			} else if (tmp.type == "money" && !tmp.finished) {
				moneyUnfinished.Add(tmp);
			}
    	}

		killFinished.Sort(delegate(Achievement x, Achievement y) {
			return x.id - y.id;
		});
		killUnfinished.Sort(delegate(Achievement x, Achievement y) {
			return x.id - y.id;
		});
    	
		foreach (Achievement tmp in killFinished) {
			ret.Add(tmp);
		}
		foreach (Achievement tmp in killUnfinished) {
			ret.Add(tmp);
		}
    	return ret;
	}

	public void FinishAchievement(int id) {
		if (Achievements[id].finished) {
			print("ERROR! Achievement already finished, ID:" + id.ToString());
			return;
		}

		AchievementUIPopup.GetComponent<AchievementPopUpUIController>().Pop(Achievements[id]);
		Achievement tmp = Achievements[id];
		tmp.finished = true;
		Achievements[id] = tmp;
		getPoints += tmp.point;
	}

	// Use this for initialization
	public void Init () {
		Achievements = new Dictionary<int, Achievement>();
		TextAsset t = Resources.Load("achievement") as TextAsset ;
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(t.ToString()); // load the file.
		XmlNodeList achList = xmlDoc.GetElementsByTagName("achievement"); // array of the level nodes.
		foreach (XmlNode msg in achList) {
			XmlNodeList content = msg.ChildNodes;
			Achievement cur = new Achievement();
			cur.id = -1;
			cur.finished = false;
			foreach (XmlNode info in content) {
				//				print (msgInfo.InnerText);
				if (info.Name == "id") {
					cur.id = int.Parse(info.InnerText);
				} else if (info.Name == "type") {
					cur.type = info.InnerText;
				} else if (info.Name == "needCount") {
					cur.needCount = int.Parse(info.InnerText);
				} else if (info.Name == "text") {
					cur.text = info.InnerText;
				} else if (info.Name == "title") {
					cur.title = info.InnerText;
				} else if (info.Name == "point") {
					cur.point = int.Parse(info.InnerText);
				}
			}
			print (cur.id);
			Achievements.Add(cur.id, cur);
			if (cur.id != 999) {
				allPoints += cur.point;
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
