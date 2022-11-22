using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime;
        //if(!moved)
        //StartCoroutine(MoveDelay());
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator MoveDelay()
    {
        moved = true;
        transform.position += Vector3.forward * Time.deltaTime;
        yield return new WaitForSeconds(5);
        moved = false;
    }
}
