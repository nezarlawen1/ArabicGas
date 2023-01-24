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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (HasMultipleInteractors())
        {

        }
    }

    private bool HasMultipleInteractors()
    {
        return interactorsSelecting.Count > 1;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (HasNoInterActors())
        {

        }
    }

    private bool HasNoInterActors()
    {
        return interactorsSelecting.Count == 0;
    }
}
