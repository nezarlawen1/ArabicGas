using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("collided");
        if (other.tag == "Bullet")
        {
            //Debug.Log("Particle Bye");
            Destroy(gameObject);
        }
    }
}
