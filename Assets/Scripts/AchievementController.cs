using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class AchievementController : MonoBehaviour {
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
		return new List<Achievement>();
	}

	public void FinishAchievement(string type, int count) {
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
		}
	}

	// Update is called once per frame
	void Update () {
		checkAchievement();
	}

	void checkAchievement() {
		//TODO: check these "special" achievements
	}
}
