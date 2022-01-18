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
        bool isClicking = false;
        Point mousePos;
        bool clicked;

        //Declaring Backgrounds
        Texture2D mainMenuSprite;
        Texture2D controlsScreen;
        Texture2D mapSprite;
        
        //Declaring Buttons
        Texture2D oneButton;
        Texture2D twoButton;
        Texture2D coopButton;
        Texture2D singleplayerButton;
        Texture2D exitButton;
        Texture2D settingsButton;

        //Declaring Players
        Texture2D coopMapSprite;
        Texture2D playerOneSprite;
        Texture2D playerOneDodgeSprite;
        Texture2D playerTwoSprite;
        Texture2D playerTwoDodgeSprite;
        Texture2D graveStoneSprite;
        Texture2D playerOneFlipped;

        // Declaring misc
        Texture2D mountainSprite;

        //Listing stuff
        List<Mountain> mountains = new List<Mountain>();

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
        bool isMoving = false;
        int mapSpeed = 1;
        bool movingup;
        bool movingright;

        float distanceToTravelX;
        float distanceToTravelY;
        float distanceToTravelTotal;
        float movementX;
        float movementY;

        // Position & walking stuff
        double redTimeSinceLastWalked = 0;
        double blueTimeSinceLastWalked = 0;
        int redWalkSoundDelay = 400; // These are used to make walk sounds better
        int blueWalkSoundDelay = 400; // They are kept seperate so they can speed up if the speed powerup is picked up

        // Dodging stuff
        int redDodgeDelay = 1200; // Red has 1.2 seconds of time between dodges, same as blue
        int redInvulnTime = 500; // Only 500ms of invincibility
        double redtimeSinceLastDodge = 0;
        double redInvulnTimer = 0; // These are used in the dodge timer

        int blueDodgeDelay = 1200; // All the same as red, however kept seperate so the dodge powerup can only effect the one who picked it up
        int blueInvulnTime = 500;
        double bluetimeSinceLastDodge = 0;
        double blueInvulnTimer = 0;

        // Firing Stuff
        bool redfireDelay = false; // booleans used to determine whether or not red or blue can fire
        bool redIsDodging = false; // This is used to stop dodge spam

        bool bluefireDelay = false;
        bool blueIsDodging = false;

        // Declaring rectangles so I can use them in collisions
        public Rectangle redguyMapRect;
        public Rectangle coopMapRect;
        public Rectangle redguyRect;
        public Rectangle blueguyRect;
        public Rectangle mouseRect;
        public Rectangle singleplayerButtonRect;
        public Rectangle coopButtonRect;
        public Rectangle settingsButtonRect;
        public Rectangle exitButtonRect;

        public Rectangle plainsRect = new Rectangle(400, 0, 300, 345);
        public Rectangle snowRect= new Rectangle(100, 0, 290, 265);
        public Rectangle desertRect = new Rectangle(100, 250, 290, 200);
        public Rectangle swampRect;

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
            int windowTitleThing = new Random().Next(1, 11);
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
            mapSprite = Content.Load<Texture2D>("Backgrounds/mapwip");

            //Buttons
            oneButton = Content.Load<Texture2D>("Items/1xbutton");
            twoButton = Content.Load<Texture2D>("Items/2xbutton");
            coopButton = Content.Load<Texture2D>("Items/coopbutton");
            exitButton = Content.Load<Texture2D>("Items/exitbutton");
            singleplayerButton = Content.Load<Texture2D>("Items/singleplayerbutton");
            settingsButton = Content.Load<Texture2D>("Items/settingsbutton");

            //Players
            coopMapSprite = Content.Load<Texture2D>("Players/CoopGuys");
            playerOneSprite = Content.Load<Texture2D>("Players/Redguy");
            playerTwoSprite = Content.Load<Texture2D>("Players/Blueguy");
            playerOneDodgeSprite = Content.Load<Texture2D>("Players/Redguydodgelarge");
            playerTwoDodgeSprite = Content.Load<Texture2D>("Players/Blueguydodgelarge");
            graveStoneSprite = Content.Load<Texture2D>("Players/Gravestone");
            playerOneFlipped = Content.Load<Texture2D>("Players/redguyflip");

            //Misc
            mountainSprite = Content.Load<Texture2D>("Scenery/Mountain");
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            clicked = mouseState.LeftButton == ButtonState.Pressed;

            if (mouseState.LeftButton == ButtonState.Released)
            {
                isClicking = false;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                Debug.WriteLine(mouseState.X + "," + mouseState.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                if (currentRes == 1 && escapeKeyWasPressed == false)
                {
                    escapeKeyWasPressed = true;
                    resScale = 2;
                    plainsRect = new Rectangle(400 * resScale, 0 * resScale, 300 * resScale, 345 * resScale);
                    snowRect = new Rectangle(100 * resScale, 0 * resScale, 290 * resScale, 265 * resScale);
                    desertRect = new Rectangle(100 * resScale, 250 * resScale, 290 * resScale, 200 * resScale);
                    mountains.Clear();
                    SpawnScenery();
                    _graphics.PreferredBackBufferWidth = 1600;
                    _graphics.PreferredBackBufferHeight = 960;
                    _graphics.ApplyChanges();
                    currentRes = 2;
                    mapPos.X = mapPos.X * resScale;
                    mapPos.Y = mapPos.Y * resScale;
                }
                else if (currentRes == 2 && escapeKeyWasPressed == false)
                {
                    escapeKeyWasPressed = true;
                    resScale = 1;
                    plainsRect = new Rectangle(400 * resScale, 0 * resScale, 300 * resScale, 345 * resScale);
                    snowRect = new Rectangle(100 * resScale, 0 * resScale, 290 * resScale, 265 * resScale);
                    desertRect = new Rectangle(100 * resScale, 250 * resScale, 290 * resScale, 200 * resScale);
                    mountains.Clear();
                    SpawnScenery();
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 480;
                    _graphics.ApplyChanges();
                    currentRes = 1;
                    mapPos.X = mapPos.X / 2;
                    mapPos.Y = mapPos.Y / 2;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F4))
            {
                escapeKeyWasPressed = false;
            }

            if(isInMainMenu == true)
            {
                if (singleplayerButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    SpawnScenery();
                    mapPos = new Vector2(385 * resScale, 245 * resScale);
                    isInMainMenu = false;
                    gameHasStarted = true;
                    inWorldMap = true;
                    inCoop = false;
                    inSingleplayer = true;
                    // Put some cutscene shit here
                }
                else if (coopButtonRect.Intersects(mouseRect) && clicked && isClicking == false)
                {
                    isClicking = true;
                    mapPos = new Vector2(385 * resScale, 245 * resScale);
                    isInMainMenu = false;
                    gameHasStarted = true;
                    inWorldMap = true;
                    inSingleplayer = false;
                    inCoop = true;
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
                if (inSingleplayer)
                {
                    PlayerMapMove(gameTime);
                    if (plainsRect.Contains(redguyMapRect))
                    {
                        //Debug.WriteLine("In plains");
                    } else if (snowRect.Contains(redguyMapRect))
                    {
                        //Debug.WriteLine("In snow");
                    } else if (desertRect.Contains(redguyMapRect))
                    {
                        //Debug.WriteLine("In desert");
                    }
                }
            }

            base.Update(gameTime);
        }

        void PlayerMapMove(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            if (clicked && !isClicking)
            {
                isClicking = true;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    targetPos.X = mousePos.X - 7 * resScale;
                    targetPos.Y = mousePos.Y - 13 * resScale;

                    distanceToTravelX = targetPos.X - mapPos.X;
                    distanceToTravelY = targetPos.Y - mapPos.Y;
                    distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                    movementX = distanceToTravelX / distanceToTravelTotal;
                    movementY = distanceToTravelY / distanceToTravelTotal;

                    isMoving = true;
                }
            }
            if (isMoving)
            {
                if (movementX > 0)
                {
                    movingright = true;
                    //Debug.WriteLine("Moving Right");
                } else if (movementX < 0)
                {
                    movingright = false;
                    //Debug.WriteLine("Moving Left");
                }
                if (movementY > 0)
                {
                    movingup = false;
                    //Debug.WriteLine("Moving Down");
                } else if (movementY < 0)
                {
                    movingup = true;
                    //Debug.WriteLine("Moving Up");
                }
                for (int i = 0; i < mountains.Count; i++)
                {
                    if (redguyMapRect.Intersects(mountains[i].mountainRect))
                    {
                        switch (movingup) {
                            case true:
                                mapPos.Y += (mapSpeed + 1) * resScale;
                                break;
                            case false:
                                mapPos.Y -= (mapSpeed + 1) * resScale;
                                break;
                        }
                        switch (movingright)
                        {
                            case true:
                                mapPos.X -= (mapSpeed + 1) * resScale;
                                break;
                            case false:
                                mapPos.X += (mapSpeed + 1) * resScale;
                                break;
                        }
                        isMoving = false;
                    }
                }
                mapPos.X += movementX * (mapSpeed * resScale);
                mapPos.Y += movementY * (mapSpeed * resScale);
                Vector2 mapPosRound = new Vector2((float)Math.Round(mapPos.X), (float)Math.Round(mapPos.Y));
                if (targetPos == mapPosRound)
                {
                    isMoving = false;
                }
            }
        }

        // That scenery....
        void SpawnScenery()
        {
            // Mountains
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(295 * resScale), (int)(7 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(305 * resScale), (int)(28 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(300 * resScale), (int)(46 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(312 * resScale), (int)(68 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(325 * resScale), (int)(101 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(340 * resScale), (int)(133 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(322 * resScale), (int)(143 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(569 * resScale), (int)(285 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(516 * resScale), (int)(289 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(545 * resScale), (int)(292 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(493 * resScale), (int)(300 * resScale)), resScale));
            mountains.Add(new Mountain(mountainSprite, new Vector2((int)(468 * resScale), (int)(307 * resScale)), resScale));
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
            //Map Drawing
            Rectangle mapRect = new Rectangle(0 * resScale, 0 * resScale, mapSprite.Width / 2 * resScale, mapSprite.Height / 2 * resScale);
            redguyMapRect = new Rectangle((int)mapPos.X, (int)mapPos.Y, playerOneSprite.Width * resScale, playerOneSprite.Height * resScale);
            if (gameHasStarted)
            {
                if (inWorldMap)
                {
                    // Map sprite here
                    _spriteBatch.Draw(mapSprite, mapRect, Color.White);
                    // Mountains
                    for (int i = 0; i < mountains.Count; i++)
                    {
                        mountains[i].Draw(_spriteBatch);
                    }
                    // Buildings / Scenery
                    if (inSingleplayer)
                    {
                        if (mousePos.X < mapPos.X + 7)
                        {
                            _spriteBatch.Draw(playerOneFlipped, redguyMapRect, Color.White);
                        }
                        else
                        {
                            _spriteBatch.Draw(playerOneSprite, redguyMapRect, Color.White);
                        }
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
