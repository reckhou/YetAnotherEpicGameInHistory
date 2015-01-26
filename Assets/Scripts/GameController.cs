using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public List<GameObject> EnemyPrefab;
	public List<GameObject> Enemies;
	public GameObject BattleLayer;
	public int AttackPower = 1;
	public float SpawnSpeed = 1.0f;
	public int MaxSpawn = 10;
	public GameObject Hand;

	float LastSpawn;

	bool SpawnEnabled;

	public int Kill;
	public int Money;
	public int Exp;
	public int Level;

	private static GameController instance;
	public static GameController Instance {
		get { 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameController>();
			}
			
			return instance;
		}
	}

	// Use this for initialization
	public void Init () {
		MessageController.Instance.Init();
		AchievementController.Instance.Init();
		GameDirector.Instance.Init();
		Enemies = new List<GameObject>();
		UIController.Instance.Init();
		// newbie
		ChatBoxController.Instance.ShowMessage(0, 0);
		AchievementController.Instance.FinishAchievement(997);
//		InputController.Instance.AllowInput(true);
		SpawnEnemy();
//		SpawnEnabled = true;
		Level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Enemies.Count < 10 && Kill > 1) {
			for (int i = 0; i < 10; i++) {
				SpawnEnemy();
			}
		}

		if (Time.time - LastSpawn > 1.0f / (SpawnSpeed)&& Enemies.Count < MaxSpawn && SpawnEnabled) {
			for (int i = 0; i < AttackPower; i++) {
				SpawnEnemy();
			}
		}

		if (AttackPower > 100) {
			AchievementController.Instance.FinishAchievement(50);
		}
	}

	public void EnableSpawn(bool enable) {
		SpawnEnabled = enable;
	}

	public void SpawnEnemy() {
		Vector3 spawnPoint = new Vector3(Tools.Random(-5f, 5f), Tools.Random(-3f, -1.8f), -0.1f);
//		print (spawnPoint);
		int enemyID = Tools.Random(0, EnemyPrefab.Count);
		bool flip = Tools.Random();
		GameObject enemy = Instantiate (EnemyPrefab[enemyID], spawnPoint, Quaternion.identity)  as GameObject;
		Enemies.Add(enemy);
		LastSpawn = Time.time;
		enemy.transform.parent = BattleLayer.transform;
		enemy.transform.localPosition = spawnPoint;
		if (flip) {
			enemy.GetComponent<CharController>().Flip();
		}

		for (int i = 0; i < Enemies.Count; i++) {
			Enemies[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = 255 - i;
		}

		float randomScale = Tools.Random(0.5f, 1.5f);
		Vector3 scale = enemy.transform.localScale;
		scale.x *= randomScale;
		scale.y *= randomScale;
		enemy.transform.localScale = scale;
	}
	public void Hit() {
		
		if (AttackPower >= 4 && AttackPower <= 15) {
			CameraControlelr.Instance.Shake();
		} else if (AttackPower > 15) {
			CameraControlelr.Instance.ShakeBig();
		}

		if (Enemies.Count < 1) {
			return;
		}
		Kill += AttackPower;
		for (int i = 0; i < AttackPower; i++) {
			if (Enemies.Count < 1) {
				return;
			}
			GameObject toKill = Enemies[0];
			Enemies.Remove(toKill);
			toKill.GetComponent<CharController>().Kill();
			Hand.GetComponent<HandController>().Shoot();
			// TODO: exp, money, level...
			Exp++;
			if (Exp == 30 && Level < 10) {
				LevelUp();
			} else if (Exp == 60 && Level < 20 && Level >= 10) {
				LevelUp();
			} else if (Exp == 160 && Level < 30) {
				LevelUp();
			} else if (Exp == 580 && Level < 50) {
				LevelUp();
			} else if (Exp == 1000 && Level < 1000) {
				LevelUp();
			}
			// Check event
			GameDirector.Instance.CheckEvent();
		}
	}

	void LevelUp() {
		Level++;
		Exp = 0;
	}
}
