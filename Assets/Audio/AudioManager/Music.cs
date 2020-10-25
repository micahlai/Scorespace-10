using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string name;

    public MusicPart[] parts;

    [Range(0f, 1f)]
    public float volume = .75f;

    public bool PlayOnAwake = false;
    public bool loop = true;
}
