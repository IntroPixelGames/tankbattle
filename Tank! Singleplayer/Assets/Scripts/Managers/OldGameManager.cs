/*using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
	public class OldGameManager : MonoBehaviour
	{
		public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
		public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
		public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
		public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
		public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
		public GameObject m_TankPrefab;             // Reference to the prefab the players will control.
		public OldTankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks.

		private int m_RoundNumber;                  // Which round the game is currently on.
		private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
		private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
		private OldTankManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
		private OldTankManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.

		private void Start()
		{
			m_StartWait = new WaitForSeconds (m_StartDelay);
			m_EndWait = new WaitForSeconds (m_EndDelay);

			SpawnAllTanks();
			SetCameraTargets();

			StartCoroutine (GameLoop ());
		}

		private void SpawnAllTanks()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].m_Instance =
					Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
				m_Tanks[i].m_PlayerNumber = i + 1;
				m_Tanks[i].Setup();
			}
		}

		private void SetCameraTargets()
		{
			Transform[] targets = new Transform[m_Tanks.Length];

			for (int i = 0; i < targets.Length; i++)
			{
				targets[i] = m_Tanks[i].m_Instance.transform;
			}

			m_CameraControl.m_Targets = targets;
		}

		private IEnumerator GameLoop ()
		{
			yield return StartCoroutine (RoundStarting ());

			yield return StartCoroutine (RoundPlaying());

			yield return StartCoroutine (RoundEnding());

			if (m_GameWinner != null)
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

			m_CameraControl.SetStartPositionAndSize ();

			m_RoundNumber++;
			m_MessageText.text = "ROUND " + m_RoundNumber;

			yield return m_StartWait;
		}

		private IEnumerator RoundPlaying ()
		{
			EnableTankControl ();

			m_MessageText.text = string.Empty;

			while (!OneTankLeft())
			{
				yield return null;
			}
		}

		private IEnumerator RoundEnding ()
		{
			// Stop tanks from moving.
			DisableTankControl ();

			m_RoundWinner = null;

			m_RoundWinner = GetRoundWinner ();

			if (m_RoundWinner != null)
				m_RoundWinner.m_Wins++;

			m_GameWinner = GetGameWinner ();

			string message = EndMessage ();
			m_MessageText.text = message;

			yield return m_EndWait;
		}

		private bool OneTankLeft()
		{
			// Start the count of tanks left at zero.
			int numTanksLeft = 0;

			for (int i = 0; i < m_Tanks.Length; i++)
			{
				if (m_Tanks[i].m_Instance.activeSelf)
					numTanksLeft++;
			}

			return numTanksLeft <= 1;
		}

		private TankManager GetRoundWinner()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				if (m_Tanks[i].m_Instance.activeSelf)
					return m_Tanks[i];
			}

			return null;
		}

		private TankManager GetGameWinner()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
					return m_Tanks[i];
			}

			return null;
		}

		private string EndMessage()
		{
			string message = "DRAW!";

			if (m_RoundWinner != null)
				message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

			// Add some line breaks after the initial message.
			message += "\n\n\n\n";

			for (int i = 0; i < m_Tanks.Length; i++)
			{
				message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
			}

			if (m_GameWinner != null)
				message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

			return message;
		}

		private void ResetAllTanks()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].Reset();
			}
		}

		private void EnableTankControl()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].EnableControl();
			}
		}

		private void DisableTankControl()
		{
			for (int i = 0; i < m_Tanks.Length; i++)
			{
				m_Tanks[i].DisableControl();
			}
		}
	}
} */