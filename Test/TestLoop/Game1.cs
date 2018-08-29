using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestLoop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;

        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            GraphicsUtility.Initialize(this);

            base.Initialize();

            player = new Player();
            player.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /* protected override void LoadContent()
        {
            // replaced by lazy loading
        }
        */

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            GamePadState gstate = GamePad.GetState(PlayerIndex.One);

            if (gstate.Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.ProcessUserInput(kstate, gstate);

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
