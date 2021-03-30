using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
	public int maxShield = 100;
	public int currentShield;
	public int currentGold = 0;

	public HUDHealthBar healthBar;
	public HUDShieldBar shieldbar;
	public HUDGold goldInd;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		currentShield = maxShield;
		shieldbar.SetMaxShield(maxShield);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(20);
			AddGold(10);
		}

	
	}

	void AddGold (int gold)
    {
		currentGold += gold;
		goldInd.SetGold(currentGold);
    }

	void TakeDamage(int damage)
	{
		if (currentShield>0)
        {
			currentShield -= damage;
			shieldbar.SetShield(currentShield);
		} else
        {
			currentHealth -= damage;
			healthBar.SetHealth(currentHealth);
		}
		

	}
}
