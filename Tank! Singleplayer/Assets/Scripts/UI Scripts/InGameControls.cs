using UnityEngine;
using System.Collections;

public class InGameControls : MonoBehaviour 
{
	public void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();

		if (Input.GetKeyDown (KeyCode.P))
				Application.LoadLevel (5);
	}
}
