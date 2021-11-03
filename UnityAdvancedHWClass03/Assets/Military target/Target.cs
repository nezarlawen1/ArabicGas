using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private AudioSource _hitMarker;
    private Material _coloration;

    // Start is called before the first frame update
    void Start()
    {
        _hitMarker = GetComponent<AudioSource>();
        _coloration = GetComponent<Renderer>().material;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    IEnumerator DelayedColorChange()
    {
        _coloration.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        _coloration.color = Color.white;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Target Hit!");

        _hitMarker.pitch=Random.Range(1,2);
        _hitMarker.Play();
        StartCoroutine(DelayedColorChange());
    }
}
