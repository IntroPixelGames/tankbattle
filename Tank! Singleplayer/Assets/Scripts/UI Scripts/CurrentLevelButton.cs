using UnityEngine;
using System.Collections;

public class CurrentLevelButton : MonoBehaviour
{
	public void ResumeGame ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}

