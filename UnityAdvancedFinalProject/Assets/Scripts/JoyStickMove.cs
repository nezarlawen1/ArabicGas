using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickMove : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(joystick.Direction);
        if (joystick.Vertical >= .2f)
        {
            Vector3 tempDirection = Vector3.forward;
            Vector3 moveTarget = transform.position + tempDirection;
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Vertical <= -.2f)
        {
            Vector3 tempDirection = Vector3.forward;
            Vector3 moveTarget = transform.position - tempDirection;
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
        }
        if (joystick.Horizontal >= .2f)
        {
            Vector3 tempDirection = Vector3.right;
            Vector3 moveTarget = transform.position + tempDirection;
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
        }
        else if (joystick.Horizontal <= -.2f)
        {
            Vector3 tempDirection = Vector3.right;
            Vector3 moveTarget = transform.position - tempDirection;
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
        }
    }
}
