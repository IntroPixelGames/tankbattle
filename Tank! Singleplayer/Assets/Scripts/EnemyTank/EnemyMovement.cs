using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerInfo;
using Shell;

namespace EnemyInfo
{
	public class EnemyMovement : MonoBehaviour 
	{
		public Transform player;
		public Transform EnemyTank;
		public int AttackRange = 20;
		public bool inRange;
		public int TooClose = 10;

		NavMeshAgent agent;
		static PlayerHealth pHealth;
		static EnemyHealth eHealth;
		Rigidbody Rigidbody;
		PlayerAttack pAttack;
		float distance;
		Vector3 pTank;
		Vector3 eTank;

		void Start ()
		{
			player = GameObject.FindWithTag ("Player").transform;
			pHealth = player.GetComponent<PlayerHealth> ();
			pAttack = player.GetComponent<PlayerAttack> ();
			eHealth = GetComponent<EnemyHealth> ();
			Rigidbody = GetComponent<Rigidbody> ();
		}

		public void OnEnable ()
		{
			Rigidbody.isKinematic = false;
		}

		public void OnDisable ()
		{
			Rigidbody.isKinematic = true;
		}

		public void LateUpdate ()
		{
			NavMeshAgent agent = GetComponent<NavMeshAgent> ();

			if (agent.enabled)
			{
				agent.SetDestination (player.position);
				transform.LookAt (player.position);
			} 
			else 
			{
				agent.enabled = false;
			}

			if (Vector3.Distance (player.position, EnemyTank.position) < AttackRange) {
				if (Vector3.Distance (player.position, EnemyTank.position) > TooClose)
				{
					inRange = true;
				}
			} 
			else if (Vector3.Distance (player.position, EnemyTank.position) > AttackRange)
			{
				inRange = false;
			}

			distance = Vector3.Distance (player.position, EnemyTank.position);
		}
	}
}