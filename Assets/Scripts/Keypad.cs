using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{


    public Text answer;
    public Animator door; 
    public AudioClip doorSFX;
    public AudioClip buttonSFX;
    public GameObject doorObject;
    public Canvas canvas;
    bool soundPlayed;


    private string codeAnswer = "8784";

    public void Number(int number)
    {
        answer.text += number.ToString();

        AudioSource.PlayClipAtPoint(buttonSFX, transform.position);
    }

    public void Execute()
    {
        AudioSource.PlayClipAtPoint(buttonSFX, transform.position);
        if (answer.text == codeAnswer)
        {
           
            answer.text = "CORRECT";
            door.SetBool("doorOpen", true); 
           
            AudioSource.PlayClipAtPoint(doorSFX, transform.position);
            
           

            canvas.enabled = false;
        }
        else
        {
            answer.text = "WRONG";

            answer.text = "";

        }
    }



    public void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
    }
}
