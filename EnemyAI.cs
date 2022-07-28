using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour, Entitity
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
	//public Text NPCtext;
	public Animator Zombie;
	public float damagex =-1;
	public Slider slide;
	public bool EnemyDead = false;
	public bool isBoss1;
	public bool isBoss2;
	public bool isBoss3;
	public bool isBoss4;
	public bool isBoss5;
	public bool isBoss6;

	//public AudioSource EnemyHurt;


	void Awake() => Zombie = GetComponent<Animator>();

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
	public void Update()
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


		slide.value = npcHP;
		r.velocity *= 0.99f;
		if(npcHP<=0)
		{
			EnemyDead = true;
			movementSpeed = 0;
			if(isBoss1  && !isBoss2 && !isBoss3 && !isBoss4 && !isBoss5 && !isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 1;
				isBoss1 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else if (!isBoss1 && isBoss2 && !isBoss3 && !isBoss4 && !isBoss5 && !isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 2;
				isBoss2 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else if (!isBoss1 && !isBoss2 && isBoss3 && !isBoss4 && !isBoss5 && !isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 3;
				isBoss3 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else if (!isBoss1 && !isBoss2 && !isBoss3 && isBoss4 && !isBoss5 && !isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 4;
				isBoss4 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else if (!isBoss1 && !isBoss2 && !isBoss3 && !isBoss4 && isBoss5 && !isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 5;
				isBoss5 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else if (!isBoss1 && !isBoss2 && !isBoss3 && !isBoss4 && !isBoss5 && isBoss6)
			{
				EnemySpawnManager.Instance.BossCheck = 6;
				isBoss6 = false;
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			else
			{
				Zombie.SetBool("EnemyDead", EnemyDead);
				Destroy(this.gameObject, 10);
			}
			
		}
		if (npcHP > 0)
		{
			//slide.value = npcHP;
			EnemyDead = false;
		}
		if (movementSpeed<=0)
		{
			movementSpeed = 0;
		}
		//NPCtext.text = npcHP.ToString();
		if(gamemanagerscript.islastshot)
		{
			movementSpeed = 0;
			Destroy(this.gameObject);
		}
		if(gamemanagerscript._timeRemaining == 0)
		{
			Destroy(this.gameObject);
		}
		/*Vector3 movement = this.transform.position;

		float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
		float velocityX = Vector3.Dot(movement.normalized, transform.right);

		Zombie.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
		Zombie.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);	 */
	}

	

	public void SlowDownEnermy(float Slow)
	{
		if (movementSpeed > 0)
		{
			movementSpeed = movementSpeed - Slow;
			StartCoroutine(ResetSpeed());
		}

	}

	public void DamageReceiver(float damage)
	{
		if(npcHP > 0)
		{
			npcHP -= damage;
			//EnemyHurt.Play();
		}
	}

	

	IEnumerator ResetSpeed()
	{
		yield return new WaitForSeconds(10);
		movementSpeed = 0.3f;
	}

	public void EnemyDestroy()
	{
		StartCoroutine(EnemyDestroyCo());
	}

	IEnumerator EnemyDestroyCo()
	{
		yield return new WaitForSeconds(1f);
		npcHP -= 100;
		EnemyDead = true;
		movementSpeed = 0;
		Zombie.SetBool("EnemyDead", EnemyDead);
		Destroy(this.gameObject, 10);
	}
}
