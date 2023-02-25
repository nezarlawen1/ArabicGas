using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    private PointMediator _playerPoints;

    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private int _price = 950;
    [SerializeField] private float _cooldown = 10;
    [SerializeField] private List<GameObject> _itemsList = new List<GameObject>();
    private GameObject _itemOutcome, _savedItem;
    private bool _activated;
    private float _cooldownTimer;


    private void Start()
    {
        _playerPoints = PointMediator.Instance;
    }

    private void Update()
    {
        CoolDownHandle();
    }

    private void CoolDownHandle()
    {
        if (_activated)
        {
            if (_cooldownTimer >= _cooldown)
            {
                _activated = false;
                _cooldownTimer = 0;

                Destroy(_savedItem);
                _savedItem = null;
            }
            else
            {
                _cooldownTimer += Time.deltaTime;
            }
        }
        else
        {
            _cooldownTimer = 0;
        }
    }

    [ContextMenu("RollBox")]
    public void RollBox()
    {
        if (!_activated)
        {
            if (_playerPoints.RemovePoints(_price))
            {
                _activated = true;

                int itemIndex = Random.Range(0, _itemsList.Count);
                _itemOutcome = _itemsList[itemIndex];

                InstantiateItem();
            }
        }
    }

    private void InstantiateItem()
    {
        _savedItem = Instantiate(_itemOutcome, _spawnLocation.position, _spawnLocation.localRotation, _spawnLocation);
        _savedItem.GetComponent<Rigidbody>().useGravity = false;
        _savedItem.GetComponent<Rigidbody>().isKinematic = true;
        //if (_savedItem.TryGetComponent(out Gun gun))
        //{
        //    gun.CreateMag();
        //}
        //else if (_savedItem.TryGetComponent(out GunNoMag nomag))
        //{
        //    nomag.CreateMag();
        //}
    }
}
