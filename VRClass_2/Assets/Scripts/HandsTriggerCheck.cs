using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsTriggerCheck : MonoBehaviour
{
    public bool SliderTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slider1911"))
        {
            SliderTriggered = true;
        }
        if (other.CompareTag("SliderAK47"))
        {
            SliderTriggered = true;
        }
        if (other.CompareTag("SliderShotGun"))
        {
            SliderTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slider1911"))
        {
            SliderTriggered = false;
        }
        if (other.CompareTag("SliderAK47"))
        {
            SliderTriggered = false;
        }
        if (other.CompareTag("SliderShotGun"))
        {
            SliderTriggered = false;
        }
    }
}
