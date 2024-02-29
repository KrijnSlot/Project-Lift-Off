using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Slow : AnimationSprite
{

    // List<Slow> slowList = new List<Slow>();
    private float timePassed = 0f;
    private float lastingTime = 4000f;
    public Slow(String filename, int rows, int cols, int x, int y, float wScale) : base(filename, rows, cols)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(x, y);
        scale = wScale;
    }

    void Update()

    {
        //GameObject[] colisions = GetCollisions();
        Animate(0.03f);
        timePassed += Time.deltaTime;
        if(timePassed > lastingTime)
        {
           this.Destroy();
        }

        GameObject[] colisions = GetCollisions();
    }

    void OnColision(GameObject other)
    {
        if (other is Walls)
        {
            Console.WriteLine("hit walls slow");
            this.Destroy();
        }
    }

}

