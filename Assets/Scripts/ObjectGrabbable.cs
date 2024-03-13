using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{

    private Rigidbody rb;
    private Transform objectGrabPoint;  

    private void Awake() {

        rb = GetComponent<Rigidbody>();   
    }
    public void Grab(Transform objectGrabPoint)
    {
        this.objectGrabPoint = objectGrabPoint;
        rb.useGravity = false;  

    }

    private void FixedUpdate()
    {
          if(objectGrabPoint != null)
        {
            float lerpSpeed = 10f; 
            Vector3.Lerp(transform.position, objectGrabPoint.position, Time.deltaTime * lerpSpeed    );  
            rb.MovePosition(objectGrabPoint.position);   
        }
    }

    public void Drop()
    {

        this.objectGrabPoint = null;
        rb.useGravity = true;
    }



}
