using UnityEngine;
using System.Collections;
using PlayerInfo;
using EnemyInfo;
using Management;
using UnityEngine.UI;
using Shell;

public class PowerUps : MonoBehaviour
{
	public Text PowerUpMessageText;

	public float SpeedBonus, SpeedDuration, SpeedCooldown;
	public float HealthPercentIncrease, HealthCooldown;
	public float DamageMultiplier, DamageDuration, DamageCooldown;

	PlayerMovement pMove;
	EnemyMovement eMove;
	PlayerHealth pHealth;
	EnemyHealth eHealth;
	PlayerAttack pAttack;
	EnemyAttack eAttack;
	ShellExplosion peDamage;
	float NewCurrentHealth;
	static PowerUps instance;

	[HideInInspector] public bool isActivable_speed = true;
	[HideInInspector] public bool isActivable_health = true;
	[HideInInspector] public bool isActivable_damage = true;

	void Awake ()
	{
		instance = this;
	}

	public IEnumerator PowerUpMessage (string message, float Delay)
	{
		PowerUpMessageText.text = message;
		PowerUpMessageText.enabled = true;
		yield return new WaitForSeconds (Delay);
		PowerUpMessageText.enabled = false;
	}

	public IEnumerator PlayerSpeedPowerUp()
	{
		StartCoroutine(PowerUpMessage("SPEED POWERUP ACTIVATED! " + "+" + SpeedBonus + "SPEED FOR " + SpeedDuration + " SECONDS", 4));

		isActivable_speed = false;

		pMove.Speed += SpeedBonus;

		yield return new WaitForSeconds (SpeedDuration);

		pMove.Speed -= SpeedBonus;

		StartCoroutine (PowerUpMessage("SPEED POWERUP FINISHED! " + SpeedCooldown + " SECOND COOLDOWN", 4));

		yield return new WaitForSeconds (SpeedCooldown);

		isActivable_speed = true;
	}

	public IEnumerator PlayerHealthPowerUp()
	{
		NewCurrentHealth = pHealth.CurrentHealth * HealthPercentIncrease;

		StartCoroutine (PowerUpMessage("HEALTH POWERUP USED! " + HealthPercentIncrease +  "% MORE HEALTH", 4));

		isActivable_health = false;

		pHealth.CurrentHealth += NewCurrentHealth;

		yield return new WaitForSeconds (HealthCooldown);

		isActivable_health = true;
	}

	public IEnumerator PlayerDamagePowerUp()
	{
		float NewDamage = peDamage.MaxDamage * DamageMultiplier;

		StartCoroutine (PowerUpMessage ("DAMAGE POWERUP ACTIVATED! " + DamageMultiplier + "x HIGHER DAMAGE", 4));

		isActivable_damage = false;

		peDamage.MaxDamage += NewDamage;

		yield return new WaitForSeconds (DamageDuration);

		peDamage.MaxDamage -= NewDamage;

		StartCoroutine (PowerUpMessage ("DAMAGE POWERUP FINISH! " + DamageCooldown + " SECOND COOLDOWN", 4));

		yield return new WaitForSeconds (DamageCooldown);

		isActivable_damage = true;
	}
}