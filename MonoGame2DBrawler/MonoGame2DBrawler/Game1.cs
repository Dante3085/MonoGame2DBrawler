using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using VosSoft.Xna.GameConsole;

using MonoGame2DBrawler.Sprites;
using MonoGame2DBrawler.Input;

namespace MonoGame2DBrawler
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static GameConsole gameConsole;

        private List<AnimatedSprite> animSprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            gameConsole = new GameConsole(this, "german", Content);

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

            // TODO: use this.Content to load your game content here
            Texture2D playerSheet = Content.Load<Texture2D>("playerSheet");

            animSprites = new List<AnimatedSprite>()
            {
                new AnimatedSprite("Player 1", new Vector2(100, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 2", new Vector2(200, 100), PlayerIndex.Two, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(300, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(400, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(500, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(600, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(700, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(800, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(900, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
                new AnimatedSprite("Player 3", new Vector2(1000, 100), PlayerIndex.One, GraphicsDevice, playerSheet),
            };

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                gameConsole.Open(Keys.Tab);

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                foreach (AnimatedSprite s in animSprites)
                    s.PDrawBoundingBox = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                foreach (AnimatedSprite s in animSprites)
                    s.PDrawBoundingBox = false;
            }

            // TODO: Add your update logic here

            foreach (AnimatedSprite s in animSprites)
                s.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (AnimatedSprite s in animSprites)
                s.Draw(spriteBatch, animSprites);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
