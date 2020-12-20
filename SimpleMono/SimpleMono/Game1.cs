using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SimpleMono.GraphicsSupport;

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
        
        // 1080p
        //const int windowWidth = 1920;
        //const int windowHeight = 1080;
        // 720p
        const int windowWidth = 1280;
        const int windowHeight = 720;

        // Support for loading and drawing the JPG image
        //Texture2D mJPGImage;     // The UWB-JPG.jpg image to be loaded
        //Vector2 mJPGPosition;    // Top-Left pixel position of UWB-JPG.jpg

        // Support for loading and drawing of the PNG image
        //Texture2D mPNGImage;     // The UWB-PNG.png image to be loaded
        //Vector2 mPNGPosition;    // Top-Left pixel position of UWB-PNG.png

        const int numObjects = 2;
        // Work with the TexturedPrimitive class
        TexturedPrimitive[] graphicsObjects; // An array of objects
        int currentIndex = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Game1.gameContent = Content;

            // Set preferred window size
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.IsFullScreen = false;
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
            // TODO: Add your initialization logic here
            // Initialize the initial image positions.
            //mJPGPosition = new Vector2(10f, 10f);
            //mPNGPosition = new Vector2(100f, 100f);



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
            // Load the images.
            //mJPGImage = Content.Load<Texture2D>("mario_static");
            //mPNGImage = Content.Load<Texture2D>("goomba_static2");
            // TODO: use this.Content to load your game content here

            // Define camera window bounds
            Camera.SetCameraWindow(new Vector2(0f, 0f), 100f);

            // Create the primitives.
            graphicsObjects = new TexturedPrimitive[numObjects];

            graphicsObjects[0] = new TexturedPrimitive("mario_static",           // Image file name
                                                        new Vector2(15, 15),     // Position to draw
                                                        new Vector2(30, 30));    // Size to draw
            graphicsObjects[1] = new TexturedPrimitive("goomba_static2",
                                                        new Vector2(60, 60),
                                                        new Vector2(30, 30));
            
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

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
            //mJPGPosition += InputWrapper.ThumbSticks.Left;
            //mPNGPosition += InputWrapper.ThumbSticks.Right;


            // Button X to select the next object to work with
            if (InputWrapper.Buttons.X == ButtonState.Pressed)
                currentIndex = (currentIndex + 1) % numObjects;

            // Update currently working object with thumb sticks.
            graphicsObjects[currentIndex].Update(
                InputWrapper.ThumbSticks.Left,
                InputWrapper.ThumbSticks.Right);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(); // Initialize drawing support

            // Loop over and draw each primitive
            foreach (TexturedPrimitive p in graphicsObjects)
            {
                p.Draw();
            }

            // Print out text message to echo status
            FontSupport.PrintStatus("0x0000ABC Hackers Use This Font", Color.Yellow);
            FontSupport.PrintStatusAt(graphicsObjects[0].GetPosition(), "// I AM MARIO //", Color.White);

            spriteBatch.End(); // Inform graphics system we are done drawing

            base.Draw(gameTime);
        }
    }
}
