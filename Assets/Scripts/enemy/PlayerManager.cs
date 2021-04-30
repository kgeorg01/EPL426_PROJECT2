using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // This script is used to know the the current position of the player by the enemy agents and move towards him

    #region Singelton

    public static PlayerManager instance;
    private void Awake() {
        instance = this;
    }
    #endregion

    public GameObject player;
}
