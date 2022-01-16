using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsClicked = false;
    public Image Comandor;
    [SerializeField] AudioSource[] MainMenu;
    [SerializeField] AudioSource[] Level1;
    [SerializeField] AudioSource[] Level2;
    [SerializeField] AudioSource[] Level3;

    private List<AudioSource[]> _allLists;
    private int _currentScene ;
    private int _pointer = 0;
    private void Awake()
    {
        
    }

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        _allLists = new List<AudioSource[]>() {MainMenu, Level1, Level2, Level3 };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Scene: " + _currentScene);
        Debug.Log("Pointer: " + _pointer);
        if (!(_pointer >= _allLists[_currentScene].Length))
        {
            if (_allLists[_currentScene][_pointer].isPlaying)
            {
                Comandor.gameObject.SetActive(true);
                _pointer++;
            }
        }
        if (_allLists[_currentScene][_pointer - 1].isPlaying)
        {
            Comandor.gameObject.SetActive(true);
        }
        else if (!_allLists[_currentScene][_pointer - 1].isPlaying)
        {
            Comandor.gameObject.SetActive(false);
        }
    }

    public void IsButtonClicked()
    {
        IsClicked = true;
    }
}
