using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ItemPickupPrompt : MonoBehaviour
{
    [SerializeField]
    private Text pickUpText;
    

    // Start is called before the first frame update
    void Start()
    {
        pickUpText.gameObject.SetActive(false);
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickUpText.gameObject.SetActive(true);

            Invoke("Disable", 2);
        }
        
    }



    private void Disable()
    {
        pickUpText.gameObject.SetActive(false); 
    }

}
