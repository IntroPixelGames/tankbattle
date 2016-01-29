using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerInfo;
using EnemyInfo;
using System;

namespace Management
{
	[Serializable]
	public class TankManager_Med 
	{
		public Color PlayerColor;
		public Transform SpawnPoint;
		[HideInInspector] public GameObject Instance;
		[HideInInspector] public string ColoredPlayerText;
		[HideInInspector] public int Wins;

		private PlayerMovement PMove;
		private PlayerAttack PAttack;
		private EnemyMovement EMove;
		private Enemy_MedAttack EAttack;
		private GameObject CanvasGameObject;

		public void Setup ()
		{
			PMove = Instance.GetComponent<PlayerMovement> ();
			PAttack = Instance.GetComponent<PlayerAttack> ();
			EMove = Instance.GetComponent<EnemyMovement> ();
			EAttack = Instance.GetComponent<Enemy_MedAttack> ();
			CanvasGameObject = Instance.GetComponentInChildren<Canvas> ().gameObject;

			ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB (PlayerColor) + ">TANK " + "</color>";

			MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer> ();

			for (int i = 0; i < renderers.Length; i++)
			{
				renderers[i].material.color = PlayerColor;
			}
		}

		public void DisableControl ()
		{
			if ( PMove != null && PAttack != null )
			{
				PMove.enabled = false;
				PAttack.enabled = false;
			}

			if ( EMove != null && EAttack != null )
			{
				EMove.enabled = false;
				EAttack.enabled = false;
			}

			CanvasGameObject.SetActive( false );

		}

		public void EnableControl ()
		{
			if ( PMove != null && PAttack != null )
			{
				PMove.enabled = true;
				PAttack.enabled = true;
			}

			if ( EMove != null && EAttack != null )
			{
				EMove.enabled = true;
				EAttack.enabled = true;
			}

			CanvasGameObject.SetActive (true);
		}

		public void Reset ()
		{
			Instance.transform.position = SpawnPoint.position;
			Instance.transform.rotation = SpawnPoint.rotation;

			Instance.SetActive (false);
			Instance.SetActive (true);
		}
	}
}