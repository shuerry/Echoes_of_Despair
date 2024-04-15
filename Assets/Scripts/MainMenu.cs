using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text playTime;
    public static float totalPlayTime;
    private string timeDisplay;

    void Start()
    {
        // Initialize total play time from saved PlayerPrefs, assuming it's saved in seconds.
        totalPlayTime = PlayerPrefs.GetFloat("PlayTimeInSeconds", 0f);
        UpdatePlayTimeDisplay();
    }

    void Update()
    {
        totalPlayTime += Time.deltaTime;
        UpdatePlayTimeDisplay();
        PlayerPrefs.SetFloat("PlayTimeInSeconds", totalPlayTime);
        PlayerPrefs.Save();
    }

    void UpdatePlayTimeDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalPlayTime);
        timeDisplay = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        playTime.text = "Play Time: " + timeDisplay;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Progress",SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

