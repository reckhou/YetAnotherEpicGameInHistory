using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillCountController : MonoBehaviour {
	public int KillCount;

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
	void Start () {
		
	}

	public void Add(int count) {
		KillCount += count;
		string killStr = KillCount.ToString() + "/10"; // TODO: find next goal from event controller
		KillCountText.GetComponent<Text>().text = killStr;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
