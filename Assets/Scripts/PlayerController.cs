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
    public float moveSpeed = 5;
    public float gravity = 9.81f;
    public float airControl = 10;

    Vector3 input, moveDirection;
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
        flashlight.intensity = 0;
        // yPos = transform.position.y;
    }
 
    void Update()
    {
        if (!LevelManager.isGameOver) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
            // input *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                moveSpeed *= 2;
                Debug.Log("Increase Speed: " + input);
            } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                moveSpeed /= 2;
                Debug.Log("Decrease Speed: " + input);
            }

            input *= moveSpeed;
            
            
            if (controller.isGrounded) {
               moveDirection = input;
                /*
                if (Input.GetButton("Jump")) {
                    moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                } else {
                    moveDirection.y = 0.0f;
                }
                */
            } else {
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(input * Time.deltaTime);

            if (Input.GetKey(KeyCode.E)) {
                Debug.Log("Interact!");
            }

            if (Input.GetKey(KeyCode.C)) {
                Debug.Log("Hide");
            }

            if (Input.GetKey(KeyCode.F)) {
                if (flashlightOn) {
                    Debug.Log("Flashlight Turn Off");
                    flashlightOn = false;
                    flashlight.intensity = 0;
                } else {
                    Debug.Log("Flashlight Turn On");
                    flashlightOn = true;
                    flashlight.intensity = 1;
                }
            }
        }
    }
}
