using UnityEngine;
using System.Collections;

public class SoundEffectContoller : MonoBehaviour {
	public AudioClip LevelUp;
	public AudioClip Achievement;
	public AudioClip Music1;
	public AudioClip Music2;
	public AudioSource sourceEffect;
	public AudioSource sourceMusic;
	private static SoundEffectContoller instance;
	public static SoundEffectContoller Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<SoundEffectContoller>();
			}
			
			return instance;
		}
	}


	public void PlayLevelUp() {
		sourceEffect.clip = LevelUp;
		sourceEffect.Play();
	}

	public void PlayAchievement() {
		sourceEffect.Stop();
		sourceEffect.clip = Achievement;
		sourceEffect.Play();
	}

	public void PlayMusic1() {
		sourceMusic.Stop();
		sourceMusic.clip = Music1;
		sourceMusic.Play();
	}

	public void PlayMusic2() {
		sourceMusic.Stop();
		sourceMusic.clip = Music2;
		sourceMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
