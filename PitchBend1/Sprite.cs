using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PitchBend1
{
    class Sprite
    {
        Texture2D texture;
        Vector2 position;
        TimeSpan birthTime;
        int fadeStep = 0;
        private static float STEP_TIME = .1f;
        private int LIFE_SPAN = 1;
        static int DEATH_SPAN = 2;

        public Sprite(GameTime gameTime, Texture2D texture, Vector2 position)
        {

            this.texture = texture;

            this.position = position;
            this.birthTime = gameTime.TotalGameTime;
        }

        public void PlayItAgain(TimeSpan deathSpan)
        {

            this.birthTime += deathSpan;
   
        }


        internal void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            if (birthTime.TotalSeconds + LIFE_SPAN < gameTime.TotalGameTime.TotalSeconds)
            {
                //if (birthTime.TotalSeconds + LIFE_SPAN + DEATH_SPAN < gameTime.TotalGameTime.TotalSeconds)
                //{
                //    birthTime = gameTime.TotalGameTime;
                //}

                //else
                //{
                if (fadeStep > 0)
                {
                    fadeStep -= 20;
                }
                else
                {
                    return;
                }
                // }
            }
            else
                if (birthTime > gameTime.TotalGameTime)
                    return;
                else

                    if (gameTime.TotalGameTime.Milliseconds % 100 == 0 && fadeStep < 100)
                    {
                        fadeStep += 20;
                    }

            spriteBatch.Draw(texture, new Rectangle((int)(position.X), (int)(position.Y), 20, 20), new Rectangle(fadeStep, 0, 20, 20), Color.White);
        }

        internal void Update(GameTime gameTime)
        {


        }
    }
}
