using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && 
            (Vector3.Distance(player.position, transform.position) < 2)) 
        {
            gameObject.GetComponent<Animator>().SetTrigger("openDoor");
            Debug.Log("Door open!");
        }
    }
}
