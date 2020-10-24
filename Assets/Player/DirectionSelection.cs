using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionSelection : MonoBehaviour
{
    public int index;
    public Vector2[] selectionPositions;
    public RectTransform selection;
    public Text[] amounts;
    public float[] status;
    public float[] displayStatusApprox;
    public int[] displayStatus;
    int[] startFontSize;
    public Color[] colors;
    public Image[] image;
    public Color[] imageColor;
    public float[] redAmount;
    // Start is called before the first frame update
    void Start()
    {
        startFontSize = new int[4];
        for (int i = 0; i < amounts.Length; i++)
        {
            startFontSize[i] = amounts[i].fontSize;
            image[i].color = colors[i];
            amounts[i].color = colors[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < status.Length; i++)
        {
            displayStatusApprox[i] = Mathf.Lerp(displayStatusApprox[i], status[i], Time.unscaledDeltaTime * 2);
            amounts[i].fontSize = Mathf.RoundToInt(Mathf.Abs(status[i] - displayStatusApprox[i]) / 2) + startFontSize[i];
            displayStatus[i] = Mathf.RoundToInt(displayStatusApprox[i]);
            float c = Mathf.Abs(status[i] - displayStatusApprox[i]) / 15;
            if (c > 1)
            {
                c = 1;
            }
            imageColor[i] = Color.Lerp(colors[i], Color.white, c);
            image[i].rectTransform.localScale = new Vector2(Mathf.Abs(status[i] - displayStatusApprox[i]) / 8.5f + 1, Mathf.Abs(status[i] - displayStatusApprox[i]) / 8.5f + 1);
            amounts[i].rectTransform.localScale = new Vector2(Mathf.Abs(status[i] - displayStatusApprox[i]) / 8.5f + 1, Mathf.Abs(status[i] - displayStatusApprox[i]) / 8.5f + 1);
            redAmount[i] = Mathf.Lerp(redAmount[i], 0, Time.deltaTime * 2);
            image[i].color = Color.Lerp(imageColor[i], Color.red, redAmount[i]);
            amounts[i].color = Color.Lerp(imageColor[i], Color.red, redAmount[i]);

        }

        
        StartCoroutine(Move(selection, selectionPositions[index]));
        selection.sizeDelta = new Vector2(55, 55);
        DirectionalControl.Direction d = FindObjectOfType<Placement>().currentDirection;
        if (d == DirectionalControl.Direction.Left)
        {
            index = 0;
        }
        if (d == DirectionalControl.Direction.Up)
        {
            index = 1;
        }
        if (d == DirectionalControl.Direction.Down)
        {
            index = 2;
        }
        if (d == DirectionalControl.Direction.Right)
        {
            index = 3;
        }
        for (int i = 0; i < amounts.Length; i++)
        {
            amounts[i].text = Mathf.RoundToInt(displayStatus[i]).ToString();
        }
    }
    IEnumerator Move(RectTransform rt, Vector2 targetPos)
    {
        float step = 0;
        while (step < 1)
        {
            rt.offsetMin = Vector2.Lerp(rt.offsetMin, targetPos, step += Time.deltaTime * 2);
            rt.offsetMax = Vector2.Lerp(rt.offsetMax, targetPos, step += Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();
        }
    }
    public void Error(int index)
    {
        redAmount[index] = 1;
    }
}
