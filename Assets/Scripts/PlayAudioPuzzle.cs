using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioPuzzle : MonoBehaviour
{


    AudioSource puzzleAudio;


    // Start is called before the first frame update
    void Start()
    {
        puzzleAudio = GetComponent<AudioSource>();
        // puzzleAudio.gameObject.SetActive(false);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleAudio.Play();
            // puzzleAudio.gameObject.SetActive(true);

            //  Invoke("Disable", 5);



        }

    }



    private void Disable()
    {
        puzzleAudio.gameObject.SetActive(false);
    }

}
