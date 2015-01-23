using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionController : MonoBehaviour {
	public int id;
	public int NextMessageID;

	public void Init() {
		MessageController.Message msg = MessageController.Instance.GetMessage(id);
		this.GetComponentInChildren<Text>().text = msg.text;
		NextMessageID = msg.nextMessage[0];
	}

	public void DoSelection() {
		ChatBoxController.Instance.DoSelection(NextMessageID);
	}
}
