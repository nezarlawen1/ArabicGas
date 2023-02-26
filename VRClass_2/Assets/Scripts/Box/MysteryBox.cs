using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    private PointMediator _playerPoints;

    [SerializeField] private TextMeshPro _priceText;
    [SerializeField] private Transform _spawnLocation, _magSpawnLocation;
    [SerializeField] private int _magsToSpawn = 5, _shellsToSpawn = 18;
    [SerializeField] private int _price = 950;
    [SerializeField] private float _cooldown = 10;
    [SerializeField] private List<GameObject> _itemsList = new List<GameObject>();
    private GameObject _itemOutcome, _savedItem;
    private bool _activated;
    private float _cooldownTimer;

    private void OnValidate()
    {
        _priceText.text = _price.ToString();
    }

    private void Start()
    {
        _playerPoints = PointMediator.Instance;
    }

    private void Update()
    {
        CoolDownHandle();
        _priceText.text = _price.ToString();
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

                if (_savedItem.TryGetComponent(out Gun gun))
                {
                    if (gun.IsHeld && _savedItem != null)
                    {
                        _savedItem = null;

                        _activated = false;
                        _cooldownTimer = 0;
                    }
                }
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
        _savedItem = Instantiate(_itemOutcome, _spawnLocation.position, _spawnLocation.localRotation);

        if (_savedItem.TryGetComponent(out Gun gun))
        {
            for (int i = 0; i < _magsToSpawn; i++)
            {
                gun.CreateMag(_magSpawnLocation);
            }
        }
        else if (_savedItem.TryGetComponent(out GunNoMag nomag))
        {
            for (int i = 0; i < _shellsToSpawn; i++)
            {
                nomag.CreateMag(_magSpawnLocation);
            }
        }
    }
}
