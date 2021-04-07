using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region Singelton

    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
}
