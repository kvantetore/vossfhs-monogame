using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Astroids
{
    class Bullet
    {
        private Texture2D Sprite;
        public Vector2 Position;
        public Vector2 Velocity = new Vector2(0, 0);
        public float Angle;

        public void Load(ContentManager c)
        {
            Sprite = c.Load<Texture2D>("bullet");
        }

        public void Update(GameTime gameTime)
        {
            float timeStep = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * timeStep;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            Vector2 center = new Vector2(Sprite.Width / 2, Sprite.Height / 2);

            spriteBatch.Draw(Sprite, Position, sourceRect, Color.White, Angle, center, 1f, SpriteEffects.None, 0);
        }

    }
}
