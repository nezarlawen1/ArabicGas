using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorSystem : MonoBehaviour
{
    [SerializeField] private DamageIndicator _damageIndicatorPrefab;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Transform _player;

    private Dictionary<Transform, DamageIndicator> _indicators = new Dictionary<Transform, DamageIndicator>();

    // Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjInSight;

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjInSight += InSight;
    }

    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjInSight -= InSight;
    }

    private void Create(Transform target)
    {
        if (_indicators.ContainsKey(target))
        {
            _indicators[target].RestartIndicator();
            return;
        }
        else
        {
            DamageIndicator newIndicator = Instantiate(_damageIndicatorPrefab, transform);
            newIndicator.Register(target, _player, new Action(() => { _indicators.Remove(target); }));

            _indicators.Add(target, newIndicator);
        }
    }

    private bool InSight(Transform t)
    {
        Vector3 screenPoint = _mainCam.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
