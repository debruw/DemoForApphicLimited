using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public Text Collected, FillText; // Text object from canvas for showing collected object count and energy bar fill percentage
    public Image FillImage; // Image object from canvas for filling the energy bar
    public RectTransform PausePanel; // RectTransform from canvas for pause menu

    public Counter PlayerCounter; // Counter Object attached to the player for counting collected objects

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.gameObject.SetActive(false);
        FillCanvas();
    }

    /// <summary>
    /// Canvas fill function for updating in game canvas
    /// </summary>
    public void FillCanvas()
    {
        Collected.text = "Collected Object : " + PlayerCounter.energyCoinCounter;
        FillImage.fillAmount = PlayerCounter.energyCoinCounter / PlayerCounter.energyBarMax;
        FillText.text = "%" + FillImage.fillAmount * 100;
    }

    /// <summary>
    /// Pause button click function
    /// Pause game and show Pause panel
    /// </summary>
    public void PauseButtonClick()
    {
        Time.timeScale = 0;
        PausePanel.gameObject.SetActive(true); // Show Pause panel
    }

    /// <summary>
    /// Pause panel close button click function
    /// Unpause game and close Pause panel
    /// </summary>
    public void PauseMenuContinueClick()
    {
        Time.timeScale = 1;
        PausePanel.gameObject.SetActive(false); // Hide Pause panel
    }

    /// <summary>
    /// Pause panel main menu button click function
    /// Change scene to the main menu
    /// </summary>
    public void PauseMenuMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene"); //Load named scene
    }

    private void OnApplicationQuit()
    {// Before leaving game, save the time data to PlayerPrefs for use it on next open
        //Save the current system time as a string in the player prefs class
        PlayerPrefs.SetString("LASTTIMESAVED", System.DateTime.Now.ToBinary().ToString());

        print("Saving this date to prefs: " + System.DateTime.Now);
    }
}
