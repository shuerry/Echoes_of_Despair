using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public GameObject enemyExpelled;
    public GameObject keyPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            DestroyNPC();
        
        }
    }

    void DestroyNPC()
    {
        Instantiate(enemyExpelled, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Instantiate(keyPrefab, transform.position, transform.rotation);


    }
}

