using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointText;
    public int CurrentPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _pointText.text = CurrentPoints.ToString();
    }


    public void IncreasePoints(int amount)
    {
        CurrentPoints += amount;
    }

    public bool DecreasePoints(int amount)
    {
        if (CurrentPoints > 0)
        {
            if (CurrentPoints >= amount)
            {
                CurrentPoints -= amount;
                return true;
            }
        }
        return false;
    }
}
