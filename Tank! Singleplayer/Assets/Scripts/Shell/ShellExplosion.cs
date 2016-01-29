using UnityEngine;
using PlayerInfo;
using EnemyInfo;

namespace Shell
{
	public class ShellExplosion : MonoBehaviour
	{
		public LayerMask TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
		public ParticleSystem ExplosionParticles;         // Reference to the particles that will play on explosion.
		public AudioSource ExplosionAudio;                // Reference to the audio that will play on explosion.
		public float MaxDamage = 100f;                     // The amount of damage done if the explosion is centred on a tank.
		public float ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
		public float MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
		public float ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.


		private void Start ()
		{
			// If it isn't destroyed by then, destroy the shell after it's lifetime.
			Destroy (gameObject, MaxLifeTime);
		}


		public void OnTriggerEnter (Collider other)
		{
			// Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
			Collider[] colliders = Physics.OverlapSphere (transform.position, ExplosionRadius, TankMask);

			// Go through all the colliders...
			for (int i = 0; i < colliders.Length; i++)
			{
				// ... and find their rigidbody.
				Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

				// If they don't have a rigidbody, go on to the next collider.
				if (!targetRigidbody)
					continue;

				// Add an explosion force.
				targetRigidbody.AddExplosionForce (ExplosionForce, transform.position, ExplosionRadius);

				// Find the TankHealth script associated with the rigidbody.
				PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth> ();
				EnemyHealth targetEHealth = targetRigidbody.GetComponent<EnemyHealth> ();

				// Calculate the amount of damage the target should take based on it's distance from the shell.
				if (targetHealth) 
				{
					float damage = CalculateDamage (targetRigidbody.position);
					targetHealth.TakeDamage (damage);
				}
				if (targetEHealth) 
				{
					float damage = CalculateDamage (targetRigidbody.position);
					targetEHealth.TakeDamage (damage);
				}
				if (!targetHealth)
					continue;
				if (!targetEHealth)
					continue;
			}

			// Unparent the particles from the shell.
			ExplosionParticles.transform.parent = null;

			// Play the particle system.
			ExplosionParticles.Play();

			// Play the explosion sound effect.
			ExplosionAudio.Play();

			// Once the particles have finished, destroy the gameobject they are on.
			Destroy (ExplosionParticles.gameObject, ExplosionParticles.duration);

			// Destroy the shell.
			Destroy (gameObject);
		}


		public float CalculateDamage (Vector3 targetPosition)
		{
			// Create a vector from the shell to the target.
			Vector3 explosionToTarget = targetPosition - transform.position;

			// Calculate the distance from the shell to the target.
			float explosionDistance = explosionToTarget.magnitude;

			// Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
			float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;

			// Calculate damage as this proportion of the maximum possible damage.
			float damage = relativeDistance * MaxDamage;

			// Make sure that the minimum damage is always 0.
			damage = Mathf.Max (0f, damage);

			return damage;
		}
	}
}