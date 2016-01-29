using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayerInfo;
using EnemyInfo;
using UnityEngine.SceneManagement;

namespace Management
{
	public class GameManager : MonoBehaviour
	{
		public int NumRoundsToWin = 5;            
		public float StartDelay = 3f;          
		public float EndDelay = 3f;              
		public CameraControl CameraControl;     
		public Text MessageText;                 
		public GameObject PlayerPrefab;       
		public GameObject EnemyPrefab;
		public TankManager[] Tanks;               

		private int RoundNumber;                 
		private WaitForSeconds StartWait;        
		private WaitForSeconds EndWait;          
		private TankManager RoundWinner;         
		private TankManager GameWinner;   

		private void Start()
		{
			StartWait = new WaitForSeconds (StartDelay);
			EndWait = new WaitForSeconds (EndDelay);

			SpawnAllTanks();
			SetCameraTargets();

			StartCoroutine (GameLoop ());
		}

		private void SpawnAllTanks()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				if (i == 0)
					Tanks[i].Instance = Instantiate (PlayerPrefab, Tanks[i].SpawnPoint.position, Tanks[i].SpawnPoint.rotation) as GameObject;
				else
					Tanks[i].Instance = Instantiate (EnemyPrefab, Tanks[i].SpawnPoint.position, Tanks[i].SpawnPoint.rotation) as GameObject;
				
				Tanks[i].Setup();
			}
		}

		private void SetCameraTargets()
		{
			Transform[] targets = new Transform[Tanks.Length];

			for (int i = 0; i < targets.Length; i++)
			{
				targets[i] = Tanks[i].Instance.transform;
			}

			CameraControl.Targets = targets;
		}

		private IEnumerator GameLoop ()
		{
			yield return StartCoroutine (RoundStarting ());

			yield return StartCoroutine (RoundPlaying());

			yield return StartCoroutine (RoundEnding());

			if (GameWinner != null)
			{
				Application.LoadLevel (Application.loadedLevel);
			}
			else
			{
				StartCoroutine (GameLoop ());
			}
		}

		private IEnumerator RoundStarting ()
		{
			ResetAllTanks ();
			DisableTankControl ();

			CameraControl.SetStartPositionAndSize ();

			RoundNumber++;
			MessageText.text = "ROUND " + RoundNumber;

			ResetAllTanks ();

			yield return StartWait;
		}

		private IEnumerator RoundPlaying ()
		{
			EnableTankControl ();

			MessageText.text = string.Empty;

			while (!OneTankLeft())
			{
				yield return null;
			}
		}

		private IEnumerator RoundEnding ()
		{
			// Stop tanks from moving.
			DisableTankControl ();

			RoundWinner = null;

			RoundWinner = GetRoundWinner ();

			if (RoundWinner != null)
				RoundWinner.Wins++;

			GameWinner = GetGameWinner ();

			string message = EndMessage ();
			MessageText.text = message;

			yield return EndWait;
		}

		private bool OneTankLeft()
		{
			// Start the count of tanks left at zero.
			int numTanksLeft = 0;

			for (int i = 0; i < Tanks.Length; i++)
			{
				if (Tanks[i].Instance.activeSelf)
					numTanksLeft++;
			}

			return numTanksLeft <= 1;
		}

		private TankManager GetRoundWinner()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				if (Tanks [i].Instance.activeSelf) 
				{
					return Tanks [i];
					#pragma warning disable
					Debug.Log (RoundWinner.ColoredPlayerText + " won round" + RoundNumber);
				}
			}

			return null;
		}

		private TankManager GetGameWinner()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				if (Tanks [i].Wins == NumRoundsToWin) 
				{
					return Tanks [i];
					#pragma warning disable
					Debug.Log (RoundWinner.ColoredPlayerText + "won the game");
				}
			}

			return null;
		}

		private string EndMessage()
		{
			string message = "DRAW!";

			if (RoundWinner != null)
				message = RoundWinner.ColoredPlayerText + " WINS THE ROUND!";

			// Add some line breaks after the initial message.
			message += "\n\n\n\n";

			for (int i = 0; i < Tanks.Length; i++)
			{
				message += Tanks[i].ColoredPlayerText + ": " + Tanks[i].Wins + " WINS\n";
			}

			if (GameWinner != null)
				message = GameWinner.ColoredPlayerText + " WINS THE GAME!";

			return message;
		}

		private void ResetAllTanks()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				Tanks[i].Reset();
			}
		}

		private void EnableTankControl()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				Tanks[i].EnableControl();
			}
		}

		private void DisableTankControl()
		{
			for (int i = 0; i < Tanks.Length; i++)
			{
				Tanks[i].DisableControl();
			}
		}
	}
}