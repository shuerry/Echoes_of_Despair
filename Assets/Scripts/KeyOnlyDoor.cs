using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnlyDoor : MonoBehaviour
{
    Transform player;
    public AudioClip doorSFX;
    bool soundPlayed;
    bool hasKey;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlayed = false;
        hasKey = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) &&
            (Vector3.Distance(player.position, transform.position) < 2) && CheckForKey(player))
        {
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");

            if (!soundPlayed)
            {
                AudioSource.PlayClipAtPoint(doorSFX, transform.position);
                soundPlayed = true;
            }

            Debug.Log("Door open!");
        }
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