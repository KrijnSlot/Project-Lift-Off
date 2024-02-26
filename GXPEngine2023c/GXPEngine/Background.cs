using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
   internal class Background : Sprite 
   {
    public Background(String filename, float pScale) : base(filename)
    {
        SetOrigin(width / 2, height / 2);
        Sprite backgroundImage = new Sprite(filename);
        backgroundImage.scale = pScale;
        AddChild(backgroundImage);
    }
}
