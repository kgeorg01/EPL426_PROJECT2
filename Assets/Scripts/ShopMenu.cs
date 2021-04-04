using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public playerVariables playerVar;

    public void BuyHealthPotion ()
    {
        if (playerVar.GetGold() >= 10)
        {
            if (playerVar.AddPotion(1)) playerVar.SubtractGold(10);
        }
        
    }
    public void BuyMaxHealth()
    {
        if (playerVar.GetGold() >= 15)
        {
            playerVar.SubtractGold(15);
            playerVar.AddMaxHealth(10);
        }
      //  Debug.Log(playerVar.maxHealth);
    }

    public void BuyMaxArmour()
    {
        if (playerVar.GetGold() >= 15)
        {
            playerVar.SubtractGold(15);
            playerVar.AddMaxArmour(10);
        }
       // Debug.Log(playerVar.maxArmour);
    }

    public void BuyDamage()
    {
        if (playerVar.GetGold() >= 25)
        {
            playerVar.SubtractGold(25);
            playerVar.IncreasePercDamage(0.2F); //20% increase
        }
       // Debug.Log(playerVar.attackDamage);
    }

    public void BuyMaxPotion()
    {
        if (playerVar.GetGold() >= 30)
            
        {
            playerVar.SubtractGold(30);
            playerVar.AddMaxPotion(1); //20% increase
        }
        //Debug.Log(playerVar.maxPotions);
    }


}
