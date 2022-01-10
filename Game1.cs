using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace TheGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int currentRes = 1;
        int resScale = 1;
        bool escapeKeyWasPressed = false;
        bool isClicking = false;
        Point mousePos;

        //Declaring Backgrounds
        Texture2D mainMenuSprite;
        Texture2D controlsScreen;
        
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

        // Main menu stuff
        bool isInMainMenu = true;

        // In game stuff
        bool gameHasStarted = false;
        bool inWorldMap = false;
        bool inCombat = false;
        bool inCoop = false;
        bool inSingleplayer = false;

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
        public Rectangle redguyRect;
        public Rectangle blueguyRect;
        public Rectangle mouseRect;
        public Rectangle singleplayerButtonRect;
        public Rectangle coopButtonRect;
        public Rectangle settingsButtonRect;
        public Rectangle exitButtonRect;

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
            int windowTitleThing = new Random().Next(1, 5);
            switch (windowTitleThing)
            {
                case 1:
                    Window.Title = "The greatest sequel ever";
                    break;
                case 2:
                    Window.Title = "2playershootergame best game 2030";
                    break;
                case 3:
                    Window.Title = "a glockenspiel is an instrument though";
                    break;
                case 4:
                    Window.Title = "incredible";
                    break;
            }
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            //Backgrounds
            mainMenuSprite = Content.Load<Texture2D>("Backgrounds/mainmenu");
            controlsScreen = Content.Load<Texture2D>("Backgrounds/Controlsscreen");

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            bool clicked = mouseState.LeftButton == ButtonState.Pressed;

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
                    _graphics.PreferredBackBufferWidth = 1600;
                    _graphics.PreferredBackBufferHeight = 960;
                    _graphics.ApplyChanges();
                    currentRes = 2;
                }
                else if (currentRes == 2 && escapeKeyWasPressed == false)
                {
                    escapeKeyWasPressed = true;
                    resScale = 1;
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 480;
                    _graphics.ApplyChanges();
                    currentRes = 1;
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

            base.Update(gameTime);
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

            if (isInMainMenu == true)
            {
                _spriteBatch.Draw(mainMenuSprite, mainmenuRect, Color.White);
                _spriteBatch.Draw(singleplayerButton, singleplayerButtonRect, Color.White);
                _spriteBatch.Draw(coopButton, coopButtonRect, Color.White);
                _spriteBatch.Draw(settingsButton, settingsButtonRect, Color.White);
                _spriteBatch.Draw(exitButton, exitButtonRect, Color.White);
                _spriteBatch.Draw(oneButton, mouseRect, Color.Transparent);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
