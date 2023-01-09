using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TowHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> SecondHandGrabPoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    public enum TwoHandRotationType
    {
        None,
        First,
        Second
    };
    public TwoHandRotationType twoHandRotationType;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var point in SecondHandGrabPoints)
        {
            point.onSelectEntered.AddListener(OnSecondHandGrab);
            point.onSelectEntered.AddListener(OnSecondHandRelease);
            //point.selectEntered.AddListener(OnSecondHandGrab);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor && selectingInteractor)
        {
            //compute the rotation
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
        }

        return targetRotation;
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        //Debug.Log("Second Hand Grab");
        secondInteractor = interactor;
    }
    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {

    }
    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        //Debug.Log("Second Hand Release");
        secondInteractor = null;
    }
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        //Debug.Log("First Grab Enter");
        base.OnSelectEntered(interactor);
        if (HasMultipleInteractors())
            attachInitialRotation = interactor.attachTransform.localRotation;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (HasMultipleInteractors())
        {

        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (HasNoInterActors())
        {

        }
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        //Debug.Log("First Grab Exit");
        base.OnSelectExited(interactor);
        if (HasNoInterActors())
        {
            secondInteractor = null;
            interactor.attachTransform.localRotation = attachInitialRotation;
        }
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }

    private bool HasMultipleInteractors()
    {
        return interactorsSelecting.Count > 1;
    }
    private bool HasNoInterActors()
    {
        return interactorsSelecting.Count == 0;
    }
}
