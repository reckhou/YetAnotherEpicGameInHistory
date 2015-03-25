using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocPanelController : MonoBehaviour {
	private static LocPanelController instance;
	public static LocPanelController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<LocPanelController>();
			}
			
			return instance;
		}
	}

	float holdTime;
	bool LangSelected;
	public string Language;

	public GameObject InstructionCN;
	public GameObject InstructionEN;

	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.PS4) {
			InstructionCN.GetComponent<Text>().text = "长按O键2秒选择中文";
			InstructionEN.GetComponent<Text>().text = "Press O for English";
		} else {
			InstructionCN.GetComponent<Text>().text = "长按Z键2秒选择中文";
			InstructionEN.GetComponent<Text>().text = "Press Z for English";
		}
	}
	
	void Update() {
		if (LangSelected) {
			return;
		}

		if (Input.GetKey(InputController.Instance.ActionKey)) {
			holdTime += Time.deltaTime;
		}

		if (holdTime > 2.0f) {
			LangSelected = true;
			Language = "CN";
			InputController.Instance.AllowInput(true);
			GameController.Instance.Init();
			this.gameObject.SetActive(false);
			return;
		}

		if (Input.GetKeyUp(InputController.Instance.ActionKey)) {
			LangSelected = true;
			Language = "EN";
			InputController.Instance.AllowInput(true);
			GameController.Instance.Init();
			this.gameObject.SetActive(false);
			return;
		}
	}
}
