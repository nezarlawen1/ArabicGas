using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsTriggerCheck : MonoBehaviour
{
    public bool Slider1911Triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slider1911"))
        {
            Slider1911Triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slider1911"))
        {
            Slider1911Triggered = false;
        }
    }
}
