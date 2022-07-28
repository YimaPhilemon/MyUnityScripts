using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeToPlayer : MonoBehaviour
{
	public float DamagePoint = 5;

	public health life;
    // Start is called before the first frame update
    void Start()
    {
		life = GameObject.FindGameObjectWithTag("Player").GetComponent<health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/*private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			life.healthAmount -= DamagePoint;
		}
	}	*/

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			life.TakeDamage(DamagePoint);
		}
	}
}
