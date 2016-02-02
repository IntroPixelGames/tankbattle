using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
	public Slider VolumeSlider;

	void OnEnable()
	{
		AudioListener.volume = 1;
		VolumeSlider.value = 1;
	}

	public void Update()
	{
		AudioListener.volume = VolumeSlider.value;
	}
}

