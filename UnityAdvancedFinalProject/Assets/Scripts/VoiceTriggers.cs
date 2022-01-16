using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTriggers : MonoBehaviour
{
    [SerializeField] AudioSource VoiceLine;
    [SerializeField] Camera PlayerCam;
    [SerializeField] Camera CrabCam;
    [SerializeField] GameObject Crab;
    [SerializeField] AudioSource CrabRoar;
    [SerializeField] AudioSource CrabMusic;


    public float _timeToPoof = 6f;
    private float _timer;
    private bool CrabEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        //AS = GetComponentInChildren<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CrabEvent)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= _timeToPoof)
        {
            //AS.Play();
            //VoiceLine.Play();
            CrabCam.gameObject.SetActive(false);
            Crab.GetComponent<EnemyAI>().enabled = true;
            PlayerCam.gameObject.SetActive(true);
            PlayerCam.GetComponentInParent<JoyStickMove>().moveSpeed = 10;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "2ndOrder":
                    VoiceLine.Play();
                    break;
                case "Lvl2Final":
                    VoiceLine.Play();
                    break;
                case "BOSS":
                    //AS.Play();
                    CrabMusic.Play();
                    CrabRoar.Play();
                    PlayerCam.gameObject.SetActive(false);
                    PlayerCam.GetComponentInParent<JoyStickMove>().moveSpeed = 0;
                    CrabCam.gameObject.SetActive(true);
                    CrabEvent = true;
                    break;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "BOSS":
                    VoiceLine.Play();
                    break;
            }
        }
    }
}
