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

    public Player(String filename, int cols, int rows, float pScale, float pSpeed = 2f) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        scale = pScale;
        _speed = pSpeed;



    }


    void Update()
    {
        bool isPressed = false;
        if (Input.GetKey(Key.W))
        {
            velocity.y += _speed/10f;
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
            velocity = velocity * 0.001f;
            Console.WriteLine("lol");
        }
    }
}

