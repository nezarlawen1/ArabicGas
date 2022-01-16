using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] int Health = 50;
    [SerializeField] GameObject PodTrigger;
    [SerializeField] AudioSource Killed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health<=0)
        {
            PodTrigger.SetActive(true);
            Killed.Play();
            gameObject.SetActive(false);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("collided");
        if (other.tag == "Bullet")
        {
            Health -= 5;
        }
    }
}
