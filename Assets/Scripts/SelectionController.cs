using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionController : MonoBehaviour {
	public int id;
	public int NextMessageID;

	public void Init() {
		MessageController.Message msg = MessageController.Instance.GetMessage(id);
		if (msg.nextMessage == null) {
			gameObject.SetActive(false);
			return;
		}
		this.GetComponentInChildren<Text>().text = msg.text;
		if (msg.nextMessage.Count > 0) {
			NextMessageID = msg.nextMessage[0];
		} else {
			NextMessageID = -1;
		}
	}

	public void DoSelection() {
		ChatBoxController.Instance.DoSelection(NextMessageID);
	}
}
