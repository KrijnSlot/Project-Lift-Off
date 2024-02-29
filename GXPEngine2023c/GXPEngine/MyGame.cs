using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;                            // System.Drawing contains drawing tools such as Color definitions
using TiledMapParser;
using System.Reflection.Emit;
using System.Media;

public class MyGame : Game
{
    SoundChannel bgMusic;
    Background background;
    Background forground;
    Player player;
    Player2 player2;
    EasyDraw timeText;
    EasyDraw lapText;
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

    bool cMarco;
    bool cHam;
    bool cDiver;

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
        timeText.SetXY(game.width - 175, 35);

        lapText = new EasyDraw(game.width / 2, 50, false);
        lapText.TextFont(font);
        lapText.TextAlign(CenterMode.Center, CenterMode.Center);
        lapText.Fill(255);
        lapText.Text("LAP: " + "0", true);
        lapText.SetOrigin(timeText.width / 2, timeText.height / 2);
        lapText.SetXY(125, 35);

        player = new Player("hampter.png", 1, 1, 1500, 800, 0.7f);

        Level level = new Level("Colisions.tmx");
        LateAddChild(level);

        lap = new Lap("hampter.png", 0, 0);
        AddChild(lap);

        checkpoint = new Checkpoint("hampter.png", 0, 0, 0.5f);
       // AddChild(checkpoint);
        

    }

    void Update()
    {
        Console.WriteLine();

        Timer();
        drawTimer(highTime);
        Swtich();
        Check();
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
            timeText.Text(String.Format("Time: " + "{0}", highTime), true);
        }
    }


    void Swtich()
    {
        ComfirmCh();
        if (Input.GetKeyDown(Key.ONE))
        {
            highTime = 0;
            background = new Background("mCS.png", 3.55f);
            AddChild(background);
            RemoveChild(player);
            isMarco = true;
            isDiver = false;
            isHam = false;

        }
        if (Input.GetKeyDown(Key.TWO))
        {
            highTime = 0;
            background = new Background("hCS.png", 3.55f);
            AddChild(background);
            RemoveChild(player);
            isMarco = false;
            isDiver = false;
            isHam = true;

        }
        if (Input.GetKeyDown(Key.THREE))
        {
            highTime = 0;
            background = new Background("gCS.png", 3.55f);
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

            lapText.Text(String.Format("LAP: " + "{0}", lap.lapCount), true);

            if (isMarco == true)
            {
                
                cMarco = true;
                player = new Player("horse.png", 1, 3, 1500, 920, 0.7f);
                player2 = new Player2("sub.png", 1, 4, 1500, 980, 0.5f);
                background = new Background("dMap.png", 1.5f);
                forground = new Background("dMapDeco.png", 1.5f);
                slow = new Slow("qSand.png", 4, 1, 200, 200, 1);
                AddChild(background);
                AddChild(player);
                //AddChild(player2);
                AddChild(forground);
                AddChild(slow);
                AddChild((EasyDraw)timeText);
                AddChild((EasyDraw)lapText);
                bgMusic = new Sound("dMusic.mp3", true, false).Play();
            }

            if (isDiver == true)
            {
                cDiver = true;
                player = new Player("sub.png", 1, 4, 1500, 950, 0.5f);
                player2 = new Player2("hamster.png", 1, 14, 1500, 980, 0.5f);
                background = new Background("wMap.png", 1.5f);
                forground = new Background("wMapDeco.png", 1.5f);
                slow = new Slow("wPool.png", 4, 1, 200, 200, 1);
                AddChild(background);
                AddChild(player);
                //AddChild(player2);
                AddChild(forground);
                AddChild(slow);
                AddChild((EasyDraw)timeText);
                bgMusic = new Sound("wMusic.mp3", true, false).Play();
            }

            if (isHam == true)
            {
                cHam = true;
                player = new Player("hamster.png", 1, 14, 1500, 980, 0.5f);
                player2 = new Player2("horse.png", 1, 3, 1500, 950, 0.7f);
                background = new Background("sMap.png", 1.5f);
                forground = new Background("sMapDeco.png", 1.5f);
                slow = new Slow("meteor.png", 1, 1, 200, 200, 1);
                AddChild(background);
                AddChild(player);
                //AddChild(player2);
                AddChild(forground);
                AddChild(slow);
                AddChild((EasyDraw)timeText);
                bgMusic = new Sound("sMusic.mp3", true, false).Play();
            }
        }
    }
    
    void Check()
    {
        if (cMarco == true) rSpawn();
        if (cDiver == true) rSpawn();
        if (cHam == true) rSpawn();
    }

    void rSpawn()
    {
        Random random = new Random();
        int sRND = random.Next(0, 100);
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

        if (MyGame.isDiver == true)
        {
            //Slow slow = null;
            if (sRND == 2)
            {
                int wRND = random.Next(100, game.width - 100);
                int hRND = random.Next(100, game.height - 100);
                Slow aSlow = new Slow("wPool.png", 4, 1, wRND, hRND, 0.5f);
                AddChild(aSlow);
            }
        }

        if (MyGame.isHam == true)
        {
            //Slow slow = null;
            if (sRND == 2)
            {
                int wRND = random.Next(100, game.width - 100);
                int hRND = random.Next(100, game.height - 100);
                Slow aSlow = new Slow("meteor.png", 1, 1, wRND, hRND, 0.7f);
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