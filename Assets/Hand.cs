using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
  public void Shoot (float speed)
  {
    GetComponent<Animator> ().speed = speed;

    GetComponent<Animator> ().CrossFade ("shoot", 0.01f);
  }

  public void Idle ()
  {
    GetComponent<Animator> ().speed = 1.0f;
    GetComponent<Animator> ().Play ("idle");
  }
}
