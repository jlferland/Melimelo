using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestLoop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GameEnvironment env;

        public Game1()
        {
            new GraphicsDeviceManager(this);
            env = new GameEnvironment(this);
            Content.RootDirectory = "Content";
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
            GraphicsUtility.Initialize(this);

            TestGameArea area = new TestGameArea();
            env.AddGameArea(area);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            env.ClearUnmanagedContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            env.Update(gameTime);

            // TODO: remove this eventually
            if (InputHandler.CurrentC1State.Buttons.Back == ButtonState.Pressed || 
                InputHandler.CurrentKState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsUtility.Draw();

            base.Draw(gameTime);
        }
    }
}
