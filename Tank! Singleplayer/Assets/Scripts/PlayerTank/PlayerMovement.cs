using UnityEngine;
using System.Collections;
using EnemyInfo;
using Shell;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PlayerInfo
{
	public class PlayerMovement : MonoBehaviour 
	{
		public float Speed = 10f;
		public float TurnSpeed = 180f;
		public AudioSource MovementAudio;
		public AudioClip EngineIdling;
		public AudioClip EngineDriving;
		public float PowerUpSpeedBonus = 5f;
		public float PowerUpDuration = 5f;
		public float PowerUpCooldown = 10f;

		string MovementAxisName;
		string TurnAxisName;
		Rigidbody Rigidbody;
		float MovementInputValue;
		float TurnInputValue;
		PowerUps PowerUp;

		void Awake ()
		{
			Rigidbody = GetComponent<Rigidbody> ();
		}

		void OnEnable ()
		{
			Rigidbody.isKinematic = false;

			MovementInputValue = 0f;
			TurnInputValue = 0f;
		}

		void OnDisable ()
		{
			Rigidbody.isKinematic = true;
		}

		void Start ()
		{
			MovementAxisName = "Vertical";
			TurnAxisName = "Horizontal";
		}

		void Update ()
		{
			MovementInputValue = Input.GetAxis (MovementAxisName);
			TurnInputValue = Input.GetAxis (TurnAxisName);

			EngineAudio ();
		}

		void EngineAudio ()
		{
			// If there is no input (the tank is stationary)...
			if (Mathf.Abs (MovementInputValue) < 0.1f && Mathf.Abs (TurnInputValue) < 0.1f)
			{
				// ... and if the audio source is currently playing the driving clip...
				if (MovementAudio.clip == EngineDriving)
				{
					// ... change the clip to idling and play it.
					MovementAudio.clip = EngineIdling;
					MovementAudio.Play ();
				}
			}
			else
			{
				// Otherwise if the tank is moving and if the idling clip is currently playing...
				if (MovementAudio.clip == EngineIdling)
				{
					// ... change the clip to driving and play.
					MovementAudio.clip = EngineDriving;
					MovementAudio.Play();
				}
			}
		}

		void FixedUpdate ()
		{
			Move ();
			Turn ();
		}

		void Move ()
		{
			Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

			Rigidbody.MovePosition (Rigidbody.position + movement);
		}

		void Turn ()
		{
			float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			Rigidbody.MoveRotation (Rigidbody.rotation * turnRotation);
		}

		void OnTriggerEnter (Collider collider)
		{
			List<int> PowerUpsPossible = new List<int> ();

			if (collider.CompareTag("PowerUp") && PowerUp.isActivable_speed) 
			{
				PowerUpsPossible.Add (1);
			}
			if (collider.CompareTag("PowerUp") && PowerUp.isActivable_health) 
			{
				PowerUpsPossible.Add (2);
			}
			if (collider.CompareTag("PowerUp") && PowerUp.isActivable_damage) 
			{
				PowerUpsPossible.Add (3);
			}
			if (PowerUpsPossible.Count < 1)
			{
				StartCoroutine(PowerUp.PowerUpMessage("ALL POWERUPS ON COOLDOWN", 4));
			}

			int PowerUpRand = Random.Range (0, PowerUpsPossible.Count);

			switch (PowerUpsPossible [PowerUpRand]) 
			{
				case 1:
					StartCoroutine (PowerUp.PlayerSpeedPowerUp());
					break;
				case 2:
					StartCoroutine (PowerUp.PlayerHealthPowerUp());
					break;
				case 3: 
					StartCoroutine (PowerUp.PlayerDamagePowerUp());
					break;
			}

			Debug.Log (PowerUpsPossible);
		}
	}
}