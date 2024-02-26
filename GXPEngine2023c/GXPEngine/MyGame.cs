using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;                            // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{

    Background background;
    Player player;
    EasyDraw timeText;
    Font font;
    Walls walls;
    List<Walls> wallList = new List<Walls>();

    float maxTimer = 1000f;
    float timer;
    int highTime;

    bool isMarco;
    bool isHam;
    bool isDiver;
    bool comCharatcer;

    public bool gameRunning = false;
    public MyGame() : base(1920, 1080, false)
    {
        font = Utils.LoadFont("ARCADE_R.TTF", 28);
        timeText = new EasyDraw(game.width / 2, 50, false);
        timeText.TextFont(font);
        timeText.TextAlign(CenterMode.Center, CenterMode.Center);
        timeText.Fill(255);
        timeText.Text("0", true);
        timeText.SetOrigin(timeText.width / 2, timeText.height / 2);
        timeText.SetXY(game.width / 2, 35);

        player = new Player("hampter.png", 1, 1, 0.7f);

        AddChild((EasyDraw)timeText);

        walls = new Walls("hampter.png", width / 2, height / 2);
        
    }

    void Update()
    {

        Timer();
        drawTimer(highTime);
        Swtich();

    }

    void Timer()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            timer = 0;
            highTime++;
        }
    }

    void drawTimer(int highTime)
    {
        if (timeText != null)
        {
            timeText.Text(String.Format("{0}", highTime), true);
        }
    }

    void Swtich()
    {
        ComfirmCh();
        if (Input.GetKeyDown(Key.ONE))
        {
            background = new Background("desertBG.png", 3.5f);
            AddChild(background);
            AddChild(walls);
            RemoveChild(player);
            isMarco = true;
            isDiver = false;
            isHam = false;

        }
        if (Input.GetKeyDown(Key.TWO))
        {
            background = new Background("spaceBG.png", 3.5f);
            AddChild(background);
            RemoveChild(player);
            isMarco = false;
            isDiver = false;
            isHam = true;

        }
        if (Input.GetKeyDown(Key.THREE))
        {
            background = new Background("waterBG.png", 3.5f);
            AddChild(background);
            RemoveChild(player);
            isMarco = false;
            isDiver = true;
            isHam = false;

        }
    }

    void ComfirmCh()
    {
        if (Input.GetKeyDown(Key.ENTER))
        {
            if (isMarco == true)
            {
                                player = new Player("horse.png", 1, 3, 0.7f);
                AddChild(player);
                comCharatcer = false;
            }

            if (isDiver == true)
            {
                player = new Player("sub.png", 1, 4, 0.5f);
                AddChild(player);
                comCharatcer = false;
            }

            if (isHam == true)
            {
                player = new Player("hamster.png", 1, 14, 0.5f);
                AddChild(player);
                comCharatcer = false;
            }
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}