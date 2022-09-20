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

        bool narb;

        int currentRes = 1;
        public int resScale = 1;
        bool escapeKeyWasPressed = false;
        bool f4KeyWasPressed = false;
        bool enterKeyWasPressed = false;
        bool isClicking = false;
        Point mousePos;
        bool clicked;
        bool debugmode;
        bool showBoundaries;
        bool drawhitboxes;
        bool ikeypressed = false;

        //Declaring Backgrounds
        Texture2D mainMenuSprite;
        Texture2D controlsScreen;
        Texture2D mapSprite;
        Texture2D plainsBackgroundSprite;
        Texture2D desertBackgroundSprite;
        Texture2D snowBackgroundSprite;
        Texture2D swampBackgroundSprite;
        Texture2D shopBackgroundSprite;
        Texture2D shopCounterSprite;
        Texture2D encounterBonusText;
        Texture2D youWonText;
        Texture2D narbBackground;
        Texture2D winScreen;
        Texture2D tutorialWorldMap;
        Texture2D tutorialCombatMap;
        
        //Declaring Buttons
        Texture2D oneButton;
        Texture2D twoButton;
        Texture2D coopButton;
        Texture2D singleplayerButton;
        Texture2D exitButton;
        Texture2D settingsButton;
        Texture2D tutorialButton;

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
        Texture2D blueguyHurt1Dodge;
        Texture2D blueguyHurt2Dodge;

        //Declaring Enemies
        Texture2D smileyEnemySprite;
        Texture2D smileyChieftanSprite;
        Texture2D statueEnemySprite;
        Texture2D chargeUpSprite;
        Texture2D beamSprite;
        Texture2D yetiSprite;
        Texture2D rockSprite;
        Texture2D frogSprite;
        Texture2D frogAttackingSprite;
        Texture2D frogTongueSprite;
        Texture2D smileyWallSprite;

        // Declaring Scenery
        Texture2D cloudSprite;
        Texture2D mountainSprite;
        Texture2D gateSprite;
        Texture2D storeSprite;
        Texture2D treeSprite;
        Texture2D snowflakeSprite;
        Texture2D swampTreeSprite;

        // Declaring misc
        Texture2D bulletSprite;
        Texture2D whiteSquareSprite;
        Texture2D coinSprite;
        Texture2D shopkeeperSprite;
        Texture2D debugIndicator;
        Texture2D damageHatSprite;
        Texture2D speedHatSprite;

        //Listing stuff
        List<Bullet> bullets = new List<Bullet>();
        List<Mountain> mountains = new List<Mountain>();
        List<Smiley> smileys = new List<Smiley>();
        List<SmileyChieftain> smileyCheiftain = new List<SmileyChieftain>();
        List<Statue> statues = new List<Statue>();
        List<Rectangle> coinSprites = new List<Rectangle>();
        List<Yeti> yetis = new List<Yeti>();
        List<Frog> frogs = new List<Frog>();
        List<Rectangle> mapBoundaries = new List<Rectangle>();
        List<Tree> trees = new List<Tree>();
        List<Snowflake> snow = new List<Snowflake>();
        List<SmileyBoss> smileyBoss = new List<SmileyBoss>();

        bool inWinScreen = false;

        // Main menu stuff
        bool isInMainMenu = true;

        // In game stuff
        bool gameHasStarted = false;
        bool inWorldMap = false;
        bool inCombat = false;
        bool inTutorial = false;
        public bool inCoop = false;
        public bool inSingleplayer = false;
        Vector2 mapPos;
        Vector2 targetPos;
        bool isMapMoving = false;
        bool canMapMove = true;
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

        double timeSinceLastWon;
        bool addedCoin;
        bool canCombatMove = true;
        bool wonCurrentEncounter = false;

        double timeSinceLastSnow = 0;

        int flashAlpha = 0;
        int timesFlashed;
        bool encounterFlashing;
        bool flashDarken = true;
        bool flashLighten = false;
        double flashLastIncrement;

        // Store stuff
        bool inStore = false;
        Rectangle storeBackgroundRect;
        Rectangle storeCounterRect;

        double timeSinceLastBob;
        bool bobUp = true;

        // Enemies
        int healthMultiplier = 1;
        double timeSinceLastSmileySpawn;
        // Enemy to fight values:
        // 0 = none
        // 1 = smiley
        // 2 = smiley chieftain
        // 3 = statue
        // 4 = yeti
        // 5 = frog
        // 6 = mosquito
        // 7 = desert snake
        // 8 = scorpion
        // 9 = smiley boss
        // 10 = frog boss
        // 11 = statue boss
        int enemyToFight = 0;

        bool hasFought1 = false;
        bool hasFought2 = false;
        bool hasFought3 = false;
        bool hasFought4 = false;
        bool hasFought5 = false;
        bool hasFought6 = false;
        bool hasFought7 = false;
        bool hasFought8 = false;

        // Player stuff
        public int redguyHealth;
        public int blueguyHealth;

        double redInvulnTimerH;
        double blueInvulnTimerH;
        int redInvulnTimeH = 500;
        int blueInvulnTimeH = 500;

        int healthFlashR = 250;
        int healthFlashB = 250;
        double redLastIncrement;
        double blueLastIncrement;

        int playerCoins = 0;
        int coinDistance;

        int playerDamage = 1;

        // Position & walking stuff
        public Vector2 redguyPos = new Vector2(162, 258);
        public Vector2 blueguyPos = new Vector2(60, 330);
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
        double blueInvulnTimerD = 0;

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
        public Rectangle tutorialButtonRect;
        
        public Rectangle gateRect;
        public Rectangle storeRect;

        public Rectangle plainsRect = new Rectangle(400, 0, 300, 345);
        public Rectangle snowRect= new Rectangle(100, 0, 310, 245);
        public Rectangle desertRect = new Rectangle(100, 235, 300, 240);
        public Rectangle swampRect = new Rectangle(400, 330, 300, 300);

        public Rectangle tutorialinteractable;

        public Rectangle smileyBossMapRect;

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
        Keys blueguyShoot = Keys.OemComma;
        Keys blueguyDodge = Keys.OemPeriod;
        Keys interact = Keys.Enter;

        public Hat damageHat;
        public Hat speedHat;

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

            mapPos = new Vector2(390 * resScale, 225 * resScale);
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
            shopBackgroundSprite = Content.Load<Texture2D>("Backgrounds/shopbackground");
            shopCounterSprite = Content.Load<Texture2D>("Backgrounds/shopcounter");
            encounterBonusText = Content.Load<Texture2D>("Backgrounds/EncounterBonusText");
            youWonText = Content.Load<Texture2D>("Backgrounds/YouWonText");
            narbBackground = Content.Load<Texture2D>("Backgrounds/narb");
            winScreen = Content.Load<Texture2D>("Backgrounds/winscreentemp");
            tutorialWorldMap = Content.Load<Texture2D>("Backgrounds/tutorialzone");
            tutorialCombatMap = Content.Load<Texture2D>("Backgrounds/tutorialzone2");

            //Buttons
            oneButton = Content.Load<Texture2D>("Items/1xbutton");
            twoButton = Content.Load<Texture2D>("Items/2xbutton");
            coopButton = Content.Load<Texture2D>("Items/coopbutton");
            exitButton = Content.Load<Texture2D>("Items/exitbutton");
            singleplayerButton = Content.Load<Texture2D>("Items/singleplayerbutton");
            settingsButton = Content.Load<Texture2D>("Items/settingsbutton");
            tutorialButton = Content.Load<Texture2D>("Items/tutorialbutton");

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
            blueguyHurt1Dodge = Content.Load<Texture2D>("Players/blueguyhurt1dodge");
            blueguyHurt2Dodge = Content.Load<Texture2D>("Players/blueguyhurt2dodge");

            //Enemies
            smileyEnemySprite = Content.Load<Texture2D>("Enemies/Smiley");
            smileyChieftanSprite = Content.Load<Texture2D>("Enemies/SmileyChieftain");
            statueEnemySprite = Content.Load<Texture2D>("Enemies/statue");
            chargeUpSprite = Content.Load<Texture2D>("Enemies/energybeamstart");
            beamSprite = Content.Load<Texture2D>("Enemies/energybeam");
            yetiSprite = Content.Load<Texture2D>("Enemies/yeti");
            rockSprite = Content.Load<Texture2D>("Enemies/rock");
            frogSprite = Content.Load<Texture2D>("Enemies/frog");
            frogAttackingSprite = Content.Load<Texture2D>("Enemies/frogattack");
            frogTongueSprite = Content.Load<Texture2D>("Enemies/frogtongue");
            smileyWallSprite = Content.Load<Texture2D>("Enemies/smileywall");

            //Scenery
            mountainSprite = Content.Load<Texture2D>("Scenery/Mountain");
            gateSprite = Content.Load<Texture2D>("Scenery/gate");
            cloudSprite = Content.Load<Texture2D>("Scenery/cloud");
            storeSprite = Content.Load<Texture2D>("Scenery/store");
            treeSprite = Content.Load<Texture2D>("Scenery/Tree");
            snowflakeSprite = Content.Load<Texture2D>("Scenery/snowflake");
            swampTreeSprite = Content.Load<Texture2D>("Scenery/SwampTree");

            //Misc
            bulletSprite = Content.Load<Texture2D>("Items/Bullet");
            whiteSquareSprite = Content.Load<Texture2D>("Items/whitesquare");
            coinSprite = Content.Load<Texture2D>("Items/coin");
            shopkeeperSprite = Content.Load<Texture2D>("Scenery/DSL");
            debugIndicator = Content.Load<Texture2D>("Backgrounds/debug");
            damageHatSprite = Content.Load<Texture2D>("Items/damagehat");
            speedHatSprite = Content.Load<Texture2D>("Items/speedhat");

            // Adding Hats
            damageHat = new Hat(damageHatSprite, new Vector2(0, 0), resScale);
            speedHat = new Hat(speedHatSprite, new Vector2(0, 0), resScale);

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

            if (debugmode)
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    Debug.WriteLine(mouseState.X + "," + mouseState.Y);
                }
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
                    AddCoin2();
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
                    AddCoin2();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F4) && f4KeyWasPressed == true)
            {
                f4KeyWasPressed = false;
            }

            if (isInMainMenu == true)
            {
                IsMouseVisible = true;
                if (singleplayerButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    ClearScenery();
                    SpawnScenery();
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
                else if (tutorialButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    mapPos.X = 240 * resScale;
                    mapPos.Y = 200 * resScale;
                    lastKnownPos.X = 240 * resScale;
                    lastKnownPos.Y = 200 * resScale;
                    isClicking = true;
                    inTutorial = true;
                    isInMainMenu = false;
                    gameHasStarted = false;
                    inWorldMap = true;
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
                if (Keyboard.GetState().IsKeyDown(Keys.F12))
                {
                    debugmode = true;
                }
            }
            if (gameHasStarted)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && escapeKeyWasPressed == false && !inCombat && !inStore && !encounterFlashing)
                {
                    escapeKeyWasPressed = true;
                    BackToMenu();
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Escape) && escapeKeyWasPressed == true && !inCombat && !inStore && !encounterFlashing)
                {
                    escapeKeyWasPressed = false;
                }
                if (isInSnow && !wonCurrentEncounter)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSnow + 10)
                    {
                        snow.Add(new Snowflake(snowflakeSprite, RandomSnowPos(), new Random().Next(2, 7), new Random().Next(2, 7), resScale, new Color(Color.White, 255)));
                        timeSinceLastSnow = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
                if (isInSwamp && !wonCurrentEncounter)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSnow + 10)
                    {
                        snow.Add(new Snowflake(snowflakeSprite, RandomRainPos(), 0f, new Random().Next(5, 9), resScale, new Color(Color.Blue, 225)));
                        timeSinceLastSnow = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
                for (int i = 0; i < snow.Count; i++)
                {
                    snow[i].Snowfall(gameTime);
                    if (snow[i].position.X < 0 - snow[i].texture.Width)
                    {
                        snow.RemoveAt(i);
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && encounterFlashing)
                {
                    timesFlashed = 7;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) && wonCurrentEncounter)
                {
                    timeSinceLastWon -= 7000;
                    escapeKeyWasPressed = true;
                }
                // SINGLEPLAYER START
                if (inSingleplayer)
                {
                    mapSpeed = 0.85f;
                    // SINGLEPLAYER WORLD MAP
                    if (inWorldMap)
                    {
                        if (!encounterFlashing)
                        {
                            IsMouseVisible = true;
                        }
                        SPPlayerMapMove(gameTime);
                        if (!debugmode)
                        {
                            SinglePlayerEncounter(gameTime);
                        }
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

                        if (redguyMapRect.Intersects(storeRect) && Keyboard.GetState().IsKeyDown(interact))
                        {
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                            inWorldMap = false;
                            inStore = true;
                        }

                        if (redguyMapRect.Intersects(smileyBossMapRect) && Keyboard.GetState().IsKeyDown(interact) && enterKeyWasPressed == false && !encounterFlashing)
                        {
                            enemyToFight = 9;
                            encounterFlashing = true;
                            isMapMoving = false;
                            smileyBoss.Add(new SmileyBoss(smileyEnemySprite, smileyWallSprite, new Vector2(525 * resScale, 200 * resScale), resScale, 50 * healthMultiplier));
                            timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                            if (!encounterFlashing)
                            {
                                inWorldMap = false;
                                inCombat = true;
                            }
                        }
                        if (enterKeyWasPressed == true && Keyboard.GetState().IsKeyUp(interact))
                        {
                            enterKeyWasPressed = false;
                        }

                        if (debugmode)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.K))
                            {
                                mountains.Add(new Mountain(mountainSprite, new Vector2((int)(mousePos.X), (int)(mousePos.Y)), resScale));
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.O))
                            {
                                trees.Add(new Tree(treeSprite, new Vector2((int)(mousePos.X), (int)(mousePos.Y)), resScale));
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
                            if (Keyboard.GetState().IsKeyDown(Keys.I) && ikeypressed == false)
                            {
                                ikeypressed = true;
                                if (!showBoundaries)
                                {
                                    showBoundaries = true;
                                } else
                                {
                                    showBoundaries = false;
                                }
                            }
                            if (Keyboard.GetState().IsKeyUp(Keys.I) && ikeypressed == true)
                            {
                                ikeypressed = false;
                            }
                        }
                    }
                    // SINGLEPLAYER COMBAT
                    if (inCombat)
                    {
                        if (!debugmode)
                        {
                            IsMouseVisible = false;
                        }
                        if (debugmode)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.I) && ikeypressed == false)
                            {
                                ikeypressed = true;
                                if (!drawhitboxes)
                                {
                                    drawhitboxes = true;
                                }
                                else
                                {
                                    drawhitboxes = false;
                                }
                            }
                            if (Keyboard.GetState().IsKeyUp(Keys.I) && ikeypressed == true)
                            {
                                ikeypressed = false;
                            }
                        }
                        if (damageHat.isEquipped)
                        {
                            playerDamage = 2;
                            combatSpeed = 3 * resScale;
                        }
                        else if (speedHat.isEquipped)
                        {
                            playerDamage = 1;
                            combatSpeed = 5 * resScale;
                        } else
                        {
                            playerDamage = 1;
                            combatSpeed = 3 * resScale;
                        }
                        if (redguyHealth <= 0)
                        {
                            GameOver(gameTime);
                        }
                        SPCombatMove(gameTime);
                        // Smileys
                        if (enemyToFight == 1)
                        {
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
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
                                        healthFlashR = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                                if (smileys.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                            }
                            if (smileys.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
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
                                        smileyCheiftain[i].health -= playerDamage;
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
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
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
                            if (smileyCheiftain.Count <= 0)
                            {
                                ClearEnemies();
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
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
                                        healthFlashR = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                    timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                            }
                            if (smileyCheiftain.Count > 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 7500 || smileys.Count <= 0 && gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 1000)
                                {
                                    int extraSmiley = new Random().Next(1, 3);
                                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 290 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                    if (extraSmiley == 2)
                                    {
                                        smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 340 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                    }
                                    timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                            }
                        }
                        // Statue
                        if (enemyToFight == 3)
                        {
                            for (int i = 0; i < statues.Count; i++)
                            {
                                statues[i].EnemyAction(gameTime);
                                if (statues[i].position.Y + 65 * resScale >= redguyPos.Y + 10 * resScale && statues[i].position.Y + 65 * resScale <= redguyPos.Y + 20 * resScale)
                                {
                                    if (!statues[i].isAttacking)
                                    {
                                        statues[i].Attack(gameTime);
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(statues[i].rectangle))
                                    {
                                        bullets.RemoveAt(b);
                                        statues[i].health -= playerDamage;
                                    }
                                }
                                if (statues[i].health <= 0)
                                {
                                    statues.RemoveAt(i);
                                }
                                if (statues.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (statues.Count > 0)
                                {
                                    if (statues[i].isAttacking)
                                    {
                                        if (redguyHead.Intersects(statues[i].beamRect) && statues[i].canDoDamage || redguyBody.Intersects(statues[i].beamRect) && statues[i].canDoDamage)
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD)
                                            {
                                                redguyHealth--;
                                            }
                                        }
                                    }
                                }
                            }
                            if (statues.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Yeti
                        if (enemyToFight == 4)
                        {
                            for (int i = 0; i < yetis.Count; i++)
                            {
                                yetis[i].EnemyAction(gameTime);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(yetis[i].hitbox))
                                    {
                                        bullets.RemoveAt(b);
                                        yetis[i].health -= playerDamage;
                                    }
                                }
                                if (yetis[i].health <= 0)
                                {
                                    yetis.RemoveAt(i);
                                }
                                if (yetis.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (yetis.Count > 0)
                                {
                                    if (redguyHead.Intersects(yetis[i].rockRect) || redguyBody.Intersects(yetis[i].rockRect))
                                    {
                                        if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                        {
                                            redguyHealth--;
                                            redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                            healthFlashR = 250;
                                        }
                                    }
                                }
                            }
                            if (yetis.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Frogs
                        if (enemyToFight == 5)
                        {
                            for (int i = 0; i < frogs.Count; i++)
                            {
                                frogs[i].EnemyAction(gameTime);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(frogs[i].hitbox))
                                    {
                                        bullets.RemoveAt(b);
                                        frogs[i].health -= playerDamage;
                                    }
                                }
                                if (frogs[i].health <= 0)
                                {
                                    frogs.RemoveAt(i);
                                }
                                if (frogs.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (frogs.Count > 0)
                                {
                                    try
                                    {
                                        if (redguyHead.Intersects(frogs[i].tongueRect) || redguyBody.Intersects(frogs[i].tongueRect))
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                            {
                                                redguyHealth--;
                                                redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                                healthFlashR = 250;
                                            }
                                        }
                                    } catch
                                    {
                                        return;
                                    }
                                }
                            }
                            if (frogs.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Smiley Boss
                        if (enemyToFight == 9)
                        {
                            for (int i = 0; i < smileyBoss.Count; i++)
                            {
                                smileyBoss[i].EnemyAction(gameTime, _graphics);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileyBoss[i].hitBox))
                                    {
                                        bullets.RemoveAt(b);
                                        smileyBoss[i].health -= playerDamage;
                                    }
                                }
                                if (smileyBoss[i].health <= 0)
                                {
                                    smileyBoss.RemoveAt(i);
                                }
                                if (smileyBoss.Count > 0)
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 1500)
                                    {
                                        smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 290 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                        timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                    }
                                }
                            }
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
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
                                        healthFlashR = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                    timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                            }
                            if (smileyBoss.Count <= 0)
                            {
                                WinGame();
                            }
                        }
                    }

                    if (inStore && Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        escapeKeyWasPressed = true;
                        inStore = false;
                        inWorldMap = true;
                    }

                    if (inStore)
                    {
                        if (mouseRect.Intersects(damageHat.hatRect))
                        {
                            if (clicked)
                            {
                                if (!damageHat.isPurchased)
                                {
                                    if (playerCoins >= 2)
                                    {
                                        playerCoins = playerCoins - 2;
                                        damageHat.isPurchased = true;
                                        speedHat.isEquipped = false;
                                        damageHat.isEquipped = true;
                                        AddCoin2();
                                    }
                                }
                                else
                                {
                                    speedHat.isEquipped = false;
                                    damageHat.isEquipped = true;
                                }
                            }
                        }
                        else if (mouseRect.Intersects(speedHat.hatRect))
                        {
                            if (clicked)
                            {
                                if (!speedHat.isPurchased)
                                {
                                    if (playerCoins >= 2)
                                    {
                                        playerCoins = playerCoins - 2;
                                        speedHat.isPurchased = true;
                                        damageHat.isEquipped = false;
                                        speedHat.isEquipped = true;
                                        AddCoin2();
                                    }
                                }
                                else
                                {
                                    damageHat.isEquipped = false;
                                    speedHat.isEquipped = true;
                                }
                            }
                        }
                    }
                }
                // MULTIPLAYER BEGIN
                if (inCoop)
                {
                    mapSpeed = 0.85f;
                    // MULTIPLAYER WORLD MAP
                    if (inWorldMap)
                    {
                        if (!encounterFlashing)
                        {
                            IsMouseVisible = true;
                        }
                        MPPlayerMapMove(gameTime);
                        if (!debugmode)
                        {
                            CoopEncounter(gameTime);
                        }
                        if (plainsRect.Contains(coopMapRect))
                        {
                            isInPlains = true;
                        }
                        else
                        {
                            isInPlains = false;
                        }
                        if (snowRect.Contains(coopMapRect))
                        {
                            isInSnow = true;
                        }
                        else
                        {
                            isInSnow = false;
                        }
                        if (desertRect.Contains(coopMapRect))
                        {
                            isInDesert = true;
                        }
                        else
                        {
                            isInDesert = false;
                        }
                        if (swampRect.Contains(coopMapRect))
                        {
                            isInSwamp = true;
                        }
                        else
                        {
                            isInSwamp = false;
                        }

                        if (coopMapRect.Intersects(storeRect) && Keyboard.GetState().IsKeyDown(interact))
                        {
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                            inWorldMap = false;
                            inStore = true;
                        }

                        if (coopMapRect.Intersects(smileyBossMapRect) && Keyboard.GetState().IsKeyDown(interact) && enterKeyWasPressed == false && !encounterFlashing)
                        {
                            enemyToFight = 9;
                            encounterFlashing = true;
                            isMapMoving = false;
                            smileyBoss.Add(new SmileyBoss(smileyEnemySprite, smileyWallSprite, new Vector2(500 * resScale, 200 * resScale), resScale, 50 * healthMultiplier));
                            timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                            if (!encounterFlashing)
                            {
                                inWorldMap = false;
                                inCombat = true;
                            }
                        }

                        if (debugmode)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.K))
                            {
                                mountains.Add(new Mountain(mountainSprite, new Vector2((int)(mousePos.X), (int)(mousePos.Y)), resScale));
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.O))
                            {
                                trees.Add(new Tree(treeSprite, new Vector2((int)(mousePos.X), (int)(mousePos.Y)), resScale));
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
                            if (Keyboard.GetState().IsKeyDown(Keys.I) && ikeypressed == false)
                            {
                                ikeypressed = true;
                                if (!showBoundaries)
                                {
                                    showBoundaries = true;
                                }
                                else
                                {
                                    showBoundaries = false;
                                }
                            }
                            if (Keyboard.GetState().IsKeyUp(Keys.I) && ikeypressed == true)
                            {
                                ikeypressed = false;
                            }
                        }
                    }
                    // MULTIPLAYER COMBAT
                    if (inCombat)
                    {
                        if (!debugmode)
                        {
                            IsMouseVisible = false;
                        }
                        if (debugmode)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.I) && ikeypressed == false)
                            {
                                ikeypressed = true;
                                if (!drawhitboxes)
                                {
                                    drawhitboxes = true;
                                }
                                else
                                {
                                    drawhitboxes = false;
                                }
                            }
                            if (Keyboard.GetState().IsKeyUp(Keys.I) && ikeypressed == true)
                            {
                                ikeypressed = false;
                            }
                        }
                        if (redguyHealth <= 0 && blueguyHealth <= 0)
                        {
                            GameOver(gameTime);
                        }
                        MPCombatMove(gameTime);
                        // Smileys
                        if (enemyToFight == 1)
                        {
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
                                if (redguyPos.Y + (20 * resScale) == smileys[i].position.Y && redguyPos.X < smileys[i].position.X && redguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                else if (blueguyPos.Y + (20 * resScale) == smileys[i].position.Y && blueguyPos.X < smileys[i].position.X && blueguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                if (redguyHead.Intersects(smileys[i].smileyRect) || redguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                    {
                                        redguyHealth--;
                                        redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashR = 250;
                                    }
                                }
                                if (blueguyHead.Intersects(smileys[i].smileyRect) || blueguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                    {
                                        blueguyHealth--;
                                        blueInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashB = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                                if (smileys.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                            }
                            if (smileys.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
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
                                        smileyCheiftain[i].health -= playerDamage;
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
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
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
                            if (smileyCheiftain.Count <= 0)
                            {
                                ClearEnemies();
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
                                if (redguyPos.Y + (20 * resScale) == smileys[i].position.Y && redguyPos.X < smileys[i].position.X && redguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                else if (blueguyPos.Y + (20 * resScale) == smileys[i].position.Y && blueguyPos.X < smileys[i].position.X && blueguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                if (redguyHead.Intersects(smileys[i].smileyRect) || redguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                    {
                                        redguyHealth--;
                                        redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashR = 250;
                                    }
                                }
                                if (blueguyHead.Intersects(smileys[i].smileyRect) || blueguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                    {
                                        blueguyHealth--;
                                        blueInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashB = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                            }
                            if (smileyCheiftain.Count > 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 7500 || smileys.Count <= 0 && gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 1000)
                                {
                                    int extraSmiley = new Random().Next(1, 3);
                                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 290 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 400 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                    if (extraSmiley == 2)
                                    {
                                        smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 340 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                    }
                                    timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                            }
                        }
                        // Statue
                        if (enemyToFight == 3)
                        {
                            for (int i = 0; i < statues.Count; i++)
                            {
                                statues[i].EnemyAction(gameTime);
                                if (redguyHealth >= 1)
                                {
                                    if (statues[i].position.Y + 65 * resScale >= redguyPos.Y + 10 * resScale && statues[i].position.Y + 65 * resScale <= redguyPos.Y + 20 * resScale)
                                    {
                                        if (!statues[i].isAttacking)
                                        {
                                            statues[i].Attack(gameTime);
                                        }
                                    }
                                }
                                if (blueguyHealth >= 1)
                                {
                                    if (statues[i].position.Y + 65 * resScale >= blueguyPos.Y + 10 * resScale && statues[i].position.Y + 65 * resScale <= blueguyPos.Y + 20 * resScale)
                                    {
                                        if (!statues[i].isAttacking)
                                        {
                                            statues[i].Attack(gameTime);
                                        }
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(statues[i].rectangle))
                                    {
                                        bullets.RemoveAt(b);
                                        statues[i].health -= playerDamage;
                                    }
                                }
                                if (statues[i].health <= 0)
                                {
                                    statues.RemoveAt(i);
                                }
                                if (statues.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (statues.Count > 0)
                                {
                                    if (statues[i].isAttacking)
                                    {
                                        if (redguyHead.Intersects(statues[i].beamRect) && statues[i].canDoDamage || redguyBody.Intersects(statues[i].beamRect) && statues[i].canDoDamage)
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD)
                                            {
                                                redguyHealth--;
                                            }
                                        }
                                        if (blueguyHead.Intersects(statues[i].beamRect) && statues[i].canDoDamage || blueguyBody.Intersects(statues[i].beamRect) && statues[i].canDoDamage)
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                            {
                                                blueguyHealth--;
                                            }
                                        }
                                    }
                                }
                            }
                            if (statues.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Yeti
                        if (enemyToFight == 4)
                        {
                            for (int i = 0; i < yetis.Count; i++)
                            {
                                yetis[i].EnemyAction(gameTime);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(yetis[i].hitbox))
                                    {
                                        bullets.RemoveAt(b);
                                        yetis[i].health -= playerDamage;
                                    }
                                }
                                if (yetis[i].health <= 0)
                                {
                                    yetis.RemoveAt(i);
                                }
                                if (yetis.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (yetis.Count > 0)
                                {
                                    if (redguyHead.Intersects(yetis[i].rockRect) || redguyBody.Intersects(yetis[i].rockRect))
                                    {
                                        if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                        {
                                            redguyHealth--;
                                            redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                            healthFlashR = 250;
                                        }
                                    }
                                    if (blueguyHead.Intersects(yetis[i].rockRect) || blueguyBody.Intersects(yetis[i].rockRect))
                                    {
                                        if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                        {
                                            blueguyHealth--;
                                            blueInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                            healthFlashB = 250;
                                        }
                                    }
                                }
                            }
                            if (yetis.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Frogs
                        if (enemyToFight == 5)
                        {
                            for (int i = 0; i < frogs.Count; i++)
                            {
                                frogs[i].EnemyAction(gameTime);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(frogs[i].hitbox))
                                    {
                                        bullets.RemoveAt(b);
                                        frogs[i].health -= playerDamage;
                                    }
                                }
                                if (frogs[i].health <= 0)
                                {
                                    frogs.RemoveAt(i);
                                }
                                if (frogs.Count <= 0)
                                {
                                    ClearEnemies();
                                    timeSinceLastWon = gameTime.TotalGameTime.TotalMilliseconds;
                                    AddCoin();
                                    canCombatMove = false;
                                    wonCurrentEncounter = true;
                                }
                                if (frogs.Count > 0)
                                {
                                    try
                                    {
                                        if (redguyHead.Intersects(frogs[i].tongueRect) || redguyBody.Intersects(frogs[i].tongueRect))
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                            {
                                                redguyHealth--;
                                                redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                                healthFlashR = 250;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        return;
                                    }
                                    try
                                    {
                                        if (blueguyHead.Intersects(frogs[i].tongueRect) || blueguyBody.Intersects(frogs[i].tongueRect))
                                        {
                                            if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                            {
                                                blueguyHealth--;
                                                blueInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                                healthFlashB = 250;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        return;
                                    }
                                }
                            }
                            if (frogs.Count <= 0)
                            {
                                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && !addedCoin)
                                {
                                    EndCombat(gameTime);
                                }
                                else if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 5000)
                                {
                                    EndCombat(gameTime);
                                }
                            }
                        }
                        // Smiley Boss
                        if (enemyToFight == 9)
                        {
                            for (int i = 0; i < smileyBoss.Count; i++)
                            {
                                smileyBoss[i].EnemyAction(gameTime, _graphics);
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileyBoss[i].hitBox))
                                    {
                                        bullets.RemoveAt(b);
                                        smileyBoss[i].health -= playerDamage;
                                    }
                                }
                                if (smileyBoss[i].health <= 0)
                                {
                                    smileyBoss.RemoveAt(i);
                                }
                                if (smileyBoss.Count > 0)
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastSmileySpawn + 1500)
                                    {
                                        smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 290 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                                        timeSinceLastSmileySpawn = gameTime.TotalGameTime.TotalMilliseconds;
                                    }
                                }
                            }
                            for (int i = 0; i < smileys.Count; i++)
                            {
                                smileys[i].EnemyAction(gameTime, _graphics);
                                if (redguyPos.Y + (20 * resScale) == smileys[i].position.Y && redguyPos.X < smileys[i].position.X && redguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                else if (blueguyPos.Y + (20 * resScale) == smileys[i].position.Y && blueguyPos.X < smileys[i].position.X && blueguyHealth >= 1)
                                {
                                    smileys[i].Charge();
                                }
                                if (redguyHead.Intersects(smileys[i].smileyRect) || redguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerH + redInvulnTimeH)
                                    {
                                        redguyHealth--;
                                        redInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashR = 250;
                                    }
                                }
                                if (blueguyHead.Intersects(smileys[i].smileyRect) || blueguyBody.Intersects(smileys[i].smileyRect))
                                {
                                    if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD && gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerH + blueInvulnTimeH)
                                    {
                                        blueguyHealth--;
                                        blueInvulnTimerH = gameTime.TotalGameTime.TotalMilliseconds;
                                        healthFlashB = 250;
                                    }
                                }
                                for (int b = 0; b < bullets.Count; b++)
                                {
                                    if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                                    {
                                        bullets.RemoveAt(b);
                                        smileys[i].health -= playerDamage;
                                    }
                                }
                                if (smileys[i].health <= 0)
                                {
                                    smileys.RemoveAt(i);
                                }
                            }
                            if (smileyBoss.Count <= 0)
                            {
                                WinGame();
                            }
                        }
                    }

                    if (inStore && Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        escapeKeyWasPressed = true;
                        inStore = false;
                        inWorldMap = true;
                    }
                }
            }
            // TUTORIAL START
            if (inTutorial)
            {
                redguyHealth = 3;
                ClearScenery();
                mapSpeed = 1.25f;
                if (inWorldMap)
                {
                    SPPlayerMapMove(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        BackToMenu();
                    }
                    if (tutorialinteractable.Intersects(redguyMapRect) && Keyboard.GetState().IsKeyDown(interact))
                    {
                        inWorldMap = false;
                        inCombat = true;
                        smileys.Add(new Smiley(smileyEnemySprite, new Vector2(570 * resScale, 240 * resScale), new Vector2(550 * resScale, 240 * resScale), resScale, 5 * healthMultiplier, 4));
                    }
                }
                if (inCombat)
                {
                    SPCombatMove(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        BackToMenu();
                    }
                    for (int i = 0; i < smileys.Count; i++)
                    {
                        smileys[i].EnemyAction(gameTime, _graphics);
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
                                healthFlashR = 250;
                            }
                        }
                        for (int b = 0; b < bullets.Count; b++)
                        {
                            if (bullets[b].bulletRect.Intersects(smileys[i].smileyRect))
                            {
                                bullets.RemoveAt(b);
                                smileys[i].health -= playerDamage;
                            }
                        }
                        if (smileys[i].health <= 0)
                        {
                            smileys.RemoveAt(i);
                        }
                        if (smileys.Count <= 0)
                        {
                            ClearEnemies();
                            BackToMenu();
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        private Vector2 RandomSnowPos()
        {
            int maxX = new Random().Next(0, _graphics.PreferredBackBufferWidth + 400 * resScale);
            int maxY = new Random().Next(0, _graphics.PreferredBackBufferHeight - 475 * resScale);
            return new Vector2(maxX, maxY);
        }
        private Vector2 RandomRainPos()
        {
            int maxX = new Random().Next(0, _graphics.PreferredBackBufferWidth);
            int maxY = 0;
            return new Vector2(maxX, maxY);
        }

        void SPPlayerMapMove(GameTime gameTime)
        {
            if (canMapMove)
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
                    if (redguyMapRect.Intersects(gateRect) && !inTutorial)
                    {
                        mapPos = lastKnownPos;
                        isMapMoving = false;
                    }
                    for (int i = 0; i < mapBoundaries.Count; i++)
                    {
                        if (redguyMapRect.Intersects(mapBoundaries[i]))
                        {
                            mapPos = lastKnownPos;
                            isMapMoving = false;
                        }
                    }
                    lastKnownPos = mapPos;
                    mapPos.X += movementX * ((float)mapSpeed * resScale);
                    mapPos.Y += movementY * ((float)mapSpeed * resScale);
                    if (distanceToTravelTotal < 1 * resScale)
                    {
                        isMapMoving = false;
                    }
                }
            }
        }

        void MPPlayerMapMove(GameTime gameTime)
        {
            if (canMapMove)
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
                        if (coopMapRect.Intersects(mountains[i].mountainRect))
                        {
                            mapPos = lastKnownPos;
                            isMapMoving = false;
                        }
                    }
                    if (coopMapRect.Intersects(gateRect))
                    {
                        mapPos = lastKnownPos;
                        isMapMoving = false;
                    }
                    for (int i = 0; i < mapBoundaries.Count; i++)
                    {
                        if (coopMapRect.Intersects(mapBoundaries[i]))
                        {
                            mapPos = lastKnownPos;
                            isMapMoving = false;
                        }
                    }
                    lastKnownPos = mapPos;
                    mapPos.X += movementX * ((float)mapSpeed * resScale);
                    mapPos.Y += movementY * ((float)mapSpeed * resScale);
                    if (distanceToTravelTotal < 1 * resScale)
                    {
                        isMapMoving = false;
                    }
                }
            }
        }

        void SPCombatMove(GameTime gameTime)
        {
            if (canCombatMove)
            {
                if (redguyHealth >= 1)
                {
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
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveDown))
                            {
                                if (redguyPos.Y <= 345 * resScale)
                                {
                                    redguyPos.Y += dodgeDistance;
                                    RedDodge(gameTime);
                                } else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveUp))
                            {
                                if (redguyPos.Y >= 250 * resScale)
                                {
                                    redguyPos.Y -= dodgeDistance;
                                    RedDodge(gameTime);
                                } else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveRight))
                            {
                                if (redguyPos.X <= 245 * resScale)
                                {
                                    redguyPos.X += dodgeDistance;
                                    RedDodge(gameTime);
                                } else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveLeft))
                            {
                                if (redguyPos.X >= 70 * resScale)
                                {
                                    redguyPos.X -= dodgeDistance;
                                    RedDodge(gameTime);
                                } else
                                {
                                    RedDodge(gameTime);

                                }
                            }
                        }
                    }
                }
            }
        }

        void MPCombatMove(GameTime gameTime)
        {
            if (canCombatMove)
            {
                if (redguyHealth >= 1)
                {
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
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveDown))
                            {
                                if (redguyPos.Y <= 345 * resScale)
                                {
                                    redguyPos.Y += dodgeDistance;
                                    RedDodge(gameTime);
                                }
                                else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveUp))
                            {
                                if (redguyPos.Y >= 250 * resScale)
                                {
                                    redguyPos.Y -= dodgeDistance;
                                    RedDodge(gameTime);
                                }
                                else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveRight))
                            {
                                if (redguyPos.X <= 245 * resScale)
                                {
                                    redguyPos.X += dodgeDistance;
                                    RedDodge(gameTime);
                                }
                                else
                                {
                                    RedDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(redguyDodge) && Keyboard.GetState().IsKeyDown(redguyMoveLeft))
                            {
                                if (redguyPos.X >= 70 * resScale)
                                {
                                    redguyPos.X -= dodgeDistance;
                                    RedDodge(gameTime);
                                }
                                else
                                {
                                    RedDodge(gameTime);

                                }
                            }
                        }
                    }
                }
                if (blueguyHealth >= 1)
                {
                    if (!blueIsDodging)
                    {
                        if (Keyboard.GetState().IsKeyDown(blueguyMoveUp) && blueguyPos.Y >= 180 * resScale)
                        {
                            blueguyPos.Y -= combatSpeed;
                        }
                        if (Keyboard.GetState().IsKeyDown(blueguyMoveDown) && blueguyPos.Y <= 390 * resScale)
                        {
                            blueguyPos.Y += combatSpeed;
                        }
                        if (Keyboard.GetState().IsKeyDown(blueguyMoveLeft) && blueguyPos.X >= 1 * resScale)
                        {
                            blueguyPos.X -= combatSpeed;
                        }
                        if (Keyboard.GetState().IsKeyDown(blueguyMoveRight) && blueguyPos.X <= 340 * resScale)
                        {
                            blueguyPos.X += combatSpeed;
                        }
                        if (Keyboard.GetState().IsKeyDown(blueguyShoot))
                        {
                            if (bluefireDelay == false)
                            {
                                bullets.Add(new Bullet(bulletSprite, blueguyPos + new Vector2(50 * resScale, 40 * resScale), new Vector2(7 * resScale, 0), resScale));
                                bluefireDelay = true;
                            }
                        }
                        if (Keyboard.GetState().IsKeyUp(blueguyShoot))
                        {
                            bluefireDelay = false;
                        }

                        // Blue dodging
                        if (gameTime.TotalGameTime.TotalMilliseconds > bluetimeSinceLastDodge + blueDodgeDelay)
                        {
                            if (Keyboard.GetState().IsKeyDown(blueguyDodge) && Keyboard.GetState().IsKeyDown(blueguyMoveDown))
                            {
                                if (blueguyPos.Y <= 345 * resScale)
                                {
                                    blueguyPos.Y += dodgeDistance;
                                    BlueDodge(gameTime);
                                }
                                else
                                {
                                    BlueDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(blueguyDodge) && Keyboard.GetState().IsKeyDown(blueguyMoveUp))
                            {
                                if (blueguyPos.Y >= 250 * resScale)
                                {
                                    blueguyPos.Y -= dodgeDistance;
                                    BlueDodge(gameTime);
                                }
                                else
                                {
                                    BlueDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(blueguyDodge) && Keyboard.GetState().IsKeyDown(blueguyMoveRight))
                            {
                                if (blueguyPos.X <= 245 * resScale)
                                {
                                    blueguyPos.X += dodgeDistance;
                                    BlueDodge(gameTime);
                                }
                                else
                                {
                                    BlueDodge(gameTime);
                                }
                            }
                            if (Keyboard.GetState().IsKeyDown(blueguyDodge) && Keyboard.GetState().IsKeyDown(blueguyMoveLeft))
                            {
                                if (blueguyPos.X >= 70 * resScale)
                                {
                                    blueguyPos.X -= dodgeDistance;
                                    BlueDodge(gameTime);
                                }
                                else
                                {
                                    BlueDodge(gameTime);
                                }
                            }
                        }
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
            healthFlashR = 0;
        }

        void BlueDodge(GameTime gameTime)
        {
            bluetimeSinceLastDodge = gameTime.TotalGameTime.TotalMilliseconds;
            blueInvulnTimerD = gameTime.TotalGameTime.TotalMilliseconds;
            blueIsDodging = true;
            blueIsDodging = false;
            healthFlashB = 0;
        }

        void SinglePlayerEncounter(GameTime gameTime)
        {
            if (isMapMoving && !redguyMapRect.Intersects(storeRect))
            {
                if (isInPlains)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
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
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
                            {
                                SnowEncounter(gameTime);
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            else
                            {
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                } else if (isInSwamp)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
                            {
                                SwampEncounter(gameTime);
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            else
                            {
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                } else if (isInDesert)
                {

                }
            }
        }

        void CoopEncounter(GameTime gameTime)
        {
            if (isMapMoving && !coopMapRect.Intersects(storeRect))
            {
                if (isInPlains)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
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
                }
                else if (isInSnow)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
                            {
                                SnowEncounter(gameTime);
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            else
                            {
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }
                else if (isInSwamp)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounter + 3000)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastEncounterAttempt + 1500)
                        {
                            int randomNumber = new Random().Next(1, 5);
                            if (randomNumber == 1)
                            {
                                SwampEncounter(gameTime);
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            else
                            {
                                timeSinceLastEncounterAttempt = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }
                else if (isInDesert)
                {

                }
            }
        }

        void PlainsEncounter(GameTime gameTime)
        {
            ClearEnemies();
            isMapMoving = false;
            canMapMove = false;
            redguyPos = new Vector2(162 * resScale, 258 * resScale);
            blueguyPos = new Vector2(60 * resScale, 330 * resScale);
            lastKnownPos = mapPos;
            timesFlashed = 0;
            encounterFlashing = true;
            int etf = new Random().Next(1, 101);
            if (etf <= 55)
            {
                enemyToFight = 1;
            }
            else if (etf >= 56)
            {
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
                }
                else if (extraSmiley == 2)
                {
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(500 * resScale, 420 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                    smileys.Add(new Smiley(smileyEnemySprite, new Vector2(510 * resScale, 310 * resScale), new Vector2(550 * resScale, 290 * resScale), resScale, 5 * healthMultiplier, 4));
                }
                for (int i = 0; i < smileys.Count; i++)
                {
                    smileys[i].moveStart = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            else if (enemyToFight == 2)
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

        void SnowEncounter(GameTime gameTime)
        {
            ClearEnemies();
            isMapMoving = false;
            canMapMove = false;
            redguyPos = new Vector2(162 * resScale, 258 * resScale);
            blueguyPos = new Vector2(60 * resScale, 330 * resScale);
            lastKnownPos = mapPos;
            timesFlashed = 0;
            encounterFlashing = true;
            int etf = new Random().Next(1, 101);
            if (etf <= 60)
            {
                enemyToFight = 4;
            } else if (etf >= 61)
            {
                enemyToFight = 3;
            }
            if (enemyToFight == 3)
            {
                statues.Add(new Statue(this, statueEnemySprite, chargeUpSprite, beamSprite, new Vector2(500 * resScale, 200 * resScale), resScale, 50 * healthMultiplier));
            }
            else if (enemyToFight == 4)
            {
                yetis.Add(new Yeti(this, gameTime, yetiSprite, rockSprite, new Vector2(500 * resScale, 200 * resScale), resScale, 30 * healthMultiplier));
                yetis.Add(new Yeti(this, gameTime, yetiSprite, rockSprite, new Vector2(600 * resScale, 200 * resScale), resScale, 30 * healthMultiplier));
            }
        }

        void SwampEncounter(GameTime gameTime)
        {
            ClearEnemies();
            isMapMoving = false;
            canMapMove = false;
            redguyPos = new Vector2(162 * resScale, 258 * resScale);
            blueguyPos = new Vector2(60 * resScale, 330 * resScale);
            lastKnownPos = mapPos;
            timesFlashed = 0;
            encounterFlashing = true;
            int etf = new Random().Next(1, 101);
            enemyToFight = 5;
            if (enemyToFight == 5)
            {
                frogs.Add(new Frog(this, frogSprite, frogAttackingSprite, frogTongueSprite, new Vector2(500 * resScale, 200 * resScale), resScale, 7 * healthMultiplier));
                frogs.Add(new Frog(this, frogSprite, frogAttackingSprite, frogTongueSprite, new Vector2(600 * resScale, 300 * resScale), resScale, 7 * healthMultiplier));
                frogs.Add(new Frog(this, frogSprite, frogAttackingSprite, frogTongueSprite, new Vector2(400 * resScale, 400 * resScale), resScale, 7 * healthMultiplier));
                int randomnumb = new Random().Next(1, 11);
                if (randomnumb <= 1)
                {
                    frogs.Add(new Frog(this, frogSprite, frogAttackingSprite, frogTongueSprite, new Vector2(600 * resScale, 400 * resScale), resScale, 7 * healthMultiplier));
                }
            }
        }

        void EndCombat(GameTime gameTime)
        {
            wonCurrentEncounter = false;
            canCombatMove = true;
            canMapMove = true;
            addedCoin = false;
            timeSinceLastEncounter = gameTime.TotalGameTime.TotalMilliseconds;
            enemyToFight = 0;
            inCombat = false;
            inWorldMap = true;
            mapPos = lastKnownPos;
            redguyHealth = 3;
            blueguyHealth = 3;
            ClearScenery();
            SpawnScenery();
        }

        void WinGame()
        {
            ClearEnemies();
            inCombat = false;
            inWorldMap = false;
            isInMainMenu = false;
            inWinScreen = true;
            gameHasStarted = false;
        }

        void AddCoin()
        {
            // Enemy to fight values:
            // 0 = none
            // 1 = smiley
            // 2 = smiley chieftain
            // 3 = statue
            // 4 = yeti
            // 5 = frog
            switch (enemyToFight)
            {
                case 1:
                    if (!hasFought1)
                    {
                        hasFought1 = true;
                        if (playerCoins > 0)
                        {
                            coinDistance += 20;
                        }
                        playerCoins++;
                        coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
                        addedCoin = true;
                    }
                    break;
                case 2:
                    if (!hasFought2)
                    {
                        hasFought2 = true;
                        if (playerCoins > 0)
                        {
                            coinDistance += 20;
                        }
                        playerCoins++;
                        coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
                        addedCoin = true;
                    }
                    break;
                case 3:
                    if (!hasFought3)
                    {
                        hasFought3 = true;
                        if (playerCoins > 0)
                        {
                            coinDistance += 20;
                        }
                        playerCoins++;
                        coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
                        addedCoin = true;
                    }
                    break;
                case 4:
                    if (!hasFought4)
                    {
                        hasFought4 = true;
                        if (playerCoins > 0)
                        {
                            coinDistance += 20;
                        }
                        playerCoins++;
                        coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
                        addedCoin = true;
                    }
                    break;
                case 5:
                    if (!hasFought5)
                    {
                        hasFought5 = true;
                        if (playerCoins > 0)
                        {
                            coinDistance += 20;
                        }
                        playerCoins++;
                        coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
                        addedCoin = true;
                    }
                    break;
            }
        }

        // FOR RESOLUTION CHANGES
        void AddCoin2()
        {
            coinSprites.Clear();
            coinDistance = 0;
            for (int i = 0; i < playerCoins; i++)
            {
                if (i > 0)
                {
                    coinDistance += 20;
                }
                coinSprites.Add(new Rectangle(0, (int)coinDistance * resScale, (int)coinSprite.Width * resScale, (int)coinSprite.Height * resScale));
            }
        }

        void ForceEncounter(GameTime gameTime)
        {
            if (isInPlains)
            {
                PlainsEncounter(gameTime);
            }
            else if (isInSnow)
            {
                SnowEncounter(gameTime);
            }
            else if (isInDesert)
            {

            }
            else if (isInSwamp)
            {
                SwampEncounter(gameTime);
            }
        }

        void BackToMenu()
        {
            narb = false;            
            if (inTutorial)
            {
                mapPos = new Vector2(390 * resScale, 225 * resScale);
                lastKnownPos = new Vector2(390 * resScale, 225 * resScale);
            }
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
            int randomchancexd = new Random().Next(1, 251);
            if (randomchancexd == 1)
            {
                narb = true;
                Window.Title = "NARB :DDD";
            }
            inTutorial = false;
            isInMainMenu = true;
            gameHasStarted = false;
            inWorldMap = false;
            inCombat = false;
            inCoop = false;
            inSingleplayer = false;
            isMapMoving = false;
            ClearScenery();
            ClearEnemies();
        }

        void GameOver(GameTime gameTime)
        {
            if (redguyHealth <= 0 || blueguyHealth <= 0)
            {
                mapPos = new Vector2(390 * resScale, 225 * resScale);
                lastKnownPos = new Vector2(390 * resScale, 225 * resScale);
            }

            playerCoins = 0;
            hasFought1 = false;
            hasFought2 = false;
            hasFought3 = false;
            hasFought4 = false;
            hasFought5 = false;

            damageHat.isPurchased = false;
            damageHat.isEquipped = false;
            speedHat.isPurchased = false;
            speedHat.isEquipped = false;

            AddCoin2();

            // REPLACE WITH GAME OVER SCREEN, THEN MENU
            BackToMenu();
        }

        // That scenery.
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

            // Trees
            trees.Add(new Tree(treeSprite, new Vector2(440 * resScale, 225 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(550 * resScale, 90 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(450 * resScale, 125 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(600 * resScale, 205 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(445 * resScale, 20 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(460 * resScale, 237 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(433 * resScale, 244 * resScale), resScale));
            trees.Add(new Tree(treeSprite, new Vector2(568 * resScale, 100 * resScale), resScale));
        }

        void ClearScenery()
        {
            mapBoundaries.Clear();
            mountains.Clear();
            trees.Clear();
        }

        void ClearEnemies()
        {
            smileys.Clear();
            smileyCheiftain.Clear();
            statues.Clear();
            bullets.Clear();
            yetis.Clear();
            frogs.Clear();
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
            singleplayerButtonRect = new Rectangle(290 * resScale, 180 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            coopButtonRect = new Rectangle(290 * resScale, 260 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            settingsButtonRect = new Rectangle(5 * resScale, 400 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            exitButtonRect = new Rectangle(595 * resScale, 400 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);
            tutorialButtonRect = new Rectangle(290 * resScale, 340 * resScale, oneButton.Width * resScale, oneButton.Height * resScale);

            //Main Menu Drawing
            if (isInMainMenu)
            {
                _spriteBatch.Draw(mainMenuSprite, mainmenuRect, Color.White);
                if (narb)
                {
                    _spriteBatch.Draw(narbBackground, new Rectangle(0, -100 * resScale, 800 * resScale, 580 * resScale), Color.White);
                }
                _spriteBatch.Draw(singleplayerButton, singleplayerButtonRect, Color.White);
                _spriteBatch.Draw(coopButton, coopButtonRect, Color.White);
                _spriteBatch.Draw(settingsButton, settingsButtonRect, Color.White);
                _spriteBatch.Draw(exitButton, exitButtonRect, Color.White);
                _spriteBatch.Draw(tutorialButton, tutorialButtonRect, Color.White);
                _spriteBatch.Draw(oneButton, mouseRect, Color.Transparent);
            }
            // Tutorial Drawing
            if (inTutorial)
            {
                Rectangle tutorialWorldMapRect = new Rectangle(0, 0, tutorialWorldMap.Width * resScale, tutorialWorldMap.Height * resScale);
                if (inWorldMap)
                {
                    tutorialinteractable = new Rectangle(490 * resScale, 160 * resScale, 160 * resScale, 160 * resScale);
                    _spriteBatch.Draw(tutorialWorldMap, tutorialWorldMapRect, Color.White);
                    redguyMapRect = new Rectangle((int)mapPos.X, (int)mapPos.Y, redguySprite.Width * 2 * resScale, redguySprite.Height * 2 * resScale);
                    if (mousePos.X < mapPos.X + 7 * 2 * resScale)
                    {
                        _spriteBatch.Draw(redguyFlipped, redguyMapRect, Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(redguySprite, redguyMapRect, Color.White);
                    }
                }
                if (inCombat)
                {
                    redguyRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguySprite.Width * (3 * resScale), redguySprite.Height * (3 * resScale));
                    redguyHead = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 2 * resScale, redguySprite.Width * (3 * resScale) - 5 * resScale, redguySprite.Height * (3 * resScale) - 50 * resScale);
                    redguyBody = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 30 * resScale, redguySprite.Width * (3 * resScale) - 10 * resScale, redguySprite.Height * (3 * resScale) - 35 * resScale);
                    Rectangle redguyDodgeRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguyDodgeSprite.Width * (3 * resScale), redguyDodgeSprite.Height * (3 * resScale));

                    _spriteBatch.Draw(tutorialCombatMap, tutorialWorldMapRect, Color.White);
                    if (gameTime.TotalGameTime.TotalMilliseconds > redInvulnTimerD + redInvulnTimeD)
                    {
                        if (redguyHealth >= 3)
                        {
                            _spriteBatch.Draw(redguySprite, redguyRect, Color.White);
                        }
                        else if (redguyHealth == 2)
                        {
                            _spriteBatch.Draw(redguyHurt1, redguyRect, Color.White);
                            while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                            {
                                healthFlashR -= 10;
                                redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            if (healthFlashR >= 0)
                            {
                                _spriteBatch.Draw(redguyHurt1, redguyRect, new Color(Color.Red, healthFlashR));
                            }
                        }
                        else if (redguyHealth <= 1)
                        {
                            _spriteBatch.Draw(redguyHurt2, redguyRect, Color.White);
                            while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                            {
                                healthFlashR -= 10;
                                redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                            }
                            if (healthFlashR >= 0)
                            {
                                _spriteBatch.Draw(redguyHurt2, redguyRect, new Color(Color.Red, healthFlashR));
                            }
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
                    for (int i = 0; i < smileys.Count; i++)
                    {
                        smileys[i].Draw(_spriteBatch);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, smileys[i].smileyRect, Color.Red);
                        }
                    }
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Draw(_spriteBatch);
                    }
                }
            }
            if (gameHasStarted)
            {
                if (inWorldMap)
                {
                    // MAP BOUNDARIES
                    mapBoundaries.Add(new Rectangle(120 * resScale, 50 * resScale, 45 * resScale, 200 * resScale));
                    mapBoundaries.Add(new Rectangle(145 * resScale, 240 * resScale, 40 * resScale, 110 * resScale));
                    mapBoundaries.Add(new Rectangle(100 * resScale, 350 * resScale, 55 * resScale, 80 * resScale));
                    mapBoundaries.Add(new Rectangle(135 * resScale, 430 * resScale, 115 * resScale, 40 * resScale));
                    mapBoundaries.Add(new Rectangle(250 * resScale, 445 * resScale, 150 * resScale, 30 * resScale));
                    mapBoundaries.Add(new Rectangle(400 * resScale, 450 * resScale, 220 * resScale, 25 * resScale));
                    mapBoundaries.Add(new Rectangle(620 * resScale, 290 * resScale, 40 * resScale, 200 * resScale));
                    mapBoundaries.Add(new Rectangle(645 * resScale, 100 * resScale, 40 * resScale, 190 * resScale));
                    mapBoundaries.Add(new Rectangle(600 * resScale, 0 * resScale, 50 * resScale, 115 * resScale));
                    mapBoundaries.Add(new Rectangle(305 * resScale, 0 * resScale, 295 * resScale, 10 * resScale));
                    mapBoundaries.Add(new Rectangle(165 * resScale, 0 * resScale, 145 * resScale, 35 * resScale));
                    mapBoundaries.Add(new Rectangle(165 * resScale, 0 * resScale, 110 * resScale, 60 * resScale));

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
                    if (showBoundaries)
                    {
                        for (int i = 0; i < mapBoundaries.Count; i++)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, mapBoundaries[i], Color.Red);
                        }
                    }
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
                    storeRect = new Rectangle(550 * resScale, 165 * resScale, storeSprite.Width * resScale, storeSprite.Height * resScale);
                    _spriteBatch.Draw(whiteSquareSprite, new Rectangle(545 * resScale, 215 * resScale, (storeSprite.Width + 10) * resScale, 13 * resScale), new Color(Color.Black, 100));
                    _spriteBatch.Draw(storeSprite, storeRect, Color.White);
                    for (int i = 0; i < trees.Count; i++)
                    {
                        trees[i].Draw(_spriteBatch);
                    }
                    _spriteBatch.Draw(whiteSquareSprite, new Rectangle(460 * resScale, 392 * resScale, (storeSprite.Width + 10) * resScale, 13 * resScale), new Color(Color.Black, 100));
                    _spriteBatch.Draw(swampTreeSprite, new Rectangle(460 * resScale, 325 * resScale, swampTreeSprite.Width * resScale, swampTreeSprite.Height * resScale), Color.White);
                    // Bosses
                    if (bobUp)
                    {
                        smileyBossMapRect = new Rectangle(515 * resScale, 25 * resScale, smileyEnemySprite.Width * 3 * resScale, smileyEnemySprite.Height * 3 * resScale);
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBob + 1250)
                        {
                            bobUp = false;
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                    else
                    {
                        smileyBossMapRect = new Rectangle(515 * resScale, 30 * resScale, smileyEnemySprite.Width * 3 * resScale, smileyEnemySprite.Height * 3 * resScale);
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBob + 1250)
                        {
                            bobUp = true;
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                    _spriteBatch.Draw(smileyEnemySprite, smileyBossMapRect, Color.White);
                    if (inSingleplayer)
                    {
                        if (mousePos.X < mapPos.X + 7 * resScale)
                        {
                            _spriteBatch.Draw(redguyFlipped, redguyMapRect, Color.White);
                            if (damageHat.isEquipped)
                            {
                                damageHat.position = new Vector2(mapPos.X - 1 * resScale, mapPos.Y - 8 * resScale);
                                damageHat.scale = resScale;
                                damageHat.Draw(_spriteBatch);
                            }
                            else if (speedHat.isEquipped)
                            {
                                speedHat.position = new Vector2(mapPos.X - 1 * resScale, mapPos.Y - 8 * resScale);
                                speedHat.scale = resScale;
                                speedHat.Draw(_spriteBatch);
                            }
                        }
                        else
                        {
                            _spriteBatch.Draw(redguySprite, redguyMapRect, Color.White);
                            if (damageHat.isEquipped)
                            {
                                damageHat.position = new Vector2(mapPos.X + 1 * resScale, mapPos.Y - 8 * resScale);
                                damageHat.scale = resScale;
                                damageHat.Draw(_spriteBatch);
                            }
                            else if (speedHat.isEquipped)
                            {
                                speedHat.position = new Vector2(mapPos.X + 1 * resScale, mapPos.Y - 8 * resScale);
                                speedHat.scale = resScale;
                                speedHat.Draw(_spriteBatch);
                            }
                        }
                        if (mapPos.Y < storeRect.Y + 30 * resScale)
                        {
                            _spriteBatch.Draw(storeSprite, storeRect, Color.White);
                        }
                        for (int i = 0; i < trees.Count; i++)
                        {
                            if (mapPos.Y < trees[i].position.Y + 22 * resScale)
                            {
                                trees[i].Draw(_spriteBatch);
                            }
                        }
                        if (mapPos.Y < 325 * resScale + 47 * resScale)
                        {
                            _spriteBatch.Draw(swampTreeSprite, new Rectangle(460 * resScale, 325 * resScale, swampTreeSprite.Width * resScale, swampTreeSprite.Height * resScale), Color.White);
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
                        if (mapPos.Y < storeRect.Y + 30 * resScale)
                        {
                            _spriteBatch.Draw(storeSprite, storeRect, Color.White);
                        }
                        for (int i = 0; i < trees.Count; i++)
                        {
                            if (mapPos.Y < trees[i].position.Y + 22 * resScale)
                            {
                                trees[i].Draw(_spriteBatch);
                            }
                        }
                        if (mapPos.Y < 325 * resScale + 47 * resScale)
                        {
                            _spriteBatch.Draw(swampTreeSprite, new Rectangle(460 * resScale, 325 * resScale, swampTreeSprite.Width * resScale, swampTreeSprite.Height * resScale), Color.White);
                        }
                    }
                    for (int i = 0; i < coinSprites.Count; i++)
                    {
                        _spriteBatch.Draw(coinSprite, coinSprites[i], Color.White);
                    }
                }
                if (inCombat)
                {
                    // Drawing maps
                    Rectangle combatMapRect = new Rectangle(0, 0, plainsBackgroundSprite.Width * resScale, plainsBackgroundSprite.Height * resScale);
                    if (isInPlains)
                    {
                        _spriteBatch.Draw(plainsBackgroundSprite, combatMapRect, Color.White);
                    } else if (isInSnow)
                    {
                        _spriteBatch.Draw(snowBackgroundSprite, combatMapRect, Color.White);
                    } else if (isInSwamp)
                    {
                        _spriteBatch.Draw(swampBackgroundSprite, combatMapRect, Color.White);
                    }
                    if (inSingleplayer)
                    {
                        //Declaring Rectangles and whatnot
                        redguyRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguySprite.Width * (3 * resScale), redguySprite.Height * (3 * resScale));
                        redguyHead = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 2 * resScale, redguySprite.Width * (3 * resScale) - 5 * resScale, redguySprite.Height * (3 * resScale) - 50 * resScale);
                        redguyBody = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 30 * resScale, redguySprite.Width * (3 * resScale) - 10 * resScale, redguySprite.Height * (3 * resScale) - 35 * resScale);
                        Rectangle redguyDodgeRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguyDodgeSprite.Width * (3 * resScale), redguyDodgeSprite.Height * (3 * resScale));

                        //Background items drawing

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
                                while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                                {
                                    healthFlashR -= 10;
                                    redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashR >= 0)
                                {
                                    _spriteBatch.Draw(redguyHurt1, redguyRect, new Color(Color.Red, healthFlashR));
                                }
                            }
                            else if (redguyHealth <= 1)
                            {
                                _spriteBatch.Draw(redguyHurt2, redguyRect, Color.White);
                                while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                                {
                                    healthFlashR -= 10;
                                    redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashR >= 0)
                                {
                                    _spriteBatch.Draw(redguyHurt2, redguyRect, new Color(Color.Red, healthFlashR));
                                }
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
                        if (damageHat.isEquipped)
                        {
                            damageHat.position = new Vector2(redguyPos.X + 2 * resScale, redguyPos.Y - 8 * 3 * resScale);
                            damageHat.scale = resScale * 3;
                            damageHat.Draw(_spriteBatch);
                        }
                        if (speedHat.isEquipped)
                        {
                            speedHat.position = new Vector2(redguyPos.X + 2 * resScale, redguyPos.Y - 8 * 3 * resScale);
                            speedHat.scale = resScale * 3;
                            speedHat.Draw(_spriteBatch);
                        }
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, redguyHead, Color.Red);
                            _spriteBatch.Draw(whiteSquareSprite, redguyBody, Color.Red);
                        }
                    }
                    if (inCoop)
                    {
                        redguyRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguySprite.Width * (3 * resScale), redguySprite.Height * (3 * resScale));
                        redguyHead = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 2 * resScale, redguySprite.Width * (3 * resScale) - 5 * resScale, redguySprite.Height * (3 * resScale) - 50 * resScale);
                        redguyBody = new Rectangle((int)redguyPos.X + 2 * resScale, (int)redguyPos.Y + 30 * resScale, redguySprite.Width * (3 * resScale) - 10 * resScale, redguySprite.Height * (3 * resScale) - 35 * resScale);
                        Rectangle redguyDodgeRect = new Rectangle((int)redguyPos.X, (int)redguyPos.Y, redguyDodgeSprite.Width * (3 * resScale), redguyDodgeSprite.Height * (3 * resScale));
                        blueguyRect = new Rectangle((int)blueguyPos.X, (int)blueguyPos.Y, blueguySprite.Width * (3 * resScale), blueguySprite.Height * (3 * resScale));
                        blueguyHead = new Rectangle((int)blueguyPos.X + 2 * resScale, (int)blueguyPos.Y + 2 * resScale, blueguySprite.Width * (3 * resScale) - 5 * resScale, blueguySprite.Height * (3 * resScale) - 50 * resScale);
                        blueguyBody = new Rectangle((int)blueguyPos.X + 2 * resScale, (int)blueguyPos.Y + 30 * resScale, blueguySprite.Width * (3 * resScale) - 10 * resScale, blueguySprite.Height * (3 * resScale) - 35 * resScale);
                        Rectangle blueguyDodgeRect = new Rectangle((int)blueguyPos.X, (int)blueguyPos.Y, blueguyDodgeSprite.Width * (3 * resScale), blueguyDodgeSprite.Height * (3 * resScale));

                        if (redguyHealth <= 0)
                        {
                            _spriteBatch.Draw(graveStoneSprite, redguyRect, Color.White);
                        }
                        if (blueguyHealth <= 0)
                        {
                            _spriteBatch.Draw(graveStoneSprite, blueguyRect, Color.White);
                        }

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
                                while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                                {
                                    healthFlashR -= 10;
                                    redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashR >= 0)
                                {
                                    _spriteBatch.Draw(redguyHurt1, redguyRect, new Color(Color.Red, healthFlashR));
                                }
                            }
                            else if (redguyHealth == 1)
                            {
                                _spriteBatch.Draw(redguyHurt2, redguyRect, Color.White);
                                while (healthFlashR >= 0 && gameTime.TotalGameTime.TotalMilliseconds > redLastIncrement + 1)
                                {
                                    healthFlashR -= 10;
                                    redLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashR >= 0)
                                {
                                    _spriteBatch.Draw(redguyHurt2, redguyRect, new Color(Color.Red, healthFlashR));
                                }
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
                            else if (redguyHealth == 1)
                            {
                                _spriteBatch.Draw(redguyHurt2Dodge, redguyDodgeRect, Color.White);
                            }
                        }

                        if (gameTime.TotalGameTime.TotalMilliseconds > blueInvulnTimerD + blueInvulnTimeD)
                        {
                            if (blueguyHealth >= 3)
                            {
                                _spriteBatch.Draw(blueguySprite, blueguyRect, Color.White);
                            }
                            else if (blueguyHealth == 2)
                            {
                                _spriteBatch.Draw(blueguyHurt1, blueguyRect, Color.White);
                                while (healthFlashB >= 0 && gameTime.TotalGameTime.TotalMilliseconds > blueLastIncrement + 1)
                                {
                                    healthFlashB -= 10;
                                    blueLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashB >= 0)
                                {
                                    _spriteBatch.Draw(blueguyHurt1, blueguyRect, new Color(Color.Red, healthFlashB));
                                }
                            }
                            else if (blueguyHealth == 1)
                            {
                                _spriteBatch.Draw(blueguyHurt2, blueguyRect, Color.White);
                                while (healthFlashB >= 0 && gameTime.TotalGameTime.TotalMilliseconds > blueLastIncrement + 1)
                                {
                                    healthFlashB -= 10;
                                    blueLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                                }
                                if (healthFlashB >= 0)
                                {
                                    _spriteBatch.Draw(blueguyHurt2, blueguyRect, new Color(Color.Red, healthFlashB));
                                }
                            }
                        }
                        else
                        {
                            if (blueguyHealth >= 3)
                            {
                                _spriteBatch.Draw(blueguyDodgeSprite, blueguyDodgeRect, Color.White);
                            }
                            else if (blueguyHealth == 2)
                            {
                                _spriteBatch.Draw(blueguyHurt1Dodge, blueguyDodgeRect, Color.White);
                            }
                            else if (blueguyHealth == 1)
                            {
                                _spriteBatch.Draw(blueguyHurt2Dodge, blueguyDodgeRect, Color.White);
                            }
                        }

                        if (blueguyPos.Y < redguyPos.Y)
                        {
                            if (redguyHealth <= 0)
                            {
                                _spriteBatch.Draw(graveStoneSprite, redguyRect, Color.White);
                            }
                        }
                        if (redguyPos.Y < blueguyPos.Y)
                        {
                            if (blueguyHealth <= 0)
                            {
                                _spriteBatch.Draw(graveStoneSprite, blueguyRect, Color.White);
                            }
                        }

                        if (drawhitboxes)
                        {
                            if (redguyHealth > 0)
                            {
                                _spriteBatch.Draw(whiteSquareSprite, redguyHead, Color.Red);
                                _spriteBatch.Draw(whiteSquareSprite, redguyBody, Color.Red);
                            }
                            if (blueguyHealth > 0)
                            {
                                _spriteBatch.Draw(whiteSquareSprite, blueguyHead, Color.Red);
                                _spriteBatch.Draw(whiteSquareSprite, blueguyBody, Color.Red);
                            }
                        }
                    }

                    // Enemies
                    for (int i = 0; i < smileyCheiftain.Count; i++)
                    {
                        smileyCheiftain[i].Draw(_spriteBatch);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, smileyCheiftain[i].rectangle, Color.Red);
                        }
                    }
                    for (int i = 0; i < smileyBoss.Count; i++)
                    {
                        smileyBoss[i].Draw(_spriteBatch);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, smileyBoss[i].hitBox, Color.Red);
                        }
                    }
                    for (int i = 0; i < smileys.Count; i++)
                    {
                        smileys[i].Draw(_spriteBatch);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, smileys[i].smileyRect, Color.Red);
                        }
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
                    for (int i = 0; i < statues.Count; i++)
                    {
                        statues[i].Draw(_spriteBatch, gameTime);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, statues[i].rectangle, Color.Red);
                        }
                    }
                    for (int i = 0; i < yetis.Count; i++)
                    {
                        yetis[i].Draw(_spriteBatch, gameTime);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, yetis[i].hitbox, Color.Red);
                            _spriteBatch.Draw(whiteSquareSprite, yetis[i].rockRect, Color.Red);
                        }
                    }
                    for (int i = 0; i < frogs.Count; i++)
                    {
                        frogs[i].Draw(_spriteBatch, gameTime);
                        if (drawhitboxes)
                        {
                            _spriteBatch.Draw(whiteSquareSprite, frogs[i].hitbox, Color.Red);
                        }
                    }
                    // Bullets
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Draw(_spriteBatch);
                    }
                }
                // Snow / rain drawing
                for (int i = 0; i < snow.Count; i++)
                {
                    snow[i].Draw(_spriteBatch);
                }
                // Encounter Flash drawing
                if (encounterFlashing)
                {
                    if (!debugmode)
                    {
                        IsMouseVisible = false;
                    }
                    if (flashDarken)
                    {
                        if (flashAlpha <= 250 && gameTime.TotalGameTime.TotalMilliseconds > flashLastIncrement + 1)
                        {
                            flashAlpha += 15;
                            flashLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        _spriteBatch.Draw(bulletSprite, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), new Color(Color.White, flashAlpha));
                        if (flashAlpha >= 250)
                        {
                            timesFlashed++;
                            flashDarken = false;
                            flashLighten = true;
                        }
                    }
                    if (flashLighten)
                    {
                        if (flashAlpha >= 0 && gameTime.TotalGameTime.TotalMilliseconds > flashLastIncrement + 1)
                        {
                            flashAlpha -= 15;
                            flashLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                        _spriteBatch.Draw(bulletSprite, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), new Color(Color.White, flashAlpha));
                        if (flashAlpha <= 0)
                        {
                            timesFlashed++;
                            flashLighten = false;
                            flashDarken = true;
                        }
                    }
                }
                if (encounterFlashing && timesFlashed >= 6)
                {
                    encounterFlashing = false;
                    inWorldMap = false;
                    inCombat = true;
                    timesFlashed = 0;
                }
                // Win screen drawing
                if (wonCurrentEncounter)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds < timeSinceLastWon + 2000)
                    {
                        _spriteBatch.Draw(youWonText, new Rectangle(0, 0, youWonText.Width * resScale, youWonText.Height * resScale), Color.White);
                    }
                    if (addedCoin && gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastWon + 2000 && gameTime.TotalGameTime.TotalMilliseconds < timeSinceLastWon + 5000)
                    {
                        _spriteBatch.Draw(encounterBonusText, new Rectangle(0, 0, encounterBonusText.Width * resScale, encounterBonusText.Height * resScale), Color.White);
                    }
                }
                if (inStore)
                {
                    storeBackgroundRect = new Rectangle(0, 0, shopBackgroundSprite.Width * resScale, shopBackgroundSprite.Height * resScale);
                    storeCounterRect = new Rectangle(0, 315 * resScale, shopCounterSprite.Width * resScale, shopCounterSprite.Height * resScale);
                    _spriteBatch.Draw(shopBackgroundSprite, storeBackgroundRect, Color.White);

                    if (bobUp)
                    {
                        _spriteBatch.Draw(shopkeeperSprite, new Rectangle(480 * resScale, 68 * resScale, shopkeeperSprite.Width * resScale, shopkeeperSprite.Height * resScale), Color.White);
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBob + 1250)
                        {
                            bobUp = false;
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    } 
                    else {
                        _spriteBatch.Draw(shopkeeperSprite, new Rectangle(480 * resScale, 74 * resScale, shopkeeperSprite.Width * resScale, shopkeeperSprite.Height * resScale), Color.White);
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBob + 1250)
                        {
                            bobUp = true;
                            timeSinceLastBob = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }

                    _spriteBatch.Draw(shopCounterSprite, storeCounterRect, Color.White);
                    for (int i = 0; i < coinSprites.Count; i++)
                    {
                        _spriteBatch.Draw(coinSprite, coinSprites[i], Color.White);
                    }

                    if (!damageHat.isEquipped)
                    {
                        damageHat.position = new Vector2(40 * resScale, 50 * resScale);
                        damageHat.scale = resScale * 5;
                        damageHat.Draw(_spriteBatch);
                    } else
                    {
                        damageHat.position = new Vector2(15 * resScale, 365 * resScale);
                        damageHat.scale = resScale * 5;
                        damageHat.Draw(_spriteBatch);
                    }
                    if (!speedHat.isEquipped)
                    {
                        speedHat.position = new Vector2(120 * resScale, 50 * resScale);
                        speedHat.scale = resScale * 5;
                        speedHat.Draw(_spriteBatch);
                    } else
                    {
                        speedHat.position = new Vector2(15 * resScale, 365 * resScale);
                        speedHat.scale = resScale * 5;
                        speedHat.Draw(_spriteBatch);
                    }
                }
            }
            if (inWinScreen)
            {
                _spriteBatch.Draw(winScreen, new Rectangle(0, 0, winScreen.Width * resScale, winScreen.Height * resScale), Color.White);
            }
            if (debugmode)
            {
                _spriteBatch.Draw(debugIndicator, new Rectangle(685 * resScale, 0, debugIndicator.Width / 2 * resScale, debugIndicator.Height / 2 * resScale), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
