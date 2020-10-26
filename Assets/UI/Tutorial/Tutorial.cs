using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public int index;
    public Sprite[] images;
    public string[] messages;
    public Text messageText;
    public Image picture;
    public UnityEvent endingEvent;
    public bool AcrossSceneData = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        messageText.text = messages[index];
        picture.sprite = images[index];
    }

    public void NextSlide()
    {
        if (index < messages.Length - 1)
        {
            index++;
        }
        else
        {
            ResetIndex();
            if (AcrossSceneData)
            {
                FindObjectOfType<AcrossSceneTransfer>().data[0] = 1;
            }
            endingEvent.Invoke();
            gameObject.SetActive(false);
            
        }
    }
    public void PreviousSlide()
    {
        if (index > 0)
        {
            index--;
        }
    }
    public void ResetIndex()
    {
        index = 0;
    }
}
