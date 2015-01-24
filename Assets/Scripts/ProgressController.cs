using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {
	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Switching() {
		animator.Play("switching");
	}

	public void StopSwitch() {
		animator.Play("normal");
	}

	public void SwitchFinish() {
		InputController.Instance.ClearKeyHold();
		animator.Play("normal");
		UIController.Instance.DoSwitch();
	}
}
