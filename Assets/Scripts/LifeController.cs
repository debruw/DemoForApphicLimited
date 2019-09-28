using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    private const int MAX_LIVES = 3; // Maximum live number

    private int livesLeft; // Current live number

    private void Start()
    {
        livesLeft = PlayerPrefs.GetInt("LIVESLEFT"); // Get "livesLeft" variable value from PlayerPrefs
    }

    /// <summary>
    /// Set "livesLeft" to MAX_LIVES and save it to the PlayerPrefs
    /// </summary>
    public void SetLivesFull()
    {
        livesLeft = MAX_LIVES;
        PlayerPrefs.SetInt("LIVESLEFT", livesLeft);
    }

    /// <summary>
    /// Function for decrease "livesLeft" variable
    /// </summary>
    public void lostLife()
    {
        livesLeft--;
        PlayerPrefs.SetInt("LIVESLEFT", livesLeft);
    }

    /// <summary>
    /// return function for "livesLeft" variable
    /// </summary>
    /// <returns> Lives Left</returns>
    public int getLives()
    {
        return livesLeft;
    }
}
