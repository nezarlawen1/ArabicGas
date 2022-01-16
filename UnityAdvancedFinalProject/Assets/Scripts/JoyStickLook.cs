using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickLook : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] Transform player;
    private Transform current;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float targetAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        player.rotation = Quaternion.Euler(0, targetAngle, 0);
    }

}
