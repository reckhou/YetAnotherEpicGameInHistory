using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillCountController : MonoBehaviour {

	public GameObject KillCountText;

	private static KillCountController instance;
	public static KillCountController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<KillCountController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {

	}

	// Update is called once per frame
	void Update () {
		string killStr = GameController.Instance.Kill.ToString() + "/" + GameDirector.Instance.GetNextGoal("kill");
		KillCountText.GetComponent<Text>().text = killStr;
	}
}
