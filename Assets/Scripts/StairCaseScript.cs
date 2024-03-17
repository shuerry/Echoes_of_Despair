using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCaseScript : MonoBehaviour
{
    Transform player;
    public AudioClip doorSFX;
    bool soundPlayed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && 
            (Vector3.Distance(player.position, transform.position) < 2) &&
            LevelManager.haveWeapon) 
        {
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");

            if (!soundPlayed) {
                AudioSource.PlayClipAtPoint(doorSFX, transform.position);
                soundPlayed = true;
            }
            
            Debug.Log("Door open!");
        }
    }
}
