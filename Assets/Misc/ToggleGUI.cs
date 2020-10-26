using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGUI : MonoBehaviour
{
    public bool On;
    // Start is called before the first frame update
    void Start()
    {
        On = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            On = !On;
        }
        GetComponent<Canvas>().enabled = On;
    }
}
