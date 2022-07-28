using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	private GameMgr gamemanagerscript;
	public AudioClip hitnetSound;
	private int RandomNum;

	public enum PowerupState
	{
		greenball,
		blueball,
		none
	}
	 PowerupState state;
	// Start is called before the first frame update
	void Start()
    {
		gamemanagerscript = GameObject.Find("GameManager").GetComponent<GameMgr>();
	}

    // Update is called once per frame
    void Update()
    {
		RandomNum = Random.Range(1, 100);
		if(RandomNum >= 1 && RandomNum <= 10)
		{
			state = PowerupState.greenball;
		}
		if (RandomNum >= 11 && RandomNum <= 20)
		{
			state = PowerupState.blueball;
		}
		if (RandomNum >= 21 && RandomNum <= 100)
		{
			state = PowerupState.none;
		}
		

		if (state == PowerupState.greenball)
		{
			//Activate PowerUP
			Debug.Log("isGreenball true");
			gamemanagerscript.isGreenball = true;
			gamemanagerscript.isBlueball = false;
			//gamemanagerscript.isRedball = false;

		}
		if (state == PowerupState.blueball)
		{
			//Activate PowerUP
			Debug.Log("isblueball true");
			gamemanagerscript.isGreenball = false;
			gamemanagerscript.isBlueball = true;
			//gamemanagerscript.isRedball = false;

		}
		if (state == PowerupState.none)
		{
			//Activate PowerUP
			Debug.Log("none true");
			gamemanagerscript.isGreenball = false;
			gamemanagerscript.isBlueball = false;;
			//gamemanagerscript.isRedball = false;

		}
		
	}

	/*void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "BasketBall")
		{
			if(state == PowerupState.greenball)
			{
				//Activate PowerUP
				Debug.Log("isGreenball true");
				gamemanagerscript.isGreenball = true;
				StartCoroutine(DeactivateBall());
				
				//AudioSource.PlayClipAtPoint(hitnetSound, gameObject.transform.position);
			}
			if (state == PowerupState.blueball)
			{
				//Activate PowerUP
				Debug.Log("isblueball true");
				gamemanagerscript.isBlueball = true;
				StartCoroutine(DeactivateBall());
				
				//AudioSource.PlayClipAtPoint(hitnetSound, gameObject.transform.position);
			}
			if (state == PowerupState.pinkball)
			{
				//Activate PowerUP
				Debug.Log("ispinkball true");
				gamemanagerscript.isPinkball = true;
				StartCoroutine(DeactivateBall());
				
				//AudioSource.PlayClipAtPoint(hitnetSound, gameObject.transform.position);
			}




		}
	}

	private IEnumerator DeactivateBall()
	{
		//gamemanagerscript.isGreenball = false;
		yield return new WaitForSeconds(10);
		if (state == PowerupState.greenball)
		{
			gamemanagerscript.isGreenball = false;
			state = PowerupState.blueball;
		}
		else if (state == PowerupState.blueball)
		{
			gamemanagerscript.isBlueball = false;
			state = PowerupState.pinkball;
		}
		else
		{
			gamemanagerscript.isPinkball = false;
			state = PowerupState.greenball;
		}

	}	*/
}
