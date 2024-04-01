using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{


    public Text answer;
    public Animator door; 
    //public AudioClip doorSFX;
    public AnimationClip doorSwingAnim;
    AudioSource audioSource;
    //public AudioClip buttonSFX;
    public GameObject doorObject;
    public Canvas canvas;
    bool soundPlayed;
    public AudioClip buttonSFX;


    private string codeAnswer = "8784";

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }
    public void Number(int number)
    {
        answer.text += number.ToString();

        AudioSource.PlayClipAtPoint(buttonSFX, transform.position);
    }

    public void Execute()
    {
        AudioSource.PlayClipAtPoint(buttonSFX, transform.position);
        if (answer.text == codeAnswer || answer.text == "1845")
        {
           
            answer.text = "CORRECT";
            door.SetBool("doorOpen", true); 
           
 
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");
            
                audioSource.Play();
                StartCoroutine(PlaySound());


                canvas.enabled = false;
        }
        else
        {
            answer.text = "WRONG";

            answer.text = "";

        }
    }


    IEnumerator PlaySound()
    {
        yield return new WaitForSecondsRealtime(doorSwingAnim.length);
        audioSource.Stop();
        Debug.Log("audio stop");
    }

    public void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
    }
}
