using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    private PointMediator _playerPoints;

    [SerializeField] private GameObject _button;
    [SerializeField] private AudioSource _openSFX;
    [SerializeField] private Animator _doorAnim;
    [SerializeField] private int _price = 1000;
    [SerializeField] private bool _open, _slider;

    public bool Open { get => _open; }


    // Start is called before the first frame update
    private void Start()
    {
        _playerPoints = PointMediator.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider)
        {
            _doorAnim.SetBool("Slider", true);
        }
        else
        {
            _doorAnim.SetBool("Slider", false);
        }

        if (_open)
        {
            _button.SetActive(false);
            _doorAnim.SetBool("Open", true);
        }
        else
        {
            _button.SetActive(true);
            _doorAnim.SetBool("Open", false);
        }
    }

    public void OpenDoor()
    {
        if (_playerPoints.RemovePoints(_price) && !_open)
        {
            _open = true;
            _openSFX.Play();
        }
    }
}
