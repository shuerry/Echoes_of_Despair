using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private bool isGrabbed = false;
    private Transform originalParent;
    private Transform playerTransform;
    private Vector3 originalPositionOffset;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHand").transform;
       // originalPositionOffset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!isGrabbed)
            {
                Grab();
                LevelManager.haveWeapon = true;
            }
            else
            {
                Drop();
                LevelManager.haveWeapon = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X) && isGrabbed)
        {
            Drop();
            LevelManager.haveWeapon = false;
        }
    }

    private void Grab()
    {
        originalParent = transform.parent;
        transform.SetParent(playerTransform);
        transform.localPosition = originalPositionOffset;
        isGrabbed = true;
    }

    private void Drop()
    {
        transform.SetParent(originalParent);
        isGrabbed = false;
    }
}
