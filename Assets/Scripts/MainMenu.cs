using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text playTime;
    private float totalPlayTime;
    private string timeDisplay;

    void Start()
    {
        // Initialize total play time from saved PlayerPrefs, assuming it's saved in seconds.
        totalPlayTime = PlayerPrefs.GetFloat("PlayTimeInSeconds", 0f);
        UpdatePlayTimeDisplay();
    }

    void Update()
    {
        // Add the time since the last frame to the total play time
        totalPlayTime += Time.deltaTime;
        UpdatePlayTimeDisplay();
        PlayerPrefs.SetFloat("PlayTimeInSeconds", totalPlayTime);
        PlayerPrefs.Save();
    }

    private void UpdatePlayTimeDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalPlayTime);
        timeDisplay = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        playTime.text = "Play Time: " + timeDisplay;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

