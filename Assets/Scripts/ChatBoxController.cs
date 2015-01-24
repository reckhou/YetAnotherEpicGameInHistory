using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChatBoxController : MonoBehaviour {
	public List<GameObject> Selections;
	public GameObject TextBox;
	public GameObject Indicator;

	public int curEventID;

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

	public void ShowMessage(int id, int eventID) {
		SetVisible(true);
		textBuffer.Clear();

		if (id < 0) {
			UIController.Instance.Close();
			return;
		} else {
			UIController.Instance.Open(UIController.UIType.Chat);
		}
		curEventID = eventID;
		msgBuffer = MessageController.Instance.GetMessage(id);
		print (msgBuffer.text);
		TextBox.GetComponent<Text>().text = msgBuffer.text;
		if (msgBuffer.type == "selection") {
			for (int i = 0; i < msgBuffer.nextMessage.Count; i++) {
				GameObject selection = Selections[i];
				selection.SetActive(true);
				selection.GetComponent<SelectionController>().id = msgBuffer.nextMessage[i];
				selection.GetComponent<SelectionController>().Init();
			}
		}


	}

	public void PlayNextMessage() {
		if (msgBuffer.type == "selection") {
			DoSelection(msgBuffer.nextMessage[0]);
		} else {

			int nextMsgID = msgBuffer.nextMessage[0];
			if (nextMsgID < 0) {
				// TODO: Message Play done
				GameDirector.Instance.ChatEventDone(curEventID);
				UIController.Instance.Close();
				return;
			}
			ShowMessage(msgBuffer.nextMessage[0], curEventID);
		}
	}

	public void DoSelection(int id) {
		foreach (GameObject selection in Selections) {
			selection.SetActive(false);
		}

		MessageController.Message message = MessageController.Instance.GetMessage(id);
		if (message.nextMessage == null || message.nextMessage.Count < 1) {
			ShowMessage(-1, curEventID);
			return;
		}

		int nextID = MessageController.Instance.GetMessage(id).nextMessage[0];
		ShowMessage(nextID, curEventID);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetVisible(bool visible) {
		this.gameObject.SetActive(visible);
		if (!visible) {
			curEventID = 0;
		}
	}

	public void Switching() {
		Indicator.GetComponent<ProgressController>().Switching();
	}

	public void StopSwitch() {
		Indicator.GetComponent<ProgressController>().StopSwitch();
	}
}
