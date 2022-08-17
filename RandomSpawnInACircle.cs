using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
	public Transform spawnpoint;
	public float radius = 23f;
	public GameObject player;

	void Start()
	{
		Instantiate(player, RandomSpawn(spawnpoint.position), spawnpoint.rotation);
	}

	Vector2 RandomSpawn(Vector2 spawnPoint)
	{
		Vector2 rpoc = Random.insideUnitCircle * radius;
		rpoc += spawnPoint;
		return rpoc;
	}
}
