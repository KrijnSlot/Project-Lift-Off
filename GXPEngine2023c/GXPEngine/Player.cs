using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : AnimationSprite
{
    
    float mSpeed = 4;

    public Player(String filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        scale = 1;

    }


    void Update()
    {
        if (Input.GetKey(Key.W))
        {
            y = y - mSpeed;
            rotation = 0;
        }
        if (Input.GetKey(Key.S))
        {
            y = y + mSpeed;
            rotation = 180;
        }
        if (Input.GetKey(Key.A))
        {
            x = x - mSpeed;
            rotation = -90;
        }
        if (Input.GetKey(Key.D))
        {
            x = x + mSpeed;
            rotation = 90;
        }

        if (Input.GetKey(Key.W) && Input.GetKey(Key.D))
        {
            rotation = 45;
        }

        if (Input.GetKey(Key.W) && Input.GetKey(Key.A))
        {
            rotation = -45;
        }

        if (Input.GetKey(Key.S) && Input.GetKey(Key.D))
        {
            rotation = 135;
        }

        if (Input.GetKey(Key.S) && Input.GetKey(Key.A))
        {
            rotation = -135;
        }
    }
}

