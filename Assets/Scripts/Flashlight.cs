using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashLight;
    private bool lightOn = false;
   
    void Start()
    {
        flashLight = GetComponentInChildren<Light>();
    }

    void Update()
    {
        turnLightOn();
        if (Input.GetKeyDown(KeyCode.F))
            lightOn = !lightOn;
    }

    private void turnLightOn()
    {
        if (lightOn)
            flashLight.enabled = true;
        else if (!lightOn)
            flashLight.enabled = false;
        
    }
}
