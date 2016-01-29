using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerInfo;
using Shell;

namespace EnemyInfo
{
	public class Enemy_MedAttack : MonoBehaviour 
	{
		public float timeBetweenAttacks = 0.75f;
		public float attackDamage;
		public Rigidbody Shell;
		public Transform FireTransform;
		public AudioSource ShootingAudio;
		public AudioClip Firing;
		public float LaunchForce;
		public Transform ETank;
		public Transform PTank;
		public int missChance = 25;

		PlayerHealth pHealth;
		EnemyHealth eHealth;
		PlayerAttack pShoot;
		EnemyAttack eShoot;
		float timer;
		EnemyMovement eMove;
		ShellExplosion sExplo;
		bool isMiss;
		float distance;
		int randPercent;
		int decideShot = 5;
		int randomShot;

		void OnEnable ()
		{

		}

		void Awake ()
		{
			PTank = GameObject.Find("PlayerTank(Clone)").transform;
			ETank = GameObject.Find("EnemyTank_Medium(Clone)").transform;
			pHealth = PTank.GetComponent<PlayerHealth> ();
			eHealth = ETank.GetComponent<EnemyHealth> ();
			eMove = ETank.GetComponent<EnemyMovement> ();
		}

		void FixedUpdate ()
		{
			distance = Vector3.Distance (PTank.position, ETank.position);
			//Debug.Log ("D: " + distance + " | " + "P: " + PTank.position + " | " + "E: " + ETank.position);
		}

		void LateUpdate ()
		{
			timer += Time.deltaTime;

			if (timer >= timeBetweenAttacks && eMove.inRange && eHealth.CurrentHealth > 0 && pHealth.CurrentHealth > 0)
			{
				Attack ();
			}
		}

		void Attack ()
		{
			int randPercent = Random.Range (1, 100);
			int randomShot = Random.Range (0, 10);

			if (randPercent <= missChance) 
			{
				if (randomShot >= decideShot) 
				{
					LaunchForce = distance * 2;
				} 
				else 
				{
					LaunchForce = distance / 2;
				}
				Debug.Log ("ETank Misses" + LaunchForce);
			} 
			else 
			{
				if (distance >= 10) 
				{
					LaunchForce = distance;
					Debug.Log ("ETank Hits" + LaunchForce);
				}
				else 
				{
					LaunchForce = distance + distance;
				}
			}

			timer = 0f;

			Rigidbody shellInstance = Instantiate (Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;

			shellInstance.velocity = LaunchForce * FireTransform.forward;
		}
	}
}