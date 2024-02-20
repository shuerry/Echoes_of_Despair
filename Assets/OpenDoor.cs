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
            (Vector3.Distance(player.position, transform.position) < 2)) {
            Debug.Log("Door open!");
            gameObject.GetComponent<Animator>().SetTrigger("doorSwing");
        }
    }
}
