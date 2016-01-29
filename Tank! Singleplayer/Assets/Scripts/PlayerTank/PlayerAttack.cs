using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EnemyInfo;
using Shell;

namespace PlayerInfo
{
	public class PlayerAttack : MonoBehaviour 
	{
		public Rigidbody Shell;
		public Transform FireTransform;
		public Slider AimSlider;
		public AudioSource ShootingAudio;
		public AudioClip ChargingClip;
		public AudioClip FireClip;
		public float MinForce = 15f;
		public float MaxForce = 30f;
		public float MaxCharge = 0.75f;
		public bool Fired;

		string FireButton;
		float CurrentForce;
		float ChargeSpeed;

		void OnEnable ()
		{
			CurrentForce = MinForce;
			AimSlider.value = MinForce;
		}

		void Start ()
		{
			FireButton = "Fire";

			ChargeSpeed = (MaxForce - MinForce) / MaxCharge;
		}

		void Update ()
		{
			AimSlider.value = MinForce;

			if (CurrentForce >= MaxForce && !Fired) 
			{
				CurrentForce = MaxForce;
				Fire ();
			} 
			else if (Input.GetButtonDown (FireButton)) 
			{
				Fired = false;
				CurrentForce = MinForce;

				ShootingAudio.clip = ChargingClip;
				ShootingAudio.Play ();
			} 
			else if (Input.GetButton (FireButton) && !Fired) 
			{
				CurrentForce += ChargeSpeed * Time.deltaTime;

				AimSlider.value = CurrentForce;
			} 
			else if (Input.GetButtonUp (FireButton) && !Fired) 
			{
				Fire ();
			}
		}

		public void Fire ()
		{
			Fired = true;

			Rigidbody shellInstance = Instantiate (Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;

			shellInstance.velocity = CurrentForce * FireTransform.forward;

			ShootingAudio.clip = FireClip;
			ShootingAudio.Play ();

			CurrentForce = MinForce;
		}
	}
}