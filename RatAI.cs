using UnityEngine;

public class RatAI : EnemyAI
{
	public override void HandleReceiveDamage(float damage, bool redPause, bool hoopDamage) 
	{
		//if(!hoopDamage) PoolSystem.instance.DeactivateToPool(name, this.gameObject);
	}
	public override void HandleSlowDown(float speed) { }
	public override void AIMovement()
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
		agent.destination = player.position;
		transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.position.z));
	}
}
