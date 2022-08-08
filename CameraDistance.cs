using UnityEngine;
using Cinemachine;

public class CameraDistance : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    void Camera(bool Zoom)
	{
        //Zoom by 100%
        if(Zoom)
		{
            CinemachineComponentBase componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = 10f;
            }
        }
		else
		{
            CinemachineComponentBase componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = 20f;
            }
        }
    }
}
