using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab, shootPoint;
    public float bulletSpeed;

    private XRIDefaultInputActions XRIaction;
    public XRIDefaultInputActions XRILeftInteraction;
    public XRIDefaultInputActions XRIRightInteraction;
    private XRIDefaultInputActions.IXRILeftHandInteractionActions XRILeftInteraction1;
    private XRIDefaultInputActions.IXRIRightHandInteractionActions XRIRightInteraction1;

    private void Awake()
    {

        /*XRILeftInteraction = XRIaction.XRILeftHandInteraction;
        XRIRightInteraction = XRIaction.XRIRightHandInteraction;*/
    }

    private void Update()
    {
        //XRIaction.
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);

    }
}
