using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SimpleMono.GraphicsSupport;
using System.Collections.Generic;
using System.Linq;

namespace SimpleMono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public GraphicsDeviceManager graphics;
        static public ContentManager gameContent;
        static public SpriteBatch spriteBatch;
        
        const int windowWidth = 1920;
        const int windowHeight = 1080;

        const int numObjects = 2;        
        TexturedPrimitive[] graphicsObjects; // An array of objects
        int currentIndex = 0;
        SpritePrimitive pacman;

        private FrameCounter frameCounter = new FrameCounter();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Game1.gameContent = Content;

            // Set preferred window size
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);            

            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(0f, 0f), 100f, 100f);

            // Create the primitives.
            graphicsObjects = new TexturedPrimitive[numObjects];

            graphicsObjects[0] = new TexturedPrimitive("pacman_1",           // Image file name
                                                        new Vector2(15, 15),     // Position to draw
                                                        new Vector2(30, 30));    // Size to draw
            graphicsObjects[1] = new TexturedPrimitive("goomba_static2",
                                                        new Vector2(60, 60),
                                                        new Vector2(30, 30));

            pacman = new SpritePrimitive("pacman_1",
                                                          new Vector2(20, 30), new Vector2(10, 10),
                                                          4,
                                                          4,
                                                          0);
            pacman.SetSpriteAnimation(0, 0, 0, 3, 5);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            pacman.Update(InputWrapper.ThumbSticks.Left, InputWrapper.ThumbSticks.Right);

            // "A" to toggle to full-screen mode
            if (InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                if (!graphics.IsFullScreen)
                {
                    graphics.IsFullScreen = true;
                    graphics.ApplyChanges();
                }
            }

            // "B" toggles back to windowed mode
            if (InputWrapper.Buttons.B == ButtonState.Pressed)
            {
                if (graphics.IsFullScreen)
                {
                    graphics.IsFullScreen = false;
                    graphics.ApplyChanges();
                }
            }

            // Allows the game to exit
            if (InputWrapper.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Update the image positions with left/right thumbsticks

            // Button X to select the next object to work with
            //if (InputWrapper.Buttons.X == ButtonState.Pressed)
            //    currentIndex = (currentIndex + 1) % numObjects;

            base.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Update(deltaTime);            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(); // Initialize drawing support

            // Loop over and draw each primitive
            //foreach (TexturedPrimitive p in graphicsObjects)
            //{
            //    p.Draw();
            //}

            pacman.Draw();

            // Print out text message to echo status
            FontSupport.PrintStatus("0x0000ABC Hackers Use This Font", Color.Yellow);            

            Vector2 lowerRight;
            lowerRight.X = 90;
            lowerRight.Y = 96;
            FontSupport.PrintStatusAt(lowerRight, string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond), Color.LimeGreen);

            spriteBatch.End(); // Inform graphics system we are done drawing

            base.Draw(gameTime);
        }
    }

    public class FrameCounter
    {
        public FrameCounter()
        {
        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);                
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
