using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
   internal class Background : AnimationSprite 
   {
    public Background(String filename, int x, int y, float pScale = 1) : base(filename, x, y)
    {
        SetOrigin(width / 2, height / 2);
        Sprite backgroundImage = new Sprite(filename);
        backgroundImage.scale = pScale;
        AddChild(backgroundImage);
    }
}
