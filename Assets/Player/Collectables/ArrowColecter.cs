using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColecter : MonoBehaviour
{
    public enum DirectionCollectable { Up, Down, Left, Right, All }
    public DirectionCollectable direction;
    public Vector2 rewardAmount;
    public MeshRenderer[] arrows;
    public Color[] directionColorsMain;
    public Color[] directionColorsShade;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        int r = Mathf.RoundToInt(Random.Range(0.5f, 9.4f));
        if (r == 1 || r == 2)
        {
            direction = DirectionCollectable.Down;
            i = 0;
        } else if (r == 3 || r == 4)
        {
            direction = DirectionCollectable.Up;
            i = 1;
        } else if (r == 5 || r == 6)
        {
            direction = DirectionCollectable.Left;
            i = 2;
        } else if (r == 7 || r == 8)
        {
            direction = DirectionCollectable.Right;
            i = 3;
        }
        else if(r == 9)
        {
            direction = DirectionCollectable.All;
            i = 4;
        }
        foreach (MeshRenderer m in arrows)
        {
            m.material.SetColor("Color_F689E8B9", directionColorsMain[i]);
            m.material.SetColor("Color_28180829", directionColorsShade[i]);
            GetComponent<FloatingColectable>().deathColor = directionColorsMain[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        if(direction == DirectionCollectable.Down) { i = 0; }
        else if (direction == DirectionCollectable.Up) { i = 1; }
        else if (direction == DirectionCollectable.Left) { i = 2; }
        else if (direction == DirectionCollectable.Right) { i = 3; }
        else if (direction == DirectionCollectable.All) { i = 4; }
        foreach (MeshRenderer m in arrows)
        {
            m.material.SetColor("Color_F689E8B9", directionColorsMain[i]);
            m.material.SetColor("Color_28180829", directionColorsShade[i]);
            GetComponent<FloatingColectable>().deathColor = directionColorsMain[i];
        }

    }
    public void GivePlayer()
    {
        int amount = Mathf.RoundToInt(Random.Range(rewardAmount.x, rewardAmount.y));
        if (direction == DirectionCollectable.Left)
        {
            Placement.main.values[0] += amount;
        }
        else if (direction == DirectionCollectable.Up)
        {
            Placement.main.values[1] += amount;
        }
        else if (direction == DirectionCollectable.Down)
        {
            Placement.main.values[2] += amount;
        }
        else if (direction == DirectionCollectable.Right)
        {
            Placement.main.values[3] += amount;
        }else if(direction == DirectionCollectable.All)
        {
            Placement.main.values[0] += amount / 2;
            Placement.main.values[1] += amount / 2;
            Placement.main.values[2] += amount / 2;
            Placement.main.values[3] += amount / 2;

        }
    }
}
