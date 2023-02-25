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


    private void Awake()
    {
        _playerPoints = PointMediator.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //RollBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RollBox()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RollBox();
        }
    }
}
