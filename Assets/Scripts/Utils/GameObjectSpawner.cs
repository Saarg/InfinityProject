using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnRate
{
	cons,
	x,
	xsquare,
	invx,
	invsquare
};

public class GameObjectSpawner : MonoBehaviour {

	public GameObject[] spawns;

	private GameObject _spawned;
	public SpawnRate spawnRateCurve;

	public float spawnRate;
	private float nextSpawn;
	private int spawnCount = 0;
	public bool waitForDeath;
	public bool spawnOnDeath;

	private float lastSpawn;

	// Use this for initialization
	void Start () {
		lastSpawn = Time.realtimeSinceStartup + Random.Range(0, spawnRate/10);

		_spawned = Instantiate (spawns [Random.Range(0, spawns.Length-1)], transform.position, transform.rotation);

		switch (spawnRateCurve) {
		case SpawnRate.cons:
			nextSpawn = spawnRate;
			break;
		case SpawnRate.x:
			nextSpawn = spawnCount*spawnRate;
			break;
		case SpawnRate.xsquare:
			nextSpawn = spawnCount*spawnCount*spawnRate;
			break;
		case SpawnRate.invx:
			nextSpawn = spawnRate/spawnCount;
			break;
		case SpawnRate.invsquare:
			nextSpawn = spawnRate/(spawnCount*spawnCount);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (waitForDeath && _spawned != null) {
			lastSpawn = Time.realtimeSinceStartup;

			return;
		}

		if (spawnOnDeath && _spawned == null) {
			nextSpawn = 0;
		}

		if (Time.realtimeSinceStartup - lastSpawn > nextSpawn) {
			lastSpawn = Time.realtimeSinceStartup;

			_spawned = Instantiate (spawns [Random.Range(0, spawns.Length-1)], transform.position, transform.rotation);
			spawnCount++;

			switch (spawnRateCurve) {
			case SpawnRate.cons:
				nextSpawn = spawnRate;
				break;
			case SpawnRate.x:
				nextSpawn = spawnCount*spawnRate;
				break;
			case SpawnRate.xsquare:
				nextSpawn = spawnCount*spawnCount*spawnRate;
				break;
			case SpawnRate.invx:
				nextSpawn = spawnRate/spawnCount;
				break;
			case SpawnRate.invsquare:
				nextSpawn = spawnRate/(spawnCount*spawnCount);
				break;
			}
		}
	}
}
