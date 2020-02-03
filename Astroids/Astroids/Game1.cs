using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Astroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color backgroundColor = Color.CornflowerBlue;
        Texture2D background;
        Texture2D rocket;
        SpriteFont arial;

        List<Bullet> bullets;

        float rocketAngularVelocity;
        float rocketAngle;

        Vector2 rocketPosition;
        Vector2 rocketVelocity;


        float currentFps = 0;


        float maxSpeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromSeconds(1f / 1000f);
            bullets = new List<Bullet>();
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
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            var posX = graphics.PreferredBackBufferWidth / 2;
            var posY = graphics.PreferredBackBufferHeight / 2;
            rocketPosition = new Vector2(posX, posY);

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
            background = Content.Load<Texture2D>("stars");
            rocket = Content.Load<Texture2D>("rocket_128");
            arial = Content.Load<SpriteFont>("arial");
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
            currentFps = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var timeStep = (float)gameTime.ElapsedGameTime.TotalSeconds;


            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.G))
            {
                backgroundColor = Color.Green;
            }

            if (keyboard.IsKeyDown(Keys.R))
            {
                backgroundColor = Color.Red;
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                rocketAngularVelocity -= 10f * timeStep;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                rocketAngularVelocity += 10f * timeStep;
            }

            Vector2 forward = new Vector2((float)Math.Sin(rocketAngle), -(float)Math.Cos(rocketAngle));
            if (keyboard.IsKeyDown(Keys.Up))
            {
                rocketVelocity += forward * 100f * timeStep;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                rocketVelocity -= forward * 100f * timeStep;
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                Bullet currentBullet = new Bullet(this);
                currentBullet.Load(Content);

                currentBullet.Angle = rocketAngle;
                currentBullet.Velocity = forward * 200f;
                currentBullet.Position = rocketPosition + forward * rocket.Width / 2;

                bullets.Add(currentBullet);
            }

            rocketAngle += rocketAngularVelocity * timeStep;
            rocketPosition += rocketVelocity * timeStep;

            float speed = (float)Math.Sqrt((rocketVelocity.X * rocketVelocity.X) + (rocketVelocity.Y * rocketVelocity.Y));
            if (speed > maxSpeed)
            {
                maxSpeed = speed;
            }

            foreach (var bullet in bullets.ToArray())
            {
                bullet.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void RemoveBullet(Bullet b)
        {
            bullets.Remove(b);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            var screenWidth = graphics.PreferredBackBufferWidth;
            var screenHeight = graphics.PreferredBackBufferHeight;
            spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            Rectangle sourceRect = new Rectangle(0, 0, rocket.Width, rocket.Height);
            Vector2 center = new Vector2(rocket.Width / 2, rocket.Height / 2);

            spriteBatch.Draw(rocket, rocketPosition, sourceRect, Color.White, rocketAngle, center, 1f, SpriteEffects.None, 0);

            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            spriteBatch.DrawString(arial, $"Fps: {currentFps:0}", new Vector2(0, 0), Color.Green);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
