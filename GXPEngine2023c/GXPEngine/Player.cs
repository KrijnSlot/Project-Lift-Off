using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
    Vec2 velCopy = new Vec2(0, 0);
    Vec2 dir = new Vec2(0,0);

    public float maxTimer = 2000f;
    public float timer;

    int _radius;
    Vec2 _position;
    float _speed;
    Lap lap;
    Checkpoint checkPoint;
    bool drifting = false;
    Arrow aVelocity;
    Arrow aPreVelocity;
    bool isSlowed = false;
    public static bool cPoint = false;

    public Player(String filename, int cols, int rows, int x, int y, float pScale, float pSpeed = 1.5f) : base(filename, cols, rows)
    {
        lap = game.FindObjectOfType<Lap>();
        checkPoint = game.FindObjectOfType<Checkpoint>();
        SetOrigin(width / 2, height / 2);
        scale = pScale;
        _speed = pSpeed;

        _position = new Vec2(x, y);

        aVelocity = new Arrow(_position, velocity, 100, 0xff0000ff, 10);
        aPreVelocity = new Arrow(_position, preVelocity, 100, 0xff5733, 10);
        //AddChild(aVelocity);
       // AddChild(aPreVelocity);
    }


    void Update()
    {
        Timer();

        drifting = false;
        velocity.x = 0;
        velocity.y = 0;

        bool isPressed = false;
        if (Input.GetKey(Key.A))
        {
            rotation -= 2;
            rotation = (rotation % 360);
            isPressed = true;
        }
        if (Input.GetKey(Key.D))
        {
            rotation += 2;
            rotation = (rotation % 360);
            isPressed = true;
        }

        dir = Vec2.GetUnitVectorDeg(-rotation);

        if (Input.GetKey(Key.LEFT_SHIFT))
        {
            drifting = true;
        }

        if (Input.GetKey(Key.W))
        {
            isPressed = true;

            if(drifting == true)
            {
                //velocity = preVelocity + (velocity * 0.1f);
                velocity = dir * _speed + preVelocity * 0.5f;
                preVelocity = velocity;
            }
            else
            {
                velocity += dir * _speed;
                preVelocity = velocity;
            }
        }
        if (Input.GetKey(Key.S))
        {
            velocity = dir * -_speed;
            isPressed = true;
        }

        if (velocity.Length() > _speed)
        {
            velocity.Normalize();
            velocity *= _speed;
        }

        if (isPressed)
        {
            Animate(0.05f);
        }

        float angle = Mathf.RadToDeg(velocity.GetAngle());

        if (velocity.Length() > _speed)
        {
            //velocity.Normalize();
           // velocity *= _speed - 0.1f;
        }

        if (preVelocity.Length() > _speed)
        {
            preVelocity.Normalize();
            preVelocity *= _speed - 0.1f;
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

        aVelocity.startPoint = _position;
        aPreVelocity.startPoint = _position;

        aVelocity.vector.x = velocity.x;
        aPreVelocity.vector.x = preVelocity.x;

        aVelocity.vector.y = -velocity.y;
        aPreVelocity.vector.y = -preVelocity.y;

        //Console.WriteLine(preVelocity);


        //Console.WriteLine(cPoint);
    }

    void Timer()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            timer = 0;
            isSlowed = false;
            _speed = 1.5f;
        }
    }

    void OnColision(GameObject other)
    {
        if (other is Walls)
        {
            _position.x -= velocity.x * 1.1f;
            _position.y += velocity.y * 1.1f;
        }

        if ((other is Slow) && (isSlowed == false))
        {
            _speed = _speed / 1.5f;
            isSlowed = true;
            maxTimer = 2000f;
        }

        if ((other is Lap) && (cPoint == true))
        {
                lap.lapCount++;
                cPoint = false;
                Console.WriteLine(lap.lapCount);
        }

        if (other is Checkpoint)
        {
            cPoint = true;
        }
    }
}

