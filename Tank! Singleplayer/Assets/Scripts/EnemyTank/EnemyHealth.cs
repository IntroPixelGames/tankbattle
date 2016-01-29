using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerInfo;
using Shell;
using Management;

namespace EnemyInfo
{
	public class EnemyHealth : MonoBehaviour 
	{
		public float StartHealth = 100f;
		public Slider Slider;
		public Image FillImage;
		public Color FullHealth = Color.green;
		public Color ZeroHealth = Color.red;
		public GameObject ExplosionPrefab;
		public bool tookDamage = false;

		AudioSource ExplosionAudio;
		ParticleSystem ExplosionParticles;
		[HideInInspector] public float CurrentHealth;
		[HideInInspector] public bool Dead;

		void Awake ()
		{
			ExplosionParticles = Instantiate (ExplosionPrefab).GetComponent<ParticleSystem> ();

			ExplosionAudio = ExplosionParticles.GetComponent<AudioSource> ();

			ExplosionParticles.gameObject.SetActive (false);
		}

		void OnEnable ()
		{
			CurrentHealth = StartHealth;
			Dead = false;

			SetHealthUI ();
		}

		public void TakeDamage (float amount)
		{
			CurrentHealth -= amount;

			SetHealthUI ();

			if (CurrentHealth <= 0f && !Dead) 
			{
				OnDeath ();
			}

			tookDamage = true;
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.End)) 
			{
				CurrentHealth = 0;
				OnDeath ();
			}
		}

		void SetHealthUI ()
		{
			Slider.value = CurrentHealth;

			FillImage.color = Color.Lerp (ZeroHealth, FullHealth, CurrentHealth / StartHealth);
		}

		void OnDeath ()
		{
			Dead = true;

			ExplosionParticles.transform.position = transform.position;
			ExplosionParticles.gameObject.SetActive (true);

			ExplosionParticles.Play ();

			ExplosionAudio.Play ();
			gameObject.SetActive (false);
		}
	}
}