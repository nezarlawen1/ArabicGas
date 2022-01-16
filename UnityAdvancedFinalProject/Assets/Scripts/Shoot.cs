using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shoot : MonoBehaviour
{
    [SerializeField] ParticleSystem Bullet;
    [SerializeField] JoyStickLook JoystickLookGO;
    public float FireRate = 0.2f;
    private float _timeToShoot;
    public Image laserGunCharge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeToShoot += Time.deltaTime;
        float modifier = 100 / FireRate;
        float fillInPercentage = _timeToShoot * modifier;
        float chargeRatio = fillInPercentage / 100;
        if (chargeRatio >= 1)
        {
            laserGunCharge.fillAmount = 1;
        }
        else
        {
            laserGunCharge.fillAmount = chargeRatio;
        }
        
        if (JoystickLookGO.joystick.Horizontal>0 || JoystickLookGO.joystick.Vertical > 0 || JoystickLookGO.joystick.Horizontal < 0 || JoystickLookGO.joystick.Vertical < 0)
        {
            if (_timeToShoot >= FireRate)
            {
                _timeToShoot = 0;
                Bullet.Play();
                GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.3f);
                GetComponent<AudioSource>().Play();
            }
        }

        /*if (Input.GetMouseButton(0))
        {
            Bullet.Play();
        }*/
    }

    /*private void OnParticleCollision(GameObject other)
    {
        Debug.Log("par collided");
        if (other.tag != "Bullet")
        {
            Debug.Log("Particle Bye");
            Destroy(this);
        }
        if (other.tag == "Enemy")
        {
            Destroy(other);
        }
    }*/
}
