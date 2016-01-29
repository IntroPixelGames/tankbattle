using UnityEngine;
using System.Collections;

public class MainMenu_Controls : MonoBehaviour
{
	public GameObject playButton;
	public GameObject quitButton;
	public GameObject contButton;
	public GameObject controls;

	private bool isShowing;

	void Awake ()
	{
		controls.SetActive (false);
	}

	public void RemoveButtons ()
	{
		playButton.SetActive (false);
		quitButton.SetActive (false);
		contButton.SetActive (false);
	}

	public void ShowControls ()
	{
		RemoveButtons ();

		isShowing = !isShowing;
		controls.SetActive (isShowing);
	}

	public void goBack ()
	{
		controls.SetActive (false);
		playButton.SetActive (true);
		quitButton.SetActive (true);
		contButton.SetActive (true);
	}
}

