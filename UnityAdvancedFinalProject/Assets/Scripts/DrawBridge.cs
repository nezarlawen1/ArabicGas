using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.right, 20 * Time.deltaTime);
        transform.RotateAround(transform.position, -Vector3.up, 20 * Time.deltaTime);
    }
}
