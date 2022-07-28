using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    public Volume vignette;
    public Animation pulsating;
    float shakeTimer;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }


    public void IntCS(float intensity, float timer)
	{
        CinemachineBasicMultiChannelPerlin CBMP =
           cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CBMP.m_AmplitudeGain = intensity;
        shakeTimer = timer;
    }

    public void VignetteActivation(bool trigger)
	{
        if(trigger)
		{
            vignette.enabled = true;
            pulsating.Play();
		}
		else
		{
            vignette.enabled = false;
            pulsating.Stop();
        }
	}



    private void Update()
	{
		if(shakeTimer > 0)
		{
            shakeTimer -= Time.deltaTime;
		}
        else if (shakeTimer <= 0)
        {
            CinemachineBasicMultiChannelPerlin CBMP =
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            CBMP.m_AmplitudeGain = 0;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
