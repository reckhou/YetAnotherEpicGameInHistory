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
	void Start () {
		MessageController.Instance.Init();
		UIController.Instance.Init();
		AchievementController.Instance.Init();
		GameDirector.Instance.Init();
		Enemies = new List<GameObject>();
//		GameDirector.Instance.Event(0);
		InputController.Instance.AllowInput(true);
		SpawnEnemy();
		SpawnEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - LastSpawn > 1.0f / (SpawnSpeed)&& Enemies.Count < MaxSpawn * AttackPower && SpawnEnabled) {
			for (int i = 0; i < AttackPower; i++) {
				SpawnEnemy();
			}
		}
	}

	public void EnableSpawn(bool enable) {
		SpawnEnabled = enable;
	}

	void SpawnEnemy() {
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

		float randomScale = Tools.Random(0.8f, 1.2f);
		Vector3 scale = enemy.transform.localScale;
		scale.x *= randomScale;
		scale.y *= randomScale;
		enemy.transform.localScale = scale;
	}

	public void Hit() {
		if (Enemies.Count < 1) {
			return;
		}

		for (int i = 0; i < AttackPower; i++) {
			if (Enemies.Count < 1) {
				return;
			}
			GameObject toKill = Enemies[0];
			Enemies.Remove(toKill);
			toKill.GetComponent<CharController>().Kill();
			Kill++;
			Hand.GetComponent<HandController>().Shoot();
			// TODO: exp, money, level...
			// Check event
			GameDirector.Instance.CheckEvent();
		}


	}
}
