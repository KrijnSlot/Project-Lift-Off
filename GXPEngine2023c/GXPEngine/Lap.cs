using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Lap : Sprite 
{
    public int lapCount = 0;
    //public bool checkPoint = false;


    public Lap(String filename, int x, int y) : base("hampter.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(1400, 900);
        scale = 0.3f;
        alpha = 0;
    }

    void Update()
    {
        GameObject[] colisions = GetCollisions();
    }
}
