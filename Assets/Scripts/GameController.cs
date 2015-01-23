﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject EnemyPrefab;
	public List<GameObject> Enemies;
	public GameObject BattleLayer;
	public int AttackPower = 1;

	float LastSpawn;

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
		ChatBoxController.Instance.Init();
		Enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - LastSpawn > 1f) {
			SpawnEnemy();
		}
	}

	void SpawnEnemy() {
		Vector3 spawnPoint = new Vector3(Tools.Random(-4.9f, 5.1f), Tools.Random(-3, 3), -0.1f);
		GameObject enemy = Instantiate (EnemyPrefab, spawnPoint, Quaternion.identity)  as GameObject;
		Enemies.Add(enemy);
		LastSpawn = Time.time;
		enemy.transform.parent = BattleLayer.transform;
		enemy.transform.localPosition = spawnPoint;
	}

	public void Hit() {
		if (Enemies.Count < 1) {
			return;
		}

		for (int i = 0; i < AttackPower; i++) {
			GameObject toKill = Enemies[0];
			Enemies.Remove(toKill);
			Destroy(toKill);
		}
	}
}