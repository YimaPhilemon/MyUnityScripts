using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
	public ScreenOrientation targetOrientation = ScreenOrientation.Portrait;
	public void ChangeScreenOrienation()
	{
		if (Screen.orientation == ScreenOrientation.Portrait)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		else
		{
			Screen.orientation = ScreenOrientation.Portrait;
		}
	}
}
