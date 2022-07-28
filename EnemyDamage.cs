using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float maxDamagePoint = 20;
	public float minDamagePoint = 5;
	public GameObject OBJ;
	public GameObject spark;
	[HideInInspector]
    public health life;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
		{
			life = other.gameObject.GetComponent<health>();
			life.TakeDamage(Random.Range(minDamagePoint, maxDamagePoint));
		}
		Instantiate(spark, this.transform.position, Quaternion.identity);
		Destroy(OBJ);
		Destroy(gameObject);
	}
}
