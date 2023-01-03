using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomMultiInteractable : XRBaseInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractor interactor = selectingInteractor;

        IXRSelectInteractor newInteractor = firstInteractorSelecting;

        List<IXRSelectInteractor> moreInteractors = interactorsSelecting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
