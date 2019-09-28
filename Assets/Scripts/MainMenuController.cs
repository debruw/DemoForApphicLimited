using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public RectTransform OptionsPanel, RewardedVideoPanel; // Object for option menu and rewarded video menu
    public Button MusicBtn; // Object for controlling music state

    public Text LifeText; // Object for live text

    public LifeController my_lifeController; // LifeController in scene
    DateTime currentDate; // Variable for current date time
    DateTime oldDate; // Variable for old date time

    private void Awake()
    {
        //Store the current time when it starts
        currentDate = System.DateTime.Now;

        //Grab the old time from the player prefs as a long
        long temp = Convert.ToInt64(PlayerPrefs.GetString("LASTTIMESAVED"));

        //Convert the old time from binary to a DataTime variable
        DateTime oldDate = DateTime.FromBinary(temp);
        print("oldDate: " + oldDate);

        //Use the Subtract method and store the result as a timespan variable
        TimeSpan difference = currentDate.Subtract(oldDate);
        print("Difference: " + difference);

        // Control if difference greater than 24 hour
        // if true give player max live
        if (difference > TimeSpan.FromHours(24))
        {
            my_lifeController.SetLivesFull();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OptionsPanel.gameObject.SetActive(false);
        RewardedVideoPanel.gameObject.SetActive(false);

        // Control is game is openin for first time in this device
        if (PlayerPrefs.GetInt("ISFIRSTTIMEOPENING", 1) == 1)
        {// its first time opening on this device
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("ISFIRSTTIMEOPENING", 0);

            // Make first  time setups for PlayerPrefs
            FirstSetup();
            // Set initialize values for game
            InitializeScene();
        }
        else
        {// its not first time opening in this device
            Debug.Log("NOT First Time Opening");

            InitializeScene();
        }

        LifeText.text = "Lives : " + PlayerPrefs.GetInt("LIVESLEFT");
    }

    /// <summary>
    ///  Update the game screen after winning the reward
    /// </summary>
    public void updateScreenForReward()
    {
        LifeText.text = "Lives : " + PlayerPrefs.GetInt("LIVESLEFT");
        RewardedVideoPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Make first  time setups for PlayerPrefs
    /// </summary>
    void FirstSetup()
    {
        Debug.Log("firstSetup");
        
        PlayerPrefs.SetInt("ISMUSICPLAYING", 1);
        PlayerPrefs.SetInt("LIVESLEFT", 3);
        PlayerPrefs.SetString("LASTTIMESAVED", System.DateTime.Now.ToString());
    }

    /// <summary>
    /// Set initialize values for game
    /// </summary>
    void InitializeScene()
    {
        if (PlayerPrefs.GetInt("ISMUSICPLAYING") == 1)
        {
            MusicBtn.GetComponent<Image>().color = Color.green;
            MusicBtn.GetComponentInChildren<Text>().text = "ON";
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().color = Color.red;
            MusicBtn.GetComponentInChildren<Text>().text = "OFF";
            GetComponent<AudioSource>().mute = true;
        }
    }

    /// <summary>
    /// Play button click function
    /// if "livesLeft" greater than zero , Decrease the "livesLeft" and load the "DemoScene"
    /// if "livesLeft" not greater than zero , show rewarded video panel
    /// </summary>
    public void PlayBtnClick()
    {
        if (my_lifeController.getLives() > 0)
        {
            my_lifeController.lostLife();
            SceneManager.LoadScene("DemoScene", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Yetersiz can.");
            RewardedVideoPanel.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Options button click function
    /// Open options menu
    /// </summary>
    public void OptionsBtnClick()
    {
        OptionsPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Options menu close button click function
    /// Cloase options menu
    /// </summary>
    public void OptionsPanelCloseBtnClick()
    {
        OptionsPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Rewarded menu no button click function
    /// Close the rewarded ad menu
    /// </summary>
    public void DontWantRewardedVideoClick()
    {
        RewardedVideoPanel.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Options menu music button click function
    /// Change the music state depends on PlayerPrefs
    /// </summary>
    public void MusicButtonClick()
    {
        if (PlayerPrefs.GetInt("ISMUSICPLAYING") == 0)
        {
            Debug.Log("Make music play");
            PlayerPrefs.SetInt("ISMUSICPLAYING", 1);
            InitializeScene();
        }
        else
        {
            Debug.Log("Make music mute");
            PlayerPrefs.SetInt("ISMUSICPLAYING", 0);
            InitializeScene();
        }
    }

    private void OnApplicationQuit()
    {// Before leaving game, save the time data to PlayerPrefs for use it on next open
        //Savee the current system time as a string in the player prefs class
        PlayerPrefs.SetString("LASTTIMESAVED", System.DateTime.Now.ToBinary().ToString());

        print("Saving this date to prefs: " + System.DateTime.Now);
    }
}
