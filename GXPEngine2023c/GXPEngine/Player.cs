﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player : AnimationSprite
{


    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Vec2 velocity;
    public Vec2 pVelocity;
    Vec2 preVelocity = new Vec2(0, 0);
    Vec2 direction;
    Vec2 targetDir;
    Vec2 velCopy = new Vec2(0, 0);

    int _radius;
    Vec2 _position;
    float _speed;
    Lap lap;
    Checkpoint checkPoint;

    public Player(String filename, int cols, int rows, int x, int y, float pScale, float pSpeed = 2f) : base(filename, cols, rows)
    {
        lap = game.FindObjectOfType<Lap>();
        checkPoint = game.FindObjectOfType<Checkpoint>();
        SetOrigin(width / 2, height / 2);
        scale = pScale;
        _speed = pSpeed;

        _position = new Vec2(x, y);

    }


    void Update()
    {
        bool isPressed = false;
        if (Input.GetKey(Key.W))
        {
            velocity.y += _speed / 10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.S))
        {
            velocity.y -= _speed / 10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.A))
        {
            velocity.x -= _speed / 10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.D))
        {
            velocity.x += _speed / 10f;
            isPressed = true;
        }

        if (velCopy.Length() > _speed)
        {
            velCopy.Normalize();
            velCopy *= _speed;
        }

        if (!isPressed)
        {
            velocity = velocity * 0.96f;
        }

        if (isPressed)
        {
            Animate(0.05f);
        }

        float angle = Mathf.RadToDeg(velocity.GetAngle());

        rotation = -angle;


        if (velocity.Length() > _speed)
        {
            velocity.Normalize();
            velocity *= _speed - 0.1f;
        }

        if (preVelocity.Length() > _speed)
        {
            preVelocity.Normalize();
            preVelocity *= _speed - 0.1f;
        }


        if (Input.GetKey(Key.LEFT_SHIFT))
        {
            velocity.Normalize();
            velocity *= _speed ;
            velocity = preVelocity * 0.85f + velocity * 0.15f;
        }

        _position.x += velocity.x;
        _position.y -= velocity.y;
        x = _position.x;
        y = _position.y;

        GameObject[] colisions = GetCollisions();

        for (int i = 0; i < colisions.Length; i++)
        {
            OnColision(colisions[i]);
        }

        preVelocity = velocity;
    }

    void OnColision(GameObject other)
    {
        if (other is Walls)
        {
            velocity.y = -velocity.y * 2;
            velocity.x = -velocity.x * 2;
        }

        if (other is Slow)
        {
            velocity = velocity / 1.5f;
        }

        if (other is Lap)
        {
            if (checkPoint.cPoint == true)
            {
                lap.lapCount++;
                checkPoint.cPoint = false;
                Console.WriteLine(lap.lapCount);
            }
        }

        if (other is Checkpoint)
        {
            checkPoint.cPoint = true;
        }
    }
}

