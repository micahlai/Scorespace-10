using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    [Range(0, 1)]
    public float target;
    public Color normal;
    Color normalColor;
    // Use this for initialization
    void Start()
    {
        normalColor = normal;
    }
    private void FixedUpdate()
    {
        if (bar.fillAmount < target)
        {
            bar.fillAmount += Time.deltaTime * Mathf.Abs(bar.fillAmount - target) * 2;
        }
        else if (bar.fillAmount > target)
        {
            bar.fillAmount -= Time.deltaTime * Mathf.Abs(bar.fillAmount - target) * 2;
        }
        normalColor = Color.Lerp(normalColor, normal, Time.deltaTime * 2);
        bar.color = Color.Lerp(normalColor, new Color(1, 1, 1, 0.75f), Mathf.Abs(bar.fillAmount - target));
        gameObject.transform.localScale = new Vector3(0.5f + (bar.fillAmount - target) * 0.6f, 0.5f + (bar.fillAmount - target) * 0.6f, 1);


    }
    // Update is called once per frame
    public void SetSize(float sizeNormalized)
    {
        target = sizeNormalized;
        if (target > 1)
        {
            target = 1;
        }
        else if (target < 0)
        {
            target = 0;
        }
    }
    public void SetColor(Color color)
    {
        normal = color;
    }
    public Color GetColor()
    {
        return normal;
    }
}
