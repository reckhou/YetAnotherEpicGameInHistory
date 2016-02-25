using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour
{
  private static HandController instance;

  public static HandController Instance {
    get { 
      if (instance == null) {
        instance = GameObject.FindObjectOfType<HandController> ();
      }

      return instance;
    }
  }

  public List<Hand> _Hands;
		
  float lastShoot;
  int _HandCnt;

  public void Shoot ()
  {
    float now = Time.time;
    float deltaTime = now - lastShoot;
    float speed = 1.0f;
    if (deltaTime <= 0.25f && deltaTime > 0) {
      speed = 0.25f / deltaTime * 2;
    } else if (deltaTime <= 0) {
      return;
    } else if (deltaTime > 0.25f) {
      speed = 0.25f / deltaTime * 2;
    }

    for (int i = 0; i < _Hands.Count; i++) {
//      if (!_Hands [i].activeInHierarchy) {
//        continue;
//      }
      _Hands [i].Shoot (speed);
    }
    lastShoot = now;

    Vector3 pos = transform.localPosition;
    pos.x = Tools.Random (-4.0f, 4f);
    transform.localPosition = pos;
  }

  public void Idle ()
  {
    for (int i = 0; i < _Hands.Count; i++) {
//      if (!_Hands [i].activeInHierarchy) {
//        continue;
//      }
      _Hands [i].Idle ();
    }
  }

  public void AddHand ()
  {
    _HandCnt++;
  }

  public void SetHand (int count)
  {
    _HandCnt = count;
    if (count < 1) {
      _HandCnt = 1;
    }

    if (count > _Hands.Count) {
      _HandCnt = _Hands.Count;
    }
  }

  void Update ()
  {
    int handCnt = _HandCnt;
    for (int i = 1; i < _Hands.Count; i++) {

      handCnt--;
      if (handCnt > 0) {
        _Hands [i].gameObject.SetActive (true);
      } else {
        _Hands [i].gameObject.SetActive (false);
      }
    }

  }

}
