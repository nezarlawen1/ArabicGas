using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMediator : MonoBehaviour
{
    private PointsSystem _playerPointSystem;

    private void Awake()
    {
        _playerPointSystem = FindObjectOfType<PointsSystem>();
    }


    public void AddPoints(int amount)
    {
        _playerPointSystem.IncreasePoints(amount);
    }

    public void RemovePoints(int amount)
    {
        _playerPointSystem.DecreasePoints(amount);
    }
}
