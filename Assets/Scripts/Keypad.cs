using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{


    public Text answer;
    public Animator door; 
    public AudioClip doorSFX;
    bool soundPlayed;


    private string codeAnswer = "8784";

    public void Number(int number)
    {
        answer.text += number.ToString(); 
        
    }

    public void Execute()
    {
        if(answer.text == codeAnswer)
        {
           
            answer.text = "CORRECT";
            door.SetBool("doorOpen", true); 
            if (!soundPlayed)
            {
                AudioSource.PlayClipAtPoint(doorSFX, transform.position);
                soundPlayed = true;
            }
        }
        else
        {
            answer.text = "WRONG";

            answer.text = "";

        }
    }
}
