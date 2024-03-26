using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Transform player;
    public AudioClip doorSFX;
    public AnimationClip doorSwingAnim;
    AudioSource audioSource;
    bool soundPlayed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlayed = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && 
            (Vector3.Distance(player.position, transform.position) < 2)) 
        {
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");
            if (!soundPlayed) {
                audioSource.Play();
                StartCoroutine(PlaySound());
                soundPlayed = true;
            }
            
            
            Debug.Log("Door open!");
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSecondsRealtime(doorSwingAnim.length);
        audioSource.Stop();
        Debug.Log("audio stop");
    }
}
