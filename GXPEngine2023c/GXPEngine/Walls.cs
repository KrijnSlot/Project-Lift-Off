﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Walls : AnimationSprite
{
    public Walls(String filename, int rows, int cols, TiledObject obj = null) : base(filename, 1, 1)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(x, y);
        alpha = 0;
    }

    void Update ()
    {
        GameObject[] colisions = GetCollisions();
    }
}

