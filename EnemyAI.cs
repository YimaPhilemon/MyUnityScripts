using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Define an enum for boss types
public enum EnemyType 
{	
	None,
	Boss1,
	Boss2,
	Boss3,
	Boss4,
	Boss5, 
	Boss6,
	Rat
}

public class EnemyAI : MonoBehaviour
{
	
	public float attackDistance = 3f;
	public float movementSpeed = 4f;
	public float move;
	public float _health = 100;
	public float attackRate = 0.5f;		 
	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public NavMeshAgent agent;
	[HideInInspector]
	public float nextAttackTime = 0;
	public Animator Enemy;
	public Slider slide;

	public EnemyType enemyType;

	// Start is called before the first frame update
	void Start()
	{
		player = GameMgr.instance.shooter.transform;
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = attackDistance;
		agent.speed = move = movementSpeed;
	}

	private void OnEnable()
	{
		OnReactivation();
		GameMgr.OnLastshot += HandleLastShot;
		GameMgr.OnGameOver += HandleGameOver;
		GoalArea.OnSendDamage += HandleReceiveDamage;
		GoalArea.OnSlowDownEnemy += HandleSlowDown;
	}

	private void OnDisable()
	{
		GameMgr.OnLastshot -= HandleLastShot;
		GameMgr.OnGameOver -= HandleGameOver;
		GoalArea.OnSendDamage -= HandleReceiveDamage;
		GoalArea.OnSlowDownEnemy -= HandleSlowDown;
	}
	public virtual void HandleLastShot(bool isLastShot)
	{
		// Use the isLastShot parameter to determine behavior
		if (isLastShot) InstantDestroy();
	}

	public void OnReactivation()
	{
		if (slide != null)
			slide.value = _health;

		if (Health >= 100) return; 

		Health = 100;

		if (enemyType != EnemyType.Rat) Enemy.SetBool("EnemyDead", false);

		movementSpeed = move;
		agent.enabled = true;
	}

	public virtual void HandleGameOver() => InstantDestroy();
	public virtual void HandleReceiveDamage(float damage, bool redPause, bool hoopDamage)
	{
		if(redPause)
		{
			if (damage < 100 && hoopDamage)
			{
				switch (enemyType)
				{
					case EnemyType.Boss1:
						Health -= 10;
						break;
					case EnemyType.Boss2:
						Health -= 8;
						break;
					case EnemyType.Boss3:
						Health -= 6;
						break;
					case EnemyType.Boss4:
						Health -= 4;
						break;
					case EnemyType.Boss5:
						Health -= 2;
						break;
					case EnemyType.Boss6:
						Health -= 2;
						break;
					case EnemyType.Rat:
						break;
					default:
						Health -= damage;
						break;
				}
			}
			else
				Health -= damage;
		}
		else
		{
			Health -= damage;
			if (enemyType != EnemyType.Rat) Enemy.SetFloat("Damage", 1);
			DamageFeel();
		}
	}

	public virtual void HandleSlowDown(float speed)
	{
		if (movementSpeed > 0)
			movementSpeed = movementSpeed - speed;

		CancelInvoke(nameof(ResetSpeed));
		Invoke(nameof(ResetSpeed), 10f);
	}
	// Update is called once per frame
	public void Update() => AIMovement();

	public virtual void AIMovement()
	{
		if (Health <= 0) return;

		agent.speed = movementSpeed;
		if (agent.remainingDistance - attackDistance < 0.01f)
		{
			if (Time.time > nextAttackTime)
			{
				nextAttackTime = Time.time + attackRate;
			}
		}

		if (movementSpeed == 0) return;

		//Move towardst he player
		agent.destination = player.position;
		transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.position.z));
	}

	public float Health
	{
		get { return _health; }
		set
		{
			_health = value;
			if (_health <= 0)
			{

				movementSpeed = 0;
				agent.enabled = false;
				if (enemyType != EnemyType.Rat) Enemy.SetBool("EnemyDead", true);

				CancelInvoke();
				Invoke(nameof(InstantDestroy), 10f);
			}

			if (slide != null)
				slide.value = _health;
		}
	}

	public void InstantDestroy()
	{
		movementSpeed = 0;
		agent.enabled = false;
		Health = 0;
		if (enemyType != EnemyType.Rat) Enemy.SetBool("EnemyDead", true);
		PoolSystem.instance.DeactivateToPool(name, gameObject);
	}

	private void ResetSpeed() => movementSpeed = 0.3f;

	void DamageFeel()
	{
		movementSpeed = 0;
		CancelInvoke(nameof(Recover));
		Invoke(nameof(Recover), 5f);
	}
	void Recover()
	{
		if (enemyType != EnemyType.Rat) Enemy.SetFloat("Damage", -1);
		movementSpeed = move;
	}
}
