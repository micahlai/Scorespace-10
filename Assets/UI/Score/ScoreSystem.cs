using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public enum ScoreType { Distance, Time};
    public ScoreType scoreType;
    public Text scoreText;
    public Text percentageText;
    public float score;
    public string stringScore;
    float startTime;
    public bool incomplete;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        startTime = Time.time;
        incomplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (percentageText != null)
        {
            percentageText.gameObject.SetActive(scoreType == ScoreType.Time);
        }
        if(scoreType == ScoreType.Distance)
        {
            
            score = Mathf.RoundToInt(FindObjectOfType<Car>().transform.position.x);
            if (score < 0)
            {
                score = 0;
            }
            stringScore = score.ToString();
        }
        else
        {
            float p = Mathf.Round((FindObjectOfType<Car>().transform.position.x / FindObjectOfType<Car>().completeDistance) * 1000) / 10;
            if(p > 100)
            {
                p = 100;
            }else if(p < 0)
            {
                p = 0;
            }
            percentageText.text = (p).ToString() + "%";
            score = Mathf.Round((Time.time - startTime) * 10) / 10;
            if (score < 0)
            {
                score = 0;
            }
            
                stringScore = score.ToString() + " sec";
            
        }
        scoreText.text = stringScore;


    }
    public void ResetTime()
    {
        startTime = Time.time;
    }
    public void RemoveTime(float seconds)
    {
        startTime += seconds;
    }
}
