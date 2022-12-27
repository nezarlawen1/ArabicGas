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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slider1911"))
        {
            SliderTriggered = false;
        }
    }
}
