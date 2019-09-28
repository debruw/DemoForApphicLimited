using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    // CanvasController object in game
    public CanvasController my_canvasController; 

    public float energyCoinCounter = 0; // Counter variable for energyCoin
    public float energyBarMax; // Max coin number for energybar 

    private void OnCollisionEnter(Collision collision)
    {// if colllision object is EnergyCoin increase "energyCoinCounter" variable and update in game canvas
        if (collision.gameObject.tag == "EnergyCoin")
        {
            energyCoinCounter++;
            my_canvasController.FillCanvas();
        }
    }
}
