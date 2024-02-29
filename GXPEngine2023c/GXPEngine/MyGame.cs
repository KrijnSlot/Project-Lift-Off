using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;                            // System.Drawing contains drawing tools such as Color definitions
using TiledMapParser;
using System.Reflection.Emit;

public class MyGame : Game
{

    Background background;
    Player player;
    Player2 player2;
    EasyDraw timeText;
    Font font;
    List<Walls> wallList = new List<Walls>();
    Slow slow;
    Lap lap;
    Checkpoint checkpoint;

    public float maxTimer = 1000f;
    public float timer;
    int highTime;

    public static bool isMarco;
    public static bool isHam;
    public static bool isDiver;

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

        player = new Player("hampter.png", 1, 1, 1500, 800, 0.7f);

        slow = new Slow("qSand.png", 4, 1, 200, 200, 1);

        
       

        lap = new Lap("hampter.png", 0, 0, 0.5f);
        AddChild(lap);

        checkpoint = new Checkpoint("hampter.png", 0, 0, 0.5f);
        AddChild(checkpoint);

    }

    void Update()
    {

        Timer();
        drawTimer(highTime);
        Swtich();
        rSpawn();
        lapWin();
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
            highTime = 0;
            background = new Background("desertBG.png", 3.7f);
            AddChild(background);
            RemoveChild(player);
            isMarco = true;
            isDiver = false;
            isHam = false;

        }
        if (Input.GetKeyDown(Key.TWO))
        {
            highTime = 0;
            background = new Background("spaceBG.png", 3.7f);
            AddChild(background);
            RemoveChild(player);
            isMarco = false;
            isDiver = false;
            isHam = true;

        }
        if (Input.GetKeyDown(Key.THREE))
        {
            highTime = 0;
            background = new Background("waterBG.png", 3.7f);
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
            Remove(player);
            

            if (isMarco == true)
            {
                player = new Player("horse.png", 1, 3, 1500, 920, 0.7f);
                player2 = new Player2("hamster.png", 1, 14, 1500, 980, 0.5f);
                background = new Background("dMap.png", 1.5f);
                Level level = new Level("Colisions.tmx");
                AddChild(background);
                AddChild(slow);
                AddChild(player);
                AddChild(player2);
                AddChild((EasyDraw)timeText);
                LateAddChild(level);

            }

            if (isDiver == true)
            {
                player = new Player("sub.png", 1, 4, 1500, 950, 0.5f);
                AddChild(player);
                AddChild((EasyDraw)timeText);
            }

            if (isHam == true)
            {
                player = new Player("hamster.png", 1, 14, 1500, 950, 0.5f);
                AddChild(player); 
                AddChild((EasyDraw)timeText);
            }
        }
    }

    void rSpawn()
    {
        Random random = new Random();
        int sRND = random.Next(0, 500);
        // Console.WriteLine(rnd);


        if (MyGame.isMarco == true)
        {
            //Slow slow = null;
            if (sRND == 2)
            {
                int wRND = random.Next(100, game.width - 100);
                int hRND = random.Next(100, game.height - 100);
                Slow aSlow = new Slow("qSand.png", 4, 1, wRND, hRND, 0.5f);
                AddChild(aSlow);
            }
        }
    }

    void lapWin()
    {
        if (lap.lapCount == 3)
        {
            Console.WriteLine("lapwin");
            maxTimer = 10000000000000000000000f;
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}