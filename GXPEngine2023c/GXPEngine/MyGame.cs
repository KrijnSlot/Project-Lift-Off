using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;    // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{

	Background background;
	Player player;
	EasyDraw timeText;
	Font font;

	float maxTimer = 1000f;
	float timer;
	int highTime;

	public bool gameRunning = false;
	public MyGame() : base(1920, 1080, false)    
	{
		font = Utils.LoadFont("ARCADE_R.TTF", 28);
        timeText = new EasyDraw(game.width / 2, 50, false);
        timeText.TextFont(font);
        timeText.TextAlign(CenterMode.Center, CenterMode.Center);
        timeText.Fill(0, 160, 180);
        timeText.Text("0", true);
        timeText.SetOrigin(timeText.width / 2, timeText.height / 2);
        timeText.SetXY(game.width / 2, 35);

        background = new Background("map.png", 1900, 1000);
		AddChild(background);

		player = new Player("horse.png", 1, 1);
		AddChild(player);

		AddChild((EasyDraw)timeText);

    }

	void Update() {
        // Empty

        Timer();
		drawTimer(highTime);
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

    static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}