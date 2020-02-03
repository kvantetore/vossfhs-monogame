using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Astroids
{
    public class Bullet
    {
        Game1 game;
        DateTime Created;

        private Texture2D Sprite;
        public Vector2 Position;
        public Vector2 Velocity = new Vector2(0, 0);
        public float Angle;

        public Bullet(Game1 g)
        {
            game = g;
            Created = DateTime.Now;
        }

        public void Load(ContentManager c)
        {
            Sprite = c.Load<Texture2D>("bullet");
        }

        public void Update(GameTime gameTime)
        {
            float timeStep = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * timeStep;

            if (Created + TimeSpan.FromSeconds(2) < DateTime.Now)
            {
                game.RemoveBullet(this);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            Vector2 center = new Vector2(Sprite.Width / 2, Sprite.Height / 2);

            spriteBatch.Draw(Sprite, Position, sourceRect, Color.White, Angle, center, 1f, SpriteEffects.None, 0);
        }

    }
}
