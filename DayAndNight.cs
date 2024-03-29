using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
	[Range(0, 1)]
	public float time;
	public float fullDayLength;
	public float startTime = 0.4f;
	public float timeRate;
	public Vector3 noon;

	[Header("Sun")]
	public Light sun;
	public Gradient sunColor;
	public AnimationCurve sunIntensity;

	[Header("Moon")]
	public Light moon;
	public Gradient moonColor;
	public AnimationCurve moonIntensity;

	[Header("Other Seetings")]
	public AnimationCurve lightingIntensityMultiplier;
	public AnimationCurve reflectionIntensityMultiplier;

	[SerializeField]
	private GameObject clouds;
	[SerializeField]
	private GameObject lightShafts;

	[SerializeField]
	private Color dayColor;
	[SerializeField]
	private Color nightColor;

	//private float duration = 2.0f;

	// Start is called before the first frame update
	void Start()
	{
		timeRate = 1 / fullDayLength;
		time = startTime;
		RenderSettings.fog = true;

		StartCoroutine(UpdateCycle());
	}

	IEnumerator UpdateCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(Time.deltaTime);

			time += timeRate * Time.deltaTime;

			if (time >= 1)
				time = 0;

			sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
			moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

			sun.intensity = sunIntensity.Evaluate(time);
			moon.intensity = moonIntensity.Evaluate(time);

			sun.color = sunColor.Evaluate(time);
			moon.color = moonColor.Evaluate(time);

			if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
			{
				sun.gameObject.SetActive(false);
			}
			else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
			{
				sun.gameObject.SetActive(true);
			}

			if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
			{
				moon.gameObject.SetActive(false);
			}
			else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
			{
				moon.gameObject.SetActive(true);
			}

			if (!moon.gameObject.activeSelf)
			{
				RenderSettings.fogColor = dayColor;
				clouds.SetActive(true);
				lightShafts.SetActive(true);
			}
			else
			{
				RenderSettings.fogColor = nightColor;
				clouds.SetActive(false);
				lightShafts.SetActive(false);
			}

			RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
			RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
		}
	}
}
