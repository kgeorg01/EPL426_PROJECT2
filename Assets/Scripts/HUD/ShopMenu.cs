using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * The logic of the shop menu.
 * The user can spend gold to buy items/power ups.
 * 
 * 
 */
public class ShopMenu : MonoBehaviour
{
    public playerVariables playerVar;

    //Buy 1 health potion
    public void BuyHealthPotion ()
    {
        if (playerVar.GetGold() >= 10)
        {
            if (playerVar.AddPotion(1)) playerVar.SubtractGold(10);
        }
        
    }

    // Increase max health by 10
    public void BuyMaxHealth()
    {
        if (playerVar.GetGold() >= 15)
        {
            playerVar.SubtractGold(15);
            playerVar.AddMaxHealth(10);
        }
  
    }

    // Increase max shield by 10
    public void BuyMaxArmour()
    {
        if (playerVar.GetGold() >= 15)
        {
            playerVar.SubtractGold(15);
            playerVar.AddMaxArmour(10);
        }
      
    }
    // Increase damage by 20%
    public void BuyDamage()
    {
        if (playerVar.GetGold() >= 25)
        {
            playerVar.SubtractGold(25);
            playerVar.IncreasePercDamage(0.2F); //20% increase
        }
      
    }

    //Buy 1 more potion slots.
    public void BuyMaxPotion()
    {
        if (playerVar.GetGold() >= 30)
            
        {
            playerVar.SubtractGold(30);
            playerVar.AddMaxPotion(1); 
        }
        
    }


}
