using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;
    public Transform Camera;

    public float MovementSpeed = 6f;

    public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;


    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        // Locking & Hiding Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Movement Axis Local Variables
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Rotating Player to Movement Angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Moving the Player
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDirection.normalized * MovementSpeed * Time.deltaTime);
        }
    }
}
