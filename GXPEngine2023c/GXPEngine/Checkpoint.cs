using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Checkpoint : Sprite
{
    

    public Checkpoint(String filename, int x, int y, float wScale) : base("hampter.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(920, 50);
        scale = wScale;
        alpha = 0;
    }

    void Update()
    {
        GameObject[] colisions = GetCollisions();
    }
}


