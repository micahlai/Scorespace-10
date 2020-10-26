using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public void AddHealthNormal()
    {
        FindObjectOfType<Car>().AddHealthNormal();
    }
    public void AddHealthBonus()
    {
        FindObjectOfType<Car>().AddHealthBonus();
    }
}
