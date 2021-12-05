using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bool : MonoBehaviour
{
    #region Instance
    private static Bool instance;
    public static Bool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Bool>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned Bool", typeof(Bool)).GetComponent<Bool>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    [Header("Logic")]
    private Gyroscope gyroscope;
    private Quaternion rotation;
    private bool gyroActive;

    // Start is called before the first frame update
    void Start()
    {
        EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroActive)
        {
            rotation = gyroscope.attitude;
        }
    }

    public Quaternion GetGyroRotation()
    {
        return rotation;
    }

    /*void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + gyroscope.rotationRate);
        GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + gyroscope.attitude);
        GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + gyroscope.enabled);
    }*/
    public void EnableGyro()
    {
        if (gyroActive)
        {
            return;
        }
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            gyroActive = gyroscope.enabled;
        }
    }
}
