using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    private PointMediator _playerPoints;

    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private int _price = 950;
    [SerializeField] private List<GameObject> _itemsList = new List<GameObject>();
    private GameObject _itemOutcome;
    private bool _activated;


    private void Start()
    {
        _playerPoints = PointMediator.Instance;
    }

    [ContextMenu("RollBox")]
    public void RollBox()
    {
        if (_playerPoints.RemovePoints(_price) && !_activated)
        {
            _activated = true;

            int itemIndex = Random.Range(0, _itemsList.Count);
            _itemOutcome = _itemsList[itemIndex];

            InstantiateItem();
        }
    }

    private void InstantiateItem()
    {
        GameObject savedItem = Instantiate(_itemOutcome, _spawnLocation.position, Quaternion.identity);
        savedItem.transform.SetParent(null);
        if (savedItem.TryGetComponent(out Gun gun))
        {
            gun.CreateMag();
        }
        else if (savedItem.TryGetComponent(out GunNoMag nomag))
        {
            nomag.CreateMag();
        }
        _activated = false;
    }
}
