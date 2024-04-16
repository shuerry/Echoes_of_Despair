using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : MonoBehaviour
{

    public float levelDuration = 3.0f;
  //  public Text timerText;
 //   public Text scoreText;
 //   public Text gameText;
 //   public AudioClip gameOverSFX;
 //   public AudioClip gameWonSFX;
    public string nextLevel;

    private int itemCount = 0;
    private int score = 1;
    private float countdown;
    public static bool isGameOver = false;

    void Start()
    {
        Invoke("LoadNextLevel", 3);
       // ResetGameState();
       
        //UpdateUI();
    }

    void Update()
    {

       
        }
    

    //void UpdateUI()
    //{
    //    if (timerText != null)
    //        timerText.text = countdown.ToString("f2");

    //    if (scoreText != null)
    //        scoreText.text = "Score: " + score.ToString();
    //}

    public void IncreaseScore(int value)
    {
        score += value;
      //  UpdateUI();
    }

    public void DecreasePickupCount()
    {
        itemCount--;
        if (itemCount <= 0)
        {
        //    LevelBeat();
        }
    }

    //public void LevelLost()
    //{
    //    isGameOver = true;
    //    if (gameText != null)
    //    {
    //        gameText.text = "GAME OVER!";
    //        gameText.gameObject.SetActive(true);
    //    }
    //    if (gameOverSFX != null)
    //        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

    //    // Reload the current scene after 2 seconds
    //    Invoke("ReloadLevel", 2);
    //}

    //public void LevelBeat()
    //{
    //    isGameOver = true;
    //    if (gameText != null)
    //    {
    //        gameText.text = "YOU WIN!";
    //        gameText.gameObject.SetActive(true);
    //    }
    //    if (gameWonSFX != null)
    //        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
    //    if (!string.IsNullOrEmpty(nextLevel))
    //    {
    //        // Load the next level after 2 seconds
    //        Invoke("LoadNextLevel", 2);
    //    }
    //}

  
    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void OnLevelWasLoaded(int level)
    {
        ResetGameState();
     //   UpdateUI();
    }

    void ResetGameState()
    {
        isGameOver = false;
        itemCount = GameObject.FindGameObjectsWithTag("Item").Length;
        score = 0;
        countdown = levelDuration;
    }

    public void DecreaseItemCount()
    {
        itemCount--;

        Debug.Log("Remaining items: " + itemCount);

        // If no items left, level is completed
        if (itemCount <= 0)
        {
           // LevelBeat();
            Debug.Log("Level completed!");
        }
    }
}
