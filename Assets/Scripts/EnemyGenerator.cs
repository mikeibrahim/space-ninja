using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
	[SerializeField] private Enemy[] enemy;
	private float spawnX = 7;
	private float spawnY = -5;
	private float spawnMin  = 0.2f;
	private float spawnMax = 1.4f;
	private float velMin  = 10f;
	private float velMax = 15f;
	float spawnInterval;
	Vector3 spawnPos;

    void Update() {
		if (spawnInterval <= 0) {
			NewSpawnPos(); // Get New Spawn Position
			SpawnEnemy();
			NewSpawnInterval(); // Get New Spawn Time
		} else {
			spawnInterval -= Time.deltaTime;
		}
    }

	private void SpawnEnemy() {
		Enemy e = Instantiate(enemy[RandNum()], spawnPos, transform.rotation); // Spawn
		e.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(velMin, velMax), ForceMode.Impulse); // Apply Force
	}

	private Vector3 NewSpawnPos() {
		return spawnPos = new Vector3(Random.Range(-spawnX, spawnX), spawnY, 0);
	}

	private void NewSpawnInterval() {
		spawnInterval = Random.Range(spawnMin, spawnMax);
	}

	private int RandNum() {
		return Random.Range(0, enemy.Length);
	}
}
