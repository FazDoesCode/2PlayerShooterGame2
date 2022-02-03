using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int currentRes = 1;
        public int resScale = 1;
        bool escapeKeyWasPressed = false;
        bool f4KeyWasPressed = false;
        bool enterKeyWasPressed = false;
        bool isClicking = false;
        Point mousePos;
        bool clicked;

        //Declaring Backgrounds
        Texture2D mainMenuSprite;
        Texture2D controlsScreen;
        Texture2D mapSprite;
        Texture2D plainsBackgroundSprite;
        Texture2D desertBackgroundSprite;
        Texture2D snowBackgroundSprite;
        Texture2D swampBackgroundSprite;
        
        //Declaring Buttons
        Texture2D oneButton;
        Texture2D twoButton;
        Texture2D coopButton;
        Texture2D singleplayerButton;
        Texture2D exitButton;
        Texture2D settingsButton;

        //Declaring Players
        Texture2D coopMapSprite;
        Texture2D redguySprite;
        Texture2D redguyDodgeSprite;
        Texture2D blueguySprite;
        Texture2D blueguyDodgeSprite;
        Texture2D graveStoneSprite;
        Texture2D redguyFlipped;
        Texture2D coopGuysFlipped;
        Texture2D redguyHurt1;
        Texture2D redguyHurt2;
        Texture2D blueguyHurt1;
        Texture2D blueguyHurt2;
        Texture2D redguyHurt1Dodge;
        Texture2D redguyHurt2Dodge;

        //Declaring Enemies
        Texture2D smileyEnemySprite;
        Texture2D smileyChieftanSprite;

        // Declaring misc
        Texture2D mountainSprite;
        Texture2D gateSprite;
        Texture2D bulletSprite;
        Texture2D cloudSprite;
        Texture2D whiteSquareSprite;

        //Listing stuff
        List<Bullet> bullets = new List<Bullet>();
        List<Mountain> mountains = new List<Mountain>();
        List<Smiley> smileys = new List<Smiley>();
        List<SmileyChieftain> smileyCheiftain = new List<SmileyChieftain>();

        // Main menu stuff
        bool isInMainMenu = true;

        // In game stuff
        bool gameHasStarted = false;
        bool inWorldMap = false;
        bool inCombat = false;
        bool inCoop = false;
        bool inSingleplayer = false;
        Vector2 mapPos;
        Vector2 targetPos;
        bool isMapMoving = false;
        double mapSpeed = 0.85;

        float distanceToTravelX;
        float distanceToTravelY;
        float distanceToTravelTotal;
        float movementX;
        float movementY;

        Vector2 lastKnownPos;

        bool isInPlains = false;
        bool isInSnow = false;
        bool isInDesert = false;
        bool isInSwamp = false;
        double timeSinceLastEncounter = 0;
        double timeSinceLastEncounterAttempt = 0;

        // Enemies
        int healthMultiplier = 1;
        double timeSinceLastSmileySpawn;
        // Enemy to fight values:
        // 0 = none
        // 1 = smiley
        // 2 = smiley chieftain
        int enemyToFight = 0;

        // Player stuff
        int redguyHealth;
        int blueguyHealth;

        double redInvulnTimerH;
        double blueInvulnTimerH;
        int redInvulnTimeH = 500;
        int blueInvulnTimeH = 500;

        // Position & walking stuff
        Vector2 redguyPos = new Vector2(162, 258);
        Vector2 blueguyPos = new Vector2(60, 330);
        double redTimeSinceLastWalked = 0;
        double blueTimeSinceLastWalked = 0;
        int redWalkSoundDelay = 400; // These are used to make walk sounds better
        int blueWalkSoundDelay = 400; // They are kept seperate so they can speed up if the speed powerup is picked up

        // Dodging stuff
        int redDodgeDelay = 1200; // Red has 1.2 seconds of time between dodges, same as blue
        int redInvulnTimeD = 500; // 500ms of invincibility
        double redtimeSinceLastDodge = 0;
        double redInvulnTimerD = 0; // These are used in the dodge timer

        int blueDodgeDelay = 1200;
        int blueInvulnTimeD = 500;
        double bluetimeSinceLastDodge = 0;
        double blueInvulnTimer = 0;

        float combatSpeed = 3;
        int dodgeDistance = 60;

        // Firing Stuff
        bool redfireDelay = false; // booleans used to determine whether or not red or blue can fire
        bool redIsDodging = false; // This is used to stop dodge spam

        bool bluefireDelay = false;
        bool blueIsDodging = false;

        // Declaring rectangles so I can use them in collisions
        public Rectangle redguyMapRect;
        public Rectangle coopMapRect;
        public Rectangle redguyRect;
        public Rectangle redguyBody;
        public Rectangle redguyHead;
        public Rectangle blueguyRect;
        public Rectangle blueguyBody;
        public Rectangle blueguyHead;
        public Rectangle mouseRect;
        public Rectangle singleplayerButtonRect;
        public Rectangle coopButtonRect;
        public Rectangle settingsButtonRect;
        public Rectangle exitButtonRect;
        public Rectangle gateRect;

        public Rectangle plainsRect = new Rectangle(400, 0, 300, 345);
        public Rectangle snowRect= new Rectangle(100, 0, 310, 245);
        public Rectangle desertRect = new Rectangle(100, 235, 300, 240);
        public Rectangle swampRect = new Rectangle(400, 330, 300, 300);

        // Red Guy Controls
        Keys redguyMoveUp = Keys.W;
        Keys redguyMoveDown = Keys.S;
        Keys redguyMoveLeft = Keys.A;
        Keys redguyMoveRight = Keys.D;
        Keys redguyShoot = Keys.F;
        Keys redguyDodge = Keys.G;

        // Blue Guy Controls
        Keys blueguyMoveUp = Keys.Up;
        Keys blueguyMoveDown = Keys.Down;
        Keys blueguyMoveLeft = Keys.Left;
        Keys blueguyMoveRight = Keys.Right;
        Keys blueguyShoot = Keys.OemPeriod;
        Keys blueguyDodge = Keys.OemComma;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int windowTitleThing = new Random().Next(1, 19);
            switch (windowTitleThing)
            {
                case 1:
                    Window.Title = "The greatest sequel ever";
                    break;
                case 2:
                    Window.Title = "2playershootergame best game 1997";
                    break;
                case 3:
                    Window.Title = "GLOCKENSPIEL!!!";
                    break;
                case 4:
                    Window.Title = "holy moly now THIS is gaming";
                    break;
                case 5:
                    Window.Title = "you know what you have to do.";
                    break;
                case 6:
                    Window.Title = "in what universe?";
                    break;
                case 7:
                    Window.Title = "video games!";
                    break;
                case 8:
                    Window.Title = "amazing";
                    break;
                case 9:
                    Window.Title = "title length";
                    break;
                case 10:
                    Window.Title = "minecraft.exe";
                    break;
                case 11:
                    Window.Title = "It looks like spain";
                    break;
                case 12:
                    Window.Title = "KILL THE IMPOSTOR!!!!!";
                    break;
                case 13:
                    Window.Title = "shoutout stack overflow for free code";
                    break;
                case 14:
                    Window.Title = "woooo, monogame!";
                    break;
                case 15:
                    Window.Title = "only the funniest";
                    break;
                case 16:
                    Window.Title = "minecraft.exe";
                    break;
                case 17:
                    Window.Title = "huh, it really DOES look like spain.";
                    break;
                case 18:
                    Window.Title = "greens-kun";
                    break;
                default:
                    Window.Title = "sample text";
                    break;

            }
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();  

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Backgrounds
            mainMenuSprite = Content.Load<Texture2D>("Backgrounds/mainmenu");
            controlsScreen = Content.Load<Texture2D>("Backgrounds/Controlsscreen");
            mapSprite = Content.Load<Texture2D>("Backgrounds/map");
            plainsBackgroundSprite = Content.Load<Texture2D>("Backgrounds/plainsbackground");
            desertBackgroundSprite = Content.Load<Texture2D>("Backgrounds/desertbackground");
            snowBackgroundSprite = Content.Load<Texture2D>("Backgrounds/snowbackground");
            swampBackgroundSprite = Content.Load<Texture2D>("Backgrounds/swampbackground");

            //Buttons
            oneButton = Content.Load<Texture2D>("Items/1xbutton");
            twoButton = Content.Load<Texture2D>("Items/2xbutton");
            coopButton = Content.Load<Texture2D>("Items/coopbutton");
            exitButton = Content.Load<Texture2D>("Items/exitbutton");
            singleplayerButton = Content.Load<Texture2D>("Items/singleplayerbutton");
            settingsButton = Content.Load<Texture2D>("Items/settingsbutton");

            //Players
            coopMapSprite = Content.Load<Texture2D>("Players/CoopGuys");
            redguySprite = Content.Load<Texture2D>("Players/Redguy");
            blueguySprite = Content.Load<Texture2D>("Players/Blueguy");
            redguyDodgeSprite = Content.Load<Texture2D>("Players/Redguydodgelarge");
            blueguyDodgeSprite = Content.Load<Texture2D>("Players/Blueguydodgelarge");
            graveStoneSprite = Content.Load<Texture2D>("Players/Gravestone");
            redguyFlipped = Content.Load<Texture2D>("Players/redguyflip");
            coopGuysFlipped = Content.Load<Texture2D>("Players/CoopGuysFlipped");
            redguyHurt1 = Content.Load<Texture2D>("Players/redguyhurt1");
            redguyHurt2 = Content.Load<Texture2D>("Players/redguyhurt2");
            blueguyHurt1 = Content.Load<Texture2D>("Players/blueguyhurt1");
            blueguyHurt2 = Content.Load<Texture2D>("Players/blueguyhurt2");
            redguyHurt1Dodge = Content.Load<Texture2D>("Players/redguyhurt1dodge");
            redguyHurt2Dodge = Content.Load<Texture2D>("Players/redguyhurt2dodge");

            //Enemies
            smileyEnemySprite = Content.Load<Texture2D>("Enemies/Smiley");
            smileyChieftanSprite = Content.Load<Texture2D>("Enemies/SmileyChieftain");

            //Misc
            mountainSprite = Content.Load<Texture2D>("Scenery/Mountain");
            cloudSprite = Content.Load<Texture2D>("Scenery/cloud");
            gateSprite = Content.Load<Texture2D>("Scenery/gatewip");
            bulletSprite = Content.Load<Texture2D>("Items/Bullet");
            whiteSquareSprite = Content.Load<Texture2D>("Items/whitesquare");
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            clicked = mouseState.LeftButton == ButtonState.Pressed;

            if (mouseState.LeftButton == ButtonState.Released)
            {
                isClicking = false;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].MoveBullet();
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Debug.WriteLine(mouseState.X + "," + mouseState.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F4) && !gameHasStarted)
            {
                if (currentRes == 1 && f4KeyWasPressed == false)
                {
                    f4KeyWasPressed = true;
                    resScale = 2;
                    plainsRect = new Rectangle(400 * resScale, 0 * resScale, 300 * resScale, 345 * resScale);
                    snowRect = new Rectangle(100 * resScale, 0 * resScale, 310 * resScale, 245 * resScale);
                    desertRect = new Rectangle(100 * resScale, 235 * resScale, 300 * resScale, 240 * resScale);
                    swampRect = new Rectangle(400 * resScale, 330 * resScale, 300 * resScale, 300 * resScale);
                    ClearScenery();
                    SpawnScenery();
                    _graphics.PreferredBackBufferWidth = 1600;
                    _graphics.PreferredBackBufferHeight = 960;
                    _graphics.ApplyChanges();
                    currentRes = 2;
                    mapPos.X = mapPos.X * resScale;
                    mapPos.Y = mapPos.Y * resScale;
                    combatSpeed = 3 * resScale;
                    dodgeDistance = 60 * resScale;
                }
                else if (currentRes == 2 && f4KeyWasPressed == false)
                {
                    f4KeyWasPressed = true;
                    resScale = 1;
                    plainsRect = new Rectangle(400 * resScale, 0 * resScale, 300 * resScale, 345 * resScale);
                    snowRect = new Rectangle(100 * resScale, 0 * resScale, 310 * resScale, 245 * resScale);
                    desertRect = new Rectangle(100 * resScale, 235 * resScale, 300 * resScale, 240 * resScale);
                    swampRect = new Rectangle(400 * resScale, 330 * resScale, 300 * resScale, 300 * resScale);
                    ClearScenery();
                    SpawnScenery();
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 480;
                    _graphics.ApplyChanges();
                    currentRes = 1;
                    mapPos.X = mapPos.X / 2;
                    mapPos.Y = mapPos.Y / 2;
                    combatSpeed = 3 * resScale;
                    dodgeDistance = 60 * resScale;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F4) && f4KeyWasPressed == true)
            {
                f4KeyWasPressed = false;
            }

            if(isInMainMenu == true)
            {
                if (singleplayerButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    ClearScenery();
                    SpawnScenery();
                    mapPos = new Vector2(390 * resScale, 225 * resScale);
                    isInMainMenu = false;
                    gameHasStarted = true;
                    inWorldMap = true;
                    // Put some cutscene shit here
                    inCoop = false;
                    inSingleplayer = true;
                    timeSinceLastEncounter = gameTime.TotalGameTime.TotalMilliseconds;
                    healthMultiplier = 1;
                    redguyHealth = 3;
                }
                else if (coopButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    ClearScenery();
                    SpawnScenery();
                    mapPos = new Vector2(390 * resScale, 225 * resScale);
                    isInMainMenu = false;
                    gameHasStarted = true;
                    inWorldMap = true;
                    // Put some cutscene shit here
                    inSingleplayer = false;
                    inCoop = true;
                    timeSinceLastEncounter = gameTime.TotalGameTime.TotalMilliseconds;
                    healthMultiplier = 2;
                    redguyHealth = 3;
                    blueguyHealth = 3;
                }
                else if (settingsButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    Debug.WriteLine("settings");
                }
                else if (exitButtonRect.Intersects(mouseRect) && clicked)
                {
                    Exit();
                }
            }
            if (gameHasStarted)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && escapeKeyWasPressed == false)
                {
                    escapeKeyWasPressed = true;
                    BackToMenu();
                } else if (Keyboard.GetState().IsKeyUp(Keys.Escape) && escapeKeyWasPressed == true)
                {
                    escapeKeyWasPressed = false;
                }
                if (inSingleplayer)
                {
                    if (inWorldMap)
                    {
                        SPPlayerMapMove(gameTime);
                        SinglePlayerEncounter(gameTime);
                        if (plainsRect.Contains(redguyMapRect))
                        {
                            isInPlains = true;
                        }
                        else
                        {
                            isInPlains = false;
                        }
                        if (snowRect.Contains(redguyMapRect))
                        {
                            isInSnow = true;
                        }
                        else
                        {
                            isInSnow = false;
                        }
                        if (desertRect.Contains(redguyMapRect))
                        {
                            isInDesert = true;
                        }
                        else
                        {
                            isInDesert = false;
                        }
                        if (swampRect.Contains(redguyMapRect))
                        {
                            isInSwamp = true;
                        }
                        else
                        {
                            isInSwamp = false;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && enterKeyWasPressed == false)
                        {
                            enterKeyWasPressed = true;
                            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(mousePos.X - 25 * resScale), (int)(mousePos.Y - 25 * resScale)), resScale));
                        }
                        if (Keyboard.GetState().IsKeyUp(Keys.Enter) && enterKeyWasPressed == true)
                        {
                            enterKeyWasPressed = false;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {
                            ClearScenery();
                            SpawnScenery();
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.L))
                        {
                            ForceEncounter(gameTime);
                        }
                    }
                    if (inCombat)
                    {
                        if (redguyHealth <= 0)
                        {
                            //put game over here
                            BackToMenu();
                        }
                        SPCombatMove(gameTime);
                        // Smileys
                        if (enemyToFight == 1)
                        {
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime);
                                if (redguyPos.Y + (20 * resScale) == smileys[i].position.Y && redguyPos.X < smileys[i].position.X)
                                {
                                    smileys[i].Charge();
                                }
                                if (redguyHead.Intersects(smileys[i].smileyRect) || redguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                    {
                                        redguyHealth--;
                                        redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        Debug.WriteLine("Hit!");
                                        Debug.WriteLine("Health: " + redguyHealth);
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health--;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                                if (smileys.Count <= 0)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Smiley Chieftain
                        if (enemyToFight == 2)
                        {
                            for (int i = 0; i < smileyCheiftain.Count; i++)
                            {
                                smileyCheiftain[i].EnemyAction(gameTime);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileyCheiftain[i].rectangle))
                                    {
                                        bullets.RemoveAt(b);
                                        smileyCheiftain[i].health--;
                                    }
                                }
                                if (smileyCheiftain[i].health <= 0)
                                {
                                    smileyCheiftain.RemoveAt(i);
                                }
                                else
                                {
                                    for (int s = 0; s < smileys.Count; s++)
                                    {
                                        smileys[s].speed = 4;
                                    }
                                }
                                if (smileyCheiftain.Count <= 0)
                                {
                                    EndCombat(gameTime);
                                }
                                if (smileyCheiftain.Count > 0)
                                {
                                    if (smileyCheiftain[i].isHyping)
                                    {
                                        for (int s = 0; s < smileys.Count; s++)
                                        {
                                            smileys[s].speed = 8;
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime);
                                if (redguyPos.Y + (20 * resScale) == smileys[i].position.Y && redguyPos.X < smileys[i].position.X)
                                {
                                    smileys[i].Charge();
                                }
                                if (redguyHead.Intersects(smileys[i].smileyRect) || redguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                    {
                                        redguyHealth--;
                                        redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        Debug.WriteLine("Hit!");
                                        Debug.WriteLine("Health: " + redguyHealth);
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health--;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                            }
                            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 7500 || smileys.Count <= 0)
                            {
                                int extraSmiley = new Random().Next(1, 3);
                                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 240 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                if (extraSmiley == 2)
                                {
                                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 340 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                }
                                timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        void SPPlayerMapMove(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            if (!isClicking)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    targetPos.X = mousePos.X - 7 * resScale;
                    targetPos.Y = mousePos.Y - 13 * resScale;

                    isMapMoving = true;
                }
            }
            
            if (isMapMoving)
            {
                distanceToTravelX = targetPos.X - mapPos.X;
                distanceToTravelY = targetPos.Y - mapPos.Y;
                distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                movementX = distanceToTravelX / distanceToTravelTotal;
                movementY = distanceToTravelY / distanceToTravelTotal;

                for (int i = 0; i < mountains.Count; i++)
                {
                    if (redguyMapRect.Intersects(mountains[i].mountainRect))
                    {
                        mapPos = lastKnownPos;
                        isMapMoving = false;
                    }
                }
                if (redguyMapRect.Intersects(gateRect))
                {
                    mapPos = lastKnownPos;
                    isMapMoving = false;
                }
                lastKnownPos = mapPos;
                mapPos.X += movementX * ((float)mapSpeed * resScale);
                mapPos.Y += movementY * ((float)mapSpeed * resScale);
                if (distanceToTravelTotal < 1)
                {
                    isMapMoving = false;
                }
            }
        }

        void SPCombatMove(GameTime gameTime)
        {
            if (redguyHealth >= 1)
            if (!redIsDodging)
            {
                if (Keyboard.GetState().IsKeyDown(redguyMoveUp) && redguyPos.Y >= 180 * resScale)
                {
                    redguyPos.Y -= combatSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(redguyMoveDown) && redguyPos.Y <= 390 * resScale)
                {
                    redguyPos.Y += combatSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(redguyMoveLeft) && redguyPos.X >= 1 * resScale)
                {
                    redguyPos.X -= combatSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(redguyMoveRight) && redguyPos.X <= 340 * resScale)
                {
                    redguyPos.X += combatSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(redguyShoot))
                {
                    if (redfireDelay == false)
                    {
                        bullets.Add(new Bullet(bulletSprite, redguyPos + new Vector2(50 * resScale, 40 * resScale), new Vector2(7 * resScale, 0), resScale));
                        redfireDelay = true;
                    }
                }
                if (Keyboard.GetState().IsKeyUp(redguyShoot))
                {
                    redfireDelay = false;
                }

                // Red dodging
                if (gameTime.TotalGameTime.TotalMilliseconds > redtimeSinceLastDodge + redDodgeDelay)
                {
                    if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveDown) && redguyPos.Y <= 345 * resScale)
                    {
                        redguyPos.Y += dodgeDistance;
                        RedDodge(gameTime);
                    }
                    if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveUp) && redguyPos.Y >= 250 * resScale)
                    {
                        redguyPos.Y -= dodgeDistance;
                        RedDodge(gameTime);
                    }
                    if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveRight) && redguyPos.X <= 245 * resScale)
                    {
                        redguyPos.X += dodgeDistance;
                        RedDodge(gameTime);
                    }
                    if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveLeft) && redguyPos.X >= 70 * resScale)
                    {
                        redguyPos.X -= dodgeDistance;
                        RedDodge(gameTime);
                    }
                }
            }
        }

        void RedDodge(GameTime gameTime)
        {
            redtimeSinceLastDodge = gameTime.TotalGameTime.TotalMilliseconds;
            redInvulnTimerD = gameTime.TotalGameTime.TotalMilliseconds;
            redIsDodging = true;
            redIsDodging = false;
        }

        void SinglePlayerEncounter(GameTime gameTime)
        {
            if (isMapMoving)
            {
                if (isInPlains)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 1500)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int numberTo5 = new Random().Next(1, 6);
                            if (numberTo5 == 1)
                            {
                                PlainsEncounter(gameTime);
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            else
                            {
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                } else if (isInSnow)
                {
                    
                } else if (isInDesert)
                {
                    
                } else if (isInSwamp)
                {

                }
            }
        }

        void PlainsEncounter(GameTime gameTime)
        {
            isMapMoving = false;
            redguyPos = new Vector2(162 * resScale, 258 * resScale);
            blueguyPos = new Vector2(60 * resScale, 330 * resScale);
            lastKnownPos = mapPos;
            inWorldMap = false;
            inCombat = true;
            int etf = new Random().Next(1, 11);
            if (etf <= 6)
            {
                enemyToFight = 1;
            } else if (etf >= 7) {
                enemyToFight = 2;
            }
            if (enemyToFight == 1)
            {
                int extraSmiley = new Random().Next(1, 4);
                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 240 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(600 * resScale, 350 * resScale), new Vector2(550 * resScale, 350 * resScale), resScale, 5 * healthMultiplier, 4));
                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(550 * resScale, 290 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                if (extraSmiley == 1)
                {
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 420 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                } else if (extraSmiley == 2)
                {
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 420 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(510 * resScale, 310 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                }
                for (int i = 0; i < smileys.Count; i++)
                {
                    smileys[i].moveStart = gameTime.TotalGameTime.TotalMilliseconds;
                }
            } else if (enemyToFight == 2)
            {
                smileyCheiftain.Add(new SmileyChieftain(smileyChieftanSprite, new Vector2(600 * resScale, 240 * resScale), resScale, 25 * healthMultiplier));
                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 420 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                smileys.Add(new Smiley(smileyEnemySprite, new Vector2(510 * resScale, 310 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                int extraSmiley = new Random().Next(1, 3);
                if (extraSmiley == 2)
                {
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 200 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                }
                for (int i = 0; i < smileys.Count; i++)
                {
                    smileys[i].moveStart = gameTime.TotalGameTime.TotalMilliseconds;
                }
                for (int i = 0; i < smileyCheiftain.Count; i++)
                {
                     smileyCheiftain[i].moveStart = gameTime.TotalGameTime.TotalMilliseconds;
                }
                timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        void EndCombat(GameTime gameTime)
        {
            bullets.Clear();
            timeSinceLastEncounter = gameTime.TotalGameTime.TotalMilliseconds;
            enemyToFight = 0;
            inCombat = false;
            inWorldMap = true;
            mapPos = lastKnownPos;
            redguyHealth = 3;
            blueguyHealth = 3;
            ClearScenery();
            SpawnScenery();
            ClearEnemies();
        }

        void ForceEncounter(GameTime gameTime)
        {
            if (isInPlains)
            {
                PlainsEncounter(gameTime);
            }
            else if (isInSnow)
            {

            }
            else if (isInDesert)
            {

            }
            else if (isInSwamp)
            {

            }
        }

        void BackToMenu()
        {
            int windowTitleThing = new Random().Next(1, 19);
            switch (windowTitleThing)
            {
                case 1:
                    Window.Title = "The greatest sequel ever";
                    break;
                case 2:
                    Window.Title = "2playershootergame best game 1997";
                    break;
                case 3:
                    Window.Title = "GLOCKENSPIEL!!!";
                    break;
                case 4:
                    Window.Title = "holy moly now THIS is gaming";
                    break;
                case 5:
                    Window.Title = "you know what you have to do.";
                    break;
                case 6:
                    Window.Title = "in what universe?";
                    break;
                case 7:
                    Window.Title = "video games!";
                    break;
                case 8:
                    Window.Title = "amazing";
                    break;
                case 9:
                    Window.Title = "title length";
                    break;
                case 10:
                    Window.Title = "minecraft.exe";
                    break;
                case 11:
                    Window.Title = "It looks like spain";
                    break;
                case 12:
                    Window.Title = "KILL THE IMPOSTOR!!!!!";
                    break;
                case 13:
                    Window.Title = "shoutout stack overflow for free code";
                    break;
                case 14:
                    Window.Title = "woooo, monogame!";
                    break;
                case 15:
                    Window.Title = "only the funniest";
                    break;
                case 16:
                    Window.Title = "minecraft.exe";
                    break;
                case 17:
                    Window.Title = "huh, it really DOES look like spain.";
                    break;
                case 18:
                    Window.Title = "greens-kun";
                    break;
                default:
                    Window.Title = "sample text";
                    break;
            }
            isInMainMenu = true;
            gameHasStarted = false;
            inWorldMap = false;
            inCombat = false;
            inCoop = false;
            inSingleplayer = false;
            isMapMoving = false;
            ClearScenery();
            ClearEnemies();
            mapPos = new Vector2(290 * resScale, 225 * resScale);
            lastKnownPos = new Vector2(290 * resScale, 225 * resScale);
        }

        // That scenery....
        void SpawnScenery()
        {
            // Mountains
            // Top
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(295 * resScale), (int)(7 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(305 * resScale), (int)(28 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(300 * resScale), (int)(46 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(312 * resScale), (int)(68 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(325 * resScale), (int)(101 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(340 * resScale), (int)(133 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(322 * resScale), (int)(143 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(302 * resScale), (int)(158 * resScale)), resScale));
            // 2nd Top
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(212 * resScale), (int)(188 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(179 * resScale), (int)(192 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(191 * resScale), (int)(196 * resScale)), resScale));
            // Bottom
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(571 * resScale), (int)(282 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(516 * resScale), (int)(289 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(546 * resScale), (int)(290 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(468 * resScale), (int)(290 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(493 * resScale), (int)(300 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(418 * resScale), (int)(296 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(443 * resScale), (int)(304 * resScale)), resScale));
        }

        void ClearScenery()
        {
            mountains.Clear();
        }

        void ClearEnemies()
        {
            smileys.Clear();
            smileyCheiftain.Clear();
            bullets.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp
               );

            mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);
            Rectangle mainmenuRect = new Rectangle(0, 0, mainMenuSprite.Width * resScale, mainMenuSprite.Height * resScale);
            singleplayerButtonRect = new Rectangle(290 * resScale, 200 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            coopButtonRect = new Rectangle(290 * resScale, 280 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            settingsButtonRect = new Rectangle(5 * resScale, 400 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            exitButtonRect = new Rectangle(595 * resScale, 400 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);

            //Main Menu Drawing
            if (isInMainMenu)
            {
                _spriteBatch.Draw(mainMenuSprite, mainmenuRect, Color.White);
                _spriteBatch.Draw(singleplayerButton, singleplayerButtonRect, Color.White);
                _spriteBatch.Draw(coopButton, coopButtonRect, Color.White);
                _spriteBatch.Draw(settingsButton, settingsButtonRect, Color.White);
                _spriteBatch.Draw(exitButton, exitButtonRect, Color.White);
                _spriteBatch.Draw(oneButton, mouseRect, Color.Transparent);
            }            
            if (gameHasStarted)
            {
                if (inWorldMap)
                {
                    Rectangle mapRect = new Rectangle(0 * resScale, 0 * resScale, mapSprite.Width / 2 * resScale, mapSprite.Height / 2 * resScale);
                    if (inSingleplayer)
                    {
                        redguyMapRect = new Rectangle((int)mapPos.X, (int)mapPos.Y, redguySprite.Width * resScale, redguySprite.Height * resScale);
                    }
                    if (inCoop)
                    {
                        coopMapRect = new Rectangle((int)mapPos.X, (int)mapPos.Y, coopMapSprite.Width * resScale, coopMapSprite.Height * resScale);
                    }
                    // Map sprite here
                    _spriteBatch.Draw(mapSprite, mapRect, Color.White);
                    // Mountains (first layer)
                    for (int i = 0; i < 8; i++)
                    {
                        mountains[i].Draw(_spriteBatch);
                    }
                    // Gate
                    gateRect = new Rectangle(247 * resScale, 174 * resScale, gateSprite.Width * resScale, gateSprite.Height * resScale);
                    _spriteBatch.Draw(gateSprite, gateRect, Color.White);
                    // Mountains (second layer)
                    for (int i = 8; i < mountains.Count; i++)
                    {
                        mountains[i].Draw(_spriteBatch);
                    }
                    // Buildings / Scenery
                    if (inSingleplayer)
                    {
                        if (mousePos.X < mapPos.X + 7 * resScale)
                        {
                            _spriteBatch.Draw(redguyFlipped, redguyMapRect, Color.White);
                        }
                        else
                        {
                            _spriteBatch.Draw(redguySprite, redguyMapRect, Color.White);
                        }
                    }
                    if (inCoop)
                    {
                        if (mousePos.X < mapPos.X + 7 * resScale)
                        {
                            _spriteBatch.Draw(coopGuysFlipped, coopMapRect, Color.White);
                        }
                        else
                        {
                            _spriteBatch.Draw(coopMapSprite, coopMapRect, Color.White);
                        }
                    }
                }
                if (inCombat)
                {
                    // Drawing maps
                    Rectangle plainsMapRect = new Rectangle(0, 0, plainsBackgroundSprite.Width * resScale, plainsBackgroundSprite.Height * resScale);
                    if (isInPlains)
                    {
                        _spriteBatch.Draw(plainsBackgroundSprite, plainsMapRect, Color.White);
                    }
                    if (inSingleplayer)
                    {
                        //Declaring Rectangles and whatnot
                        redguyRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguySprite.Width * (3 * resScale), redguySprite.Height * (3 * resScale));
                        redguyHead = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 2 * resScale, redguySprite.Width * (3 * resScale) - 5 * resScale, redguySprite.Height * (3 * resScale) - 50 * resScale);
                        redguyBody = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 30 * resScale, redguySprite.Width * (3 * resScale) - 10 * resScale, redguySprite.Height * (3 * resScale) - 35 * resScale);
                        Rectangle redguyDodgeRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguyDodgeSprite.Width * (3 * resScale), redguyDodgeSprite.Height * (3 * resScale));

                        //Background drawing

                        // Player
                        if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD)
                        {
                            if (redguyHealth >= 3)
                            {
                                _spriteBatch.Draw(redguySprite, redguyRect, Color.White);
                            } 
                            else if (redguyHealth == 2)
                            {
                                _spriteBatch.Draw(redguyHurt1, redguyRect, Color.White);
                            }
                            else if (redguyHealth <= 1)
                            {
                                _spriteBatch.Draw(redguyHurt2, redguyRect, Color.White);
                            }
                        }
                        else
                        {
                            if (redguyHealth >= 3)
                            {
                                _spriteBatch.Draw(redguyDodgeSprite, redguyDodgeRect, Color.White);
                            }
                            else if (redguyHealth == 2)
                            {
                                _spriteBatch.Draw(redguyHurt1Dodge, redguyDodgeRect, Color.White);
                            }
                            else if (redguyHealth <= 1)
                            {
                                _spriteBatch.Draw(redguyHurt2Dodge, redguyDodgeRect, Color.White);
                            }
                        }
                        //_spriteBatch.Draw(whiteSquareSprite, redguyHead, Color.Red);
                        //_spriteBatch.Draw(whiteSquareSprite, redguyBody, Color.Green);

                        // Enemies
                        for (int i = 0; i < smileyCheiftain.Count; i++)
                        {
                            smileyCheiftain[i].Draw(_spriteBatch);
                        }
                        for (int i = 0; i < smileys.Count; i++)
                        {
                            smileys[i].Draw(_spriteBatch);
                        }
                        for (int i = 0; i < smileyCheiftain.Count; i++)
                        {
                            if (smileyCheiftain[i].isHyping)
                            {
                                for (int s = 0; s < smileys.Count; s++)
                                {
                                    _spriteBatch.Draw(smileyEnemySprite, smileys[s].smileyRect, Color.Red);
                                }
                            }
                        }
                    }
                    // Bullets
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Draw(_spriteBatch);
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
