using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    /* 
    Create a variable called 'rb' that will represent the 
    rigid body of this object.
    */
    private Rigidbody rb;
    public float moveSpeed;
    Vector3 input;
    CharacterController controller;
    Light flashlight;
    bool flashlightOn;

    void Start()
    {
    	// make our rb variable equal the rigid body component
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        flashlight = GetComponentInChildren<Light>();
        flashlightOn = false;
        // yPos = transform.position.y;
    }
 
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= moveSpeed;
        
        controller.Move(input * Time.deltaTime);

        if (Input.GetKey(KeyCode.E)) {
            Debug.Log("Interact!");
        }

        if (Input.GetKey(KeyCode.C)) {
            Debug.Log("Hide");
        }

        if (Input.GetKey(KeyCode.F)) {
            Debug.Log("Flashlight");
            if (flashlightOn) {
                flashlight.intensity = 0;
            } else {
                flashlight.intensity = 1;
            }
            
        }
    }
}
