using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerVariables : MonoBehaviour
{
    // Variables about the state of our player
    public static bool grounded;
    public static bool blocking;
    public static bool attacking;
    public static bool attackingheavy;
    public static bool attackinglight;
    public static bool dead;
    public static bool falling;

    // Variables about movement
    public static float mH;
    public static float mV;
    public static float speed;
    public static float jump;

    // Variables for stats
    public int maxHealth;
    public  int maxPotions;
    public  int maxArmour;
    public  int health;
    public  int armour;
    public  int gold;
    public  int potions;
    public  float attackDamage;

    // Variables for attack
    public static int clickslight;
    public static int clicksheavy;
    public AudioSource pain;
    public AudioSource drink;

    public  HUDHealthBar healthBar;
    public  HUDShieldBar shieldbar;
    public  HUDGold goldInd;
    public  HUDPotion potionInd;

    //sound
    public bool sound = true;

    //SaveSystem
    private static string SaveSystemInstruction = "0";
    

    private void Start()
    {
        grounded = false;
        blocking = false;
        attacking = false;
        dead = false;
        mH = 0;
        mV = 0;
        speed = 5f;
        jump = 5f;
        clickslight = 0;
        clicksheavy = 0;

        //Starting values
        maxHealth = 100;
        maxArmour = 50;
        maxPotions = 3;
        gold = 0;
        potions = 0;
        attackDamage = 20;

        health = maxHealth;
        armour = maxArmour;
        

        healthBar.SetMaxHealth(maxHealth);
        shieldbar.SetMaxShield(maxArmour);
        goldInd.SetGold(gold);
        potionInd.SetPotion(potions);


        //SaveSystem operations
        if (SaveSystemInstruction == "load1") LoadData(1);
        else if (SaveSystemInstruction == "load2") LoadData(2);
        else if (SaveSystemInstruction == "load3") LoadData(3);
        else if(SaveSystemInstruction == "load0") LoadData(0, false); //doesnt load a scence and used the secret save 0. (save on 0 when finishing a level and load 0 when starting a new level)

        SaveSystemInstruction = "0";

       
       LoadOptionPreferences();

    }

    public void LateUpdate()
    {
        if (health == 0) dead = true;
    }

    private void LoadOptionPreferences()
    {

        Debug.Log("Player");

        //GameObject [] optionMenuUI = GameObject.FindGameObjectsWithTag("OptionMenu");
        OptionsMenu[] optionMenus = GameObject.FindObjectsOfType<OptionsMenu>(true);
        OptionsMenu optionMenu = optionMenus[0];

        try
        {
            optionMenu.setVolume(PlayerPrefs.GetFloat("volume"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences volume");
        }

        try
        {
            optionMenu.setMusic(PlayerPrefs.GetFloat("music"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences music");
        }

        try
        {
            optionMenu.setQuality(PlayerPrefs.GetInt("quality"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences quality");
        }

        try
        {
            if (PlayerPrefs.GetInt("fullscreen") == 1) optionMenu.setFulllscreen(true);
            else optionMenu.setFulllscreen(false);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences fullscreen");
        }

       try
        {
            optionMenu.setResolution(PlayerPrefs.GetInt("resolution"));
      }
       catch (Exception e)
       {
          Debug.LogWarning("Cant load preferences resolution");
      }

    }



    public void SetDamage (int dmg)
    {
        attackDamage = dmg;
    }

    // for 20% increase the input is 0.2
    public void IncreasePercDamage (float percentage )
    {
        float increasePercDamage = attackDamage* percentage;
        attackDamage += increasePercDamage;
    }

    public  void TakeDamage(int damage)
    {
        if (!pain.isPlaying && !dead) pain.Play();
        if (blocking)
        {
            if (armour > damage)
            {
                armour -= damage;
                shieldbar.SetShield(armour);

            }
            else if (armour > 0)
            {
                //damage>armor
                damage -= armour;
                armour = 0; //damage >=armor so armor becomes 0
                shieldbar.SetShield(armour);

                //rest of the damage affects health
                health -= damage;
                if (health < 0) health = 0;
                healthBar.SetHealth(health);

            }
            else
            {
                //no shield left
                health -= damage;
                if (health < 0) health = 0;
                healthBar.SetHealth(health);
            }

        }
        else
        {
            health -= damage;
            if (health < 0) health = 0;
            healthBar.SetHealth(health);
        }

        if (health == 0)
        {
            dead = true;
        }
    }

    public void AddGold(int gold2)
    {

        gold += gold2;
        goldInd.SetGold(gold);
    }

    public void SubtractGold(int gold2)
    {
        gold -= gold2;
        
        goldInd.SetGold(gold);
    }

    public  void AddHealth (int hp)
    {
        health += hp;
        if (health > maxHealth) health = maxHealth;
        healthBar.SetHealth(health);
    }

    public void AddArmour(int shield)
    {
        armour += shield;
        if (armour> maxArmour) armour= maxArmour;
        shieldbar.SetShield(armour);
    }

    public  bool AddPotion(int pot)
    {
        if (potions >= maxPotions) return false;
        else
        {
            potions += pot;
            potionInd.SetPotion(potions);
            return true;
        }
       
    }

    public  void RemovePotion(int pot)
    {
        potions -= pot;
        if (potions >= 0) potionInd.SetPotion(potions);
    }

    public void DrinkPotion()
    {
        if (health < maxHealth && potions>0)
        {
            drink.Play();
            RemovePotion(1);
            AddHealth(25);
        }

    }

    public void AddMaxHealth (int health)
    {
        maxHealth += health;
        AddHealth(health);
    }

    public void AddMaxArmour(int armor)
    {
        maxArmour += armor;
        AddArmour(armor);
    }

    public void AddMaxPotion(int pot)
    {
        maxPotions += pot;
       
    }

    public int  GetGold ()
    {
        return gold;
    }

    public void SavePlayer(int slot)
    {
        Debug.Log("Saving ...");
        //Debug.Log(transform.position);
        SaveSystem.SavePlayer(this , slot);


    }

    public void LoadPlayer (int slot)
    {

        
       
        SaveSystemInstruction = "load" + slot;

        Time.timeScale = 1f;
        if (slot == 0)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene. Must be in the correct order in build scene.
        }
        else
        {
            PlayerData pd = SaveSystem.LoadPlayer(slot);
            SceneManager.LoadScene(pd.scenceIdx);

        }

    }

    public void LoadData (int slot, bool pos = true )
    {
        //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID" (potions , shield, gold etc)
        PlayerData pd = SaveSystem.LoadPlayer(slot );
        Debug.Log("Loading ...");
        maxHealth = pd.maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        health = pd.health;
        healthBar.SetHealth(health);

        maxArmour = pd.maxArmour;
        shieldbar.SetMaxShield(maxArmour);

        armour = pd.armour;
        shieldbar.SetShield(armour);

        gold = pd.gold;
        goldInd.SetGold(gold);

        maxPotions = pd.maxPotions;

        potions = pd.potions;
        potionInd.SetPotion(potions);

        attackDamage = pd.attackDamage;

        if (pos)
        {
            Vector3 position;
            position.x = pd.position[0];
            position.y = pd.position[1];
            position.z = pd.position[2];
            transform.position = position;
        }

    }

}
