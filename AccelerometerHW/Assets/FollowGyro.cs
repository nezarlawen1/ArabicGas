using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGyro : MonoBehaviour
{
    [Header("Tweaks")]
    [SerializeField] private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        Bool.Instance.EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Bool.Instance.GetGyroRotation() * baseRotation;
    }
}
