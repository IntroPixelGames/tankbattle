using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;

	private bool isShowing;
	private bool paused;

	void Awake ()
	{
		pauseMenu.SetActive (false);
		Cursor.visible = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			Cursor.visible = true;

			isShowing = !isShowing;
			pauseMenu.SetActive (isShowing);

			paused = !paused;
		}

		if (paused) 
		{
			Time.timeScale = 0;
		}

		if (!paused) 
		{
			Time.timeScale = 1;
			Cursor.visible = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit();
		}
	}

	public void ResumeGame ()
	{
		if (paused && Input.GetKeyDown (KeyCode.P)) 
		{
			Time.timeScale = 1;
			Cursor.visible = false;
		}
	}
}

