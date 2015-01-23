using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChatBoxController : MonoBehaviour {
	public List<GameObject> Selections;
	public GameObject TextBox;
	public GameObject Indicator;

	bool isMessagePlaying;
	List<char> textBuffer;
	MessageController.Message msgBuffer;


	private static ChatBoxController instance;
	public static ChatBoxController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<ChatBoxController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {
		foreach (GameObject selection in Selections) {
			selection.SetActive(false);
		}
		textBuffer = new List<char>();
		SetVisible(false);
	}

	public void ShowMessage(int id) {
		SetVisible(true);
		textBuffer.Clear();

		if (id < 0) {
			SetVisible(false);
			return;
		}

		msgBuffer = MessageController.Instance.GetMessage(id);
		print (msgBuffer.text);
		TextBox.GetComponent<Text>().text = msgBuffer.text;
		if (msgBuffer.type == "selection") {
			Indicator.SetActive(false);
			for (int i = 0; i < msgBuffer.nextMessage.Count; i++) {
				GameObject selection = Selections[i];
				selection.SetActive(true);
				selection.GetComponent<SelectionController>().id = msgBuffer.nextMessage[i];
				selection.GetComponent<SelectionController>().Init();
			}
		} else {
			Indicator.SetActive(true);
		}
	}

	public void PlayNextMessage() {
		int nextMsgID = msgBuffer.nextMessage[0];
		if (nextMsgID < -1) {
			// TODO: Message Play done
		}
		ShowMessage(msgBuffer.nextMessage[0]);
	}

	public void DoSelection(int id) {
		ShowMessage(id);
		foreach (GameObject selection in Selections) {
			selection.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetVisible(bool visible) {
		this.gameObject.SetActive(visible);
	}
}
