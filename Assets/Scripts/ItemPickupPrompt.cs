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
<<<<<<< HEAD
            Invoke("Disable", 1.0f);
=======
            Invoke("Disable", 2);
>>>>>>> d29e712cf408343859c846fa6bc5d2c9c8644ce1


           
        }
        
    }



    private void Disable()
    {
        pickUpText.gameObject.SetActive(false); 
    }

}
