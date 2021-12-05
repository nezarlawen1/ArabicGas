using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    [SerializeField] GameObject Cube;
    Color color;

    private Quaternion localRotation;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        color = Cube.GetComponent<Renderer>().material.color;
        localRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        color = Cube.GetComponent<Renderer>().material.color;
        transform.Translate(Input.acceleration.x / 50, 0, -Input.acceleration.z / 50);
        float curSpeed = Time.deltaTime * speed;
        localRotation.y += Input.acceleration.x * curSpeed;
        localRotation.x += Input.acceleration.y * curSpeed;
        Debug.Log(Input.acceleration);
        Debug.Log(localRotation);
        Debug.Log(color);

        transform.rotation = localRotation;

        if (Input.acceleration.z > 0 && color.r < 1)
        {
            color.r += 0.001f;
            Cube.GetComponent<Renderer>().material.color = color;
        }
        else if (Input.acceleration.z < 0 && color.r > 0)
        {
            color.r -= 0.001f;
            Cube.GetComponent<Renderer>().material.color = color;
        }
        if (Input.acceleration.x > 0 && color.g < 1)
        {
            color.g += 0.001f;
            Cube.GetComponent<Renderer>().material.color = color;
        }
        else if (Input.acceleration.x < 0 && color.g > 0)
        {
            color.g -= 0.001f;
            Cube.GetComponent<Renderer>().material.color = color;
        }
        //if (color.b < 1)
        //{
        //    transform.rotation = localRotation;
        //    color.r += 0.001f;
        //    Cube.GetComponent<Renderer>().material.color = color;
        //}
        //else if (color.b > 0)
        //{
        //    transform.rotation = localRotation;
        //    color.r -= 0.001f;
        //    Cube.GetComponent<Renderer>().material.color = color;
        //}
    }
}
