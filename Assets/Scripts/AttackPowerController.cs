using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackPowerController : MonoBehaviour {
	public int AttackPower;
	public GameObject AttackPowerText;

	private static AttackPowerController instance;
	public static AttackPowerController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<AttackPowerController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Set(GameController.Instance.AttackPower);
	}

	public void Set(int power) {
		AttackPower = power;
		string pwrStr = AttackPower.ToString();
		AttackPowerText.GetComponent<Text>().text = pwrStr;
	}
}
