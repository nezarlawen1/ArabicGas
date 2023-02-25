using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMediator : MonoBehaviour
{
    public static PointMediator Instance;

    private PointsSystem _playerPointSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerPointSystem = FindObjectOfType<PointsSystem>();
    }


    public void AddPoints(int amount)
    {
        _playerPointSystem.IncreasePoints(amount);
    }

    public bool RemovePoints(int amount)
    {
        return _playerPointSystem.DecreasePoints(amount);
    }
}
