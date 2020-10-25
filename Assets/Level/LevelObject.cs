using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelObject", order = 1)]
public class LevelObject : ScriptableObject
{
    public string name;

    public GameObject prefab;

    public float spawnFrequency;

    public Vector2 spawnRange;
    public float yLevel;
}
