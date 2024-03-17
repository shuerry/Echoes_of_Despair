using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float destroyedDuration = 3;
    void Start()
    {
        Destroy(gameObject, destroyedDuration);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
