using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TerminalOpen : MonoBehaviour
{
    [SerializeField] public Button Interact;

    [Header("Door 1")]
    [SerializeField] GameObject Door;
    [SerializeField] Animation Door1Animation;
    [SerializeField] AudioSource Door1Audio;
    [Header("Door 2")]
    [SerializeField] GameObject Door2;
    [SerializeField] Animation Door2Animation;
    [SerializeField] AudioSource Door2Audio;
    [Header("Door 3")]
    [SerializeField] GameObject Door3;
    [SerializeField] Animation Door3Animation;
    [SerializeField] AudioSource Door3Audio;
    [Header("Bridge")]
    [SerializeField] Animation BridgeAnimation;
    [Header("Pod")]
    [SerializeField] Animation PodAnimation;
    [Header("Win")]
    [SerializeField] Image Win;
    GameManager gameManager;
    public static bool IsClicked = false;
    private GameObject PlayerHPRef;
    private bool _usedHeal = false;

    public bool IsColliding;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHPRef = GameObject.FindGameObjectWithTag("PlayerHealth");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsClicked + this.gameObject.tag);
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Colliding with: " + gameObject.tag);
        if (other.tag == "Player")
        {
            Interact.gameObject.SetActive(true);
            switch (gameObject.tag)
            {
                case "HealingStation":
                    IsColliding = true;
                    if (IsClicked && _usedHeal == false)
                    {
                        Debug.Log("Healing");
                        PlayerHPRef.GetComponent<PlayerHP>().currentHP += 50;
                        IsClicked = false;
                        _usedHeal = true;
                        GetComponent<AudioSource>().Play();
                        GetComponent<BoxCollider>().enabled = false;
                        Interact.gameObject.SetActive(false);
                        IsColliding = false;
                    }
                    break;
                case "Terminal1":
                    IsColliding = true;
                    if (IsClicked)
                    {
                        Door1Animation.Play();
                        Door1Audio.Play();
                        IsClicked = false;
                        GetComponent<BoxCollider>().enabled = false;
                        Interact.gameObject.SetActive(false);
                        IsColliding = false;
                    }
                    break;
                case "Terminal2":
                    IsColliding = true;
                    if (IsClicked)
                    {
                        Door2Animation.Play();
                        Door2Audio.Play();
                        IsClicked = false;
                        GetComponent<BoxCollider>().enabled = false;
                        Interact.gameObject.SetActive(false);
                        IsColliding = false;
                    }
                    break;
                case "Terminal3":
                    IsColliding = true;
                    if (IsClicked)
                    {
                        Door3Animation.Play();
                        Door3Audio.Play();
                        IsClicked = false;
                        GetComponent<BoxCollider>().enabled = false;
                        Interact.gameObject.SetActive(false);
                        IsColliding = false;
                    }
                    break;
                case "Level 1 Elevator":
                    if (IsClicked)
                    {
                        SceneManager.LoadScene(2);
                        IsClicked = false;
                    }
                    break;
                case "Level 2 Elevator":
                    if (IsClicked)
                    {
                        SceneManager.LoadScene(3);
                        IsClicked = false;
                    }
                    break;
                case "Bridge":
                    IsColliding = true;
                    if (IsClicked)
                    {
                        BridgeAnimation.Play();
                        GetComponent<AudioSource>().Play();
                        IsClicked = false;
                        GetComponent<BoxCollider>().enabled = false;
                        Interact.gameObject.SetActive(false);
                        IsColliding = false;
                    }
                    break;
                case "Pod":
                    if (IsClicked)
                    {
                        PodAnimation.wrapMode = WrapMode.ClampForever;
                        PodAnimation.Play();
                    }
                    break;
                case "Win":
                    if (IsClicked)
                    {
                        Win.gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interact.gameObject.SetActive(false);
        IsColliding = false;
    }
    public void IsButtonClicked()
    {
        /*if ()
        {

        }*/
        IsClicked = true;
    }
}
