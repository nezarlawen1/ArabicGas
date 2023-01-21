using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollider : MonoBehaviour
{
    public delegate void OnHitCollider(HealthCollider hCol);
    public event OnHitCollider OnHit;

    public float DamageMultiplier = 1f;
    public GameObject DamagerObjRef;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DamagerObjRef != null)
        {
            DamagerObjRef = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damager"))
        {
            DamagerObjRef = other.gameObject;
            if (OnHit != null) OnHit(this);
        }
    }
}
