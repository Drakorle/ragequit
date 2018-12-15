using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour {
    #region Singleton

    public static PlayerManager2 instance;

    void Awake()
    {
        instance = this; 
    }
    #endregion

    public GameObject player;

}
