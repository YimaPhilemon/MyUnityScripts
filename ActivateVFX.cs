using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVFX : MonoBehaviour
{

    public GameObject vfx;
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			vfx.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			StartCoroutine(VFXDisable());
		}
	}

	IEnumerator VFXDisable()
	{
		yield return new WaitForSeconds(2f);
		vfx.SetActive(false);
	}
}
