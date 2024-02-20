using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{

    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private Transform objectGrab;

      private ObjectGrabbable objectGrabbable;
      
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //not carrying an object try to grab 
            if (objectGrabbable == null)
            {
                float pickupDistance = 2f;
                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrab);

                    }
                }
            }

            else
            {
                //currently carrying something drop
                objectGrabbable.Drop();
                objectGrabbable = null;

            }
        }
        
    }
}
 