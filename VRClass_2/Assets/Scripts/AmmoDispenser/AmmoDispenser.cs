using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDispenser : MonoBehaviour
{
    private PointMediator _playerPoints;

    [SerializeField] private TextMeshPro _priceText;
    [SerializeField] private Transform _magSpawnLocation;
    [SerializeField] private int _magsToSpawn = 2, _shellsToSpawn = 6;
    [SerializeField] private int _price = 500;
    [SerializeField] private List<GameObject> _itemsList = new List<GameObject>();
    private GameObject _itemOutcome;

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
        _priceText.text = _price.ToString();
    }

    [ContextMenu("Dispense Ammo")]
    public void DispenseAmmo()
    {
        if (_playerPoints.RemovePoints(_price))
        {

            int itemIndex = Random.Range(0, _itemsList.Count);
            _itemOutcome = _itemsList[itemIndex];

            InstantiateItem();
        }
    }

    private void InstantiateItem()
    {

        if (_itemOutcome.TryGetComponent(out Gun gun))
        {
            for (int i = 0; i < _magsToSpawn; i++)
            {
                gun.CreateMag(_magSpawnLocation);
            }
        }
        else if (_itemOutcome.TryGetComponent(out GunNoMag nomag))
        {
            for (int i = 0; i < _shellsToSpawn; i++)
            {
                nomag.CreateMag(_magSpawnLocation);
            }
        }
    }
}
