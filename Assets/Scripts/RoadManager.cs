using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
	public GameObject[] roadPrefabs;

	private Transform playerTransform;
	private float spawnZ = 0f;
	private float roadLength = 100f;
	private int numberRoadsOnScreen = 4;

	private float safeZone = 100f;
	private List<GameObject> activeRoads;

	private int lastPrefabIndex;


	// Use this for initialization
	void Start ()
	{
		activeRoads = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < numberRoadsOnScreen; i++) {
			if (i < 1)
				SpawnRoad (0);
			else
				SpawnRoad ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerTransform.position.z - safeZone > (spawnZ - roadLength * numberRoadsOnScreen)) {
			SpawnRoad ();
			DeleteRoad ();
		}
	}

	void SpawnRoad (int prefabIndex = -1)
	{
		GameObject gameObject;
		if (prefabIndex == -1)
			gameObject = Instantiate (roadPrefabs [RandomPrefabIndex ()]) as GameObject;
		else
			gameObject = Instantiate (roadPrefabs [prefabIndex]) as GameObject;
		gameObject.transform.SetParent (transform);
		gameObject.transform.position = Vector3.forward * spawnZ;
		spawnZ += roadLength;
		activeRoads.Add (gameObject);
	}

	void DeleteRoad ()
	{
		Destroy (activeRoads [0]);
		activeRoads.RemoveAt (0);
	}

	int RandomPrefabIndex ()
	{
		if (roadPrefabs.Length <= 1) {
			return 0;
		}
		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex) {
			randomIndex = Random.Range (0, roadPrefabs.Length);
		}
		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}
