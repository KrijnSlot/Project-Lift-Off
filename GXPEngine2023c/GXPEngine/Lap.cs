using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Lap : Sprite 
{
    public int lapCount = 0;

    public Lap(String filename, int x, int y, float wScale) : base("hampter.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(1400, 700);
        scale = wScale;
    }

    void Update()
    {
        GameObject[] colisions = GetCollisions();

        if (Input.GetKeyDown(Key.O))
        {
            lapCount = 3;
        }
    }
}
