using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Reload1911 : MonoBehaviour
{
    private XRGrabInteractable _interactableGun;
    private XRIDefaultInputActions _defaultInputActions;
    private XRIDefaultInputActions.XRIRightHandActions _rightHand;

    [SerializeField] GameObject Mag;
    // Start is called before the first frame update
    void Awake()
    {
        _defaultInputActions = new XRIDefaultInputActions();
        _rightHand = _defaultInputActions.XRIRightHand;
        _interactableGun = GetComponent<XRGrabInteractable>();


        _rightHand.A_Button.performed += ctx => Reaload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reaload()
    {
        Mag.GetComponent<Rigidbody>().useGravity = true;
    }
}
