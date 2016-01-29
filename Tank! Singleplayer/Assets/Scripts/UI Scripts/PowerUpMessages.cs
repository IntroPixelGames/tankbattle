using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UIInfo
{
	public class PowerUpMessages : MonoBehaviour
	{
		public static PowerUpMessages instance;

		public Text PowerUpMessageText;

		void Awake ()
		{
			instance = this;
		}

	}
}