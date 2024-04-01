using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JournalReading4 : MonoBehaviour
{


    public Text infoText;

    public GameObject bookText;

    public float maxDistance; // Maximum distance at which object info will be displayed

    private bool reading = false;

    public static JournalReading4 Instance;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {

        }
    }



    // Update is called once per frame
    void Update()
    {

        if (reading)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowBookText(false);
            }

            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Book"))
            {
                float distance = Vector3.Distance(transform.position, hitObject.transform.position);
                if (distance <= maxDistance)
                {
                    infoText.text = hitObject.name + " (E)";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ShowBookText(true);
                    }
                }
                else
                {
                    // Clear the information text if the player is too far away from the inspectable object
                    infoText.text = "";
                }
            }
            else
            {
                // Clear the information text if the player is too far away from the inspectable object
                infoText.text = "";
            }
        }
    }

    public void ShowBookText(bool show)
    {
        if (show)
        {
            infoText.text = "";
            bookText.SetActive(true);
            reading = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            infoText.text = "";
            bookText.SetActive(false);
            reading = false;
        }
    }

}



