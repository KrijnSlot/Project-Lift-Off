using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Player2 : AnimationSprite
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

    int _radius;
    Vec2 _position;
    float _speed;
    Lap lap;
    Checkpoint checkPoint;

    public Player2(String filename, int cols, int rows, int x, int y, float pScale, float pSpeed = 1f) : base(filename, cols, rows)
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
        if (Input.GetKey(Key.UP))
        {
            velocity.y += _speed/10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.DOWN))
        {
            velocity.y -= _speed / 10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.LEFT))
        {
            velocity.x -= _speed / 10f;
            isPressed = true;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            velocity.x += _speed / 10f;
            isPressed = true;
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
            velocity *= _speed-0.1f;
        }


        _position.x += velocity.x;
        _position.y -= velocity.y;
        x = _position.x;
        y = _position.y;

        GameObject[] colisions = GetCollisions();

        for (int i = 0; i < colisions.Length; i++)
        {
            OnColision(colisions[i]);                   //Makes coliders for every gameobject.
        }
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

