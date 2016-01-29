using UnityEngine;
using System.Collections;
using EnemyInfo;
using Shell;
using UnityEngine.UI;
using UIInfo;

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
		bool PowerUpActivable = true;

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
			if (collider.CompareTag ("PowerUp") && PowerUpActivable) 
			{
				StartCoroutine (SpeedPowerUp (PowerUpSpeedBonus, PowerUpDuration, PowerUpCooldown));
			} 
			else 
			{
				StartCoroutine (PowerUpMessage ("POWERUP ON COOLDOWN", 3f));
			}
		}

		IEnumerator SpeedPowerUp(float bonus, float bonusDuration, float cooldown)
		{
			//MessageText = PowerUpMessages.instance.PowerUpMessageText.text;

			StartCoroutine(PowerUpMessage("SPEED POWERUP ACTIVATED! +5 SPEED FOR 5 SECONDS", 5f));

			PowerUpActivable = false;

			Speed += bonus;

			yield return new WaitForSeconds (PowerUpDuration);

			Speed -= bonus;

			StartCoroutine (PowerUpMessage ("SPEED POWERUP FINISH! 10 SECOND COOLDOWN", 3f));

			//StartCoroutine (PowerUpMessage ("POWERUP ON COOLDOWN", 3f));

			yield return new WaitForSeconds (cooldown);

			PowerUpActivable = true;
		}

		IEnumerator PowerUpMessage (string message, float delay) 
		{
			PowerUpMessages.instance.PowerUpMessageText.text = message;
			PowerUpMessages.instance.PowerUpMessageText.enabled = true;
			yield return new WaitForSeconds (delay);
			PowerUpMessages.instance.PowerUpMessageText.enabled = false;
		}

	}
}