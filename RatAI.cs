using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatAI : MonoBehaviour
{
	public float attackDistance = 3f;
	public float movementSpeed = 4f;
	public float move;
	public float npcHP = 100;
	//How much damage will npc deal to the player
	public float npcDamage = 5;
	public float attackRate = 0.5f;

	//[HideInInspector]
	private Transform playerTransform;
	public Transform enemyTransform;
	[HideInInspector]
	//public SC_EnemySpawner es;
	NavMeshAgent agent;
	float nextAttackTime = 0;
	Rigidbody r;
	public bool enemyIsDead = false;
	public bool damagetaken = false;
	private GameMgr gamemanagerscript;
	// Start is called before the first frame update
	void Start()
    {
		gamemanagerscript = GameObject.Find("GameManager").GetComponent<GameMgr>();
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = attackDistance;
		agent.speed = move = movementSpeed;
		r = GetComponent<Rigidbody>();
		r.useGravity = false;
		r.isKinematic = true;
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update()
    {
		agent.speed = movementSpeed;
		if (agent.remainingDistance - attackDistance < 0.01f)
		{
			if (Time.time > nextAttackTime)
			{
				nextAttackTime = Time.time + attackRate;

			}
		}
		//Move towardst he player
		agent.destination = playerTransform.position;

		transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));

		if (gamemanagerscript.islastshot)
		{
			movementSpeed = 0;
			Destroy(this.gameObject);
		}
		if (gamemanagerscript._timeRemaining == 0)
		{
			Destroy(this.gameObject);
		}
	}
}
