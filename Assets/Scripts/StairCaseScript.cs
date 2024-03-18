using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCaseScript : MonoBehaviour
{
    Transform player;
    public AudioClip doorSFX;
    bool soundPlayed;
    bool spawnedYet;
    public GameObject enemySpawner;
    public GameObject enemyPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlayed = false;
        spawnedYet = false;
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

         if (spawnedYet == false && Vector3.Distance(player.transform.position, enemySpawner.transform.position) < 2.1) {
            GameObject enemy = Instantiate(enemyPrefab, enemySpawner.transform.position, enemySpawner.transform.rotation) as GameObject;
            Debug.Log("EnemySpawned");
            spawnedYet = true;
         }
    }
}
