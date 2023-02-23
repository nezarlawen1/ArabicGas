using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTag : XRSocketInteractor
{
    public List<string> targetTag;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        foreach (var item in targetTag)
        {
            return base.CanSelect(interactable) && interactable.CompareTag(item);

        }
        return true;
    }
}
