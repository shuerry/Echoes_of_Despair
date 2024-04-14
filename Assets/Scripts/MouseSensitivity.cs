using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public Slider mouseSlider;
    // Start is called before the first frame update
    void Start()
    {
        mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeSlider(float Value)
    {
        CameraController.sensitivity = Value;
        Debug.Log("Sensitivity is now: " + CameraController.sensitivity);
        PlayerPrefs.SetFloat("MouseSensitivity", Value);
        PlayerPrefs.Save();
    }
}
