using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Walls : Sprite 
{
    public Walls(String filename, int x, int y, float wScale = 0.5f) : base(filename)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(x, y);
        scale = wScale;
    }

    void Update ()
    {
        GameObject[] colisions = GetCollisions();
    }


}

