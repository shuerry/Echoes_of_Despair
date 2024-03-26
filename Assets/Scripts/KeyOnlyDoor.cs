using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnlyDoor : MonoBehaviour
{
    Transform player;
    bool soundPlayed;
    bool hasKey;
    public AnimationClip doorSwingAnim;
    AudioSource audioSource;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlayed = false;
        hasKey = false;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) &&
            (Vector3.Distance(player.position, transform.position) < 2) && CheckForKey(player))
        {
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");
            if (!soundPlayed)
            {
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

bool CheckForKey(Transform obj)
    {
        foreach (Transform child in obj)
        {
            if (child.CompareTag("Key"))
            {
                return true;
            }
            // Recursively check children of the current child
            if (CheckForKey(child))
            {
                return true;
            }
        }
        return false;
    }
}