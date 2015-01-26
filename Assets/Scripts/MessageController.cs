using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class MessageController : MonoBehaviour {
	public struct Message {
		public int id;
		public List<int> nextMessage;
		public string text;
		public string type;
		public int avatar; // 0: none 1: girl 2: developer
	}

	Dictionary<int, Message> Messages;

	private static MessageController instance;
	public static MessageController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<MessageController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {
		Messages = new Dictionary<int, Message>();
		TextAsset t;
		if (LocPanelController.Instance.Language == "CN") {
			t = Resources.Load("message") as TextAsset ;
		} else {
			t = Resources.Load("message-en") as TextAsset ;
		}

		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(t.ToString()); // load the file.
		XmlNodeList msgList = xmlDoc.GetElementsByTagName("message"); // array of the level nodes.
		foreach (XmlNode msg in msgList) {
			XmlNodeList msgContent = msg.ChildNodes;
			Message curMsg = new Message();
			curMsg.id = -1;
			curMsg.avatar = 0;
			foreach (XmlNode msgInfo in msgContent) {
//				print (msgInfo.InnerText);
				if (msgInfo.Name == "id") {
					curMsg.id = int.Parse(msgInfo.InnerText);
				} else if (msgInfo.Name == "nextMessage") {
					string[] tmpArray = msgInfo.InnerText.Split(',');
					curMsg.nextMessage = new List<int>();
					foreach (string str in tmpArray) {
						curMsg.nextMessage.Add(int.Parse(str));
					}
				} else if (msgInfo.Name == "text") {
					curMsg.text = msgInfo.InnerText;
				} else if (msgInfo.Name == "type") {
					curMsg.type = msgInfo.InnerText;
				} else if (msgInfo.Name == "avatar") {
					curMsg.avatar = int.Parse(msgInfo.InnerText);
				}
			}
//			print (curMsg.id);
			Messages.Add(curMsg.id, curMsg);
		}
	}
	
	public Message GetMessage(int id) {
		if (id < 0) {
			return new Message();
		}

		return Messages[id];
	}
}
