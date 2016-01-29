using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour 
{
	public void LevelLoad(int LevelNumber)
	{
		Application.LoadLevel(LevelNumber);
	}
}
