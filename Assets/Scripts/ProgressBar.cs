using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
	public float percentage = 1.0f;
	protected Transform bar;
	protected Transform barBg;

	private bool inited;

	// Use this for initialization
	void Start () {
		if (!inited) {
			init();
		}
	}

	private void init() {
		inited = true;
		bar = transform.FindChild("bar");
		barBg = transform.FindChild("barBg");
		SetPercentage(percentage);
	}

	public void SetPercentage(float Percentage) {
		if (!inited) {
			init ();
		}

		if (Percentage > 1.0f || Percentage < 0) {
			print ("Illegal percentage:"+Percentage);
			return;
		}
		percentage = Percentage;
		Vector3 scale = bar.localScale;
		scale.x = percentage;
		bar.localScale = scale;
	}

	public float GetPercentage() {
		return percentage;
	}
}
