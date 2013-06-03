using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;



namespace PitchBend1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<int, Vector2> timePitchList;
        private float pitchBend;
        SoundEffectInstance sound;
        List<SoundEffectInstance> sounds = new List<SoundEffectInstance>();

       // private static float VOLUME_DIAL = 5;

        private static float RIGHT_Y_MAGNIFIER = 3;
        private Vector2 window;
        float volume = 0;
        private Texture2D spot;
        List<Sprite> spots = new List<Sprite>();
        private float mousePosY;
        private float mousePosX;
        private TimeSpan startTime = TimeSpan.FromSeconds(0);
        private bool spaceKeyLocked = false;
        TimeSpan deathTime = TimeSpan.Zero;
        private Texture2D cursor;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 880;
            graphics.PreferredBackBufferHeight = 990;
            window = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
           timePitchList = new Dictionary<int,Vector2>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            cursor = Content.Load<Texture2D>(@"cursor");
            spot = Content.Load<Texture2D>("spot");
            sound = Content.Load<SoundEffect>(@"low").CreateInstance();
            sounds.Add(sound);
            sound = Content.Load<SoundEffect>(@"middle").CreateInstance();
            sounds.Add(sound);
            sound = Content.Load<SoundEffect>(@"high").CreateInstance();
            sounds.Add(sound);
            for (int i = 0; i <= 2; i++)
            {
                sounds[i].Play();
                sounds[i].Volume = 0;
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        /// 



        protected override void Update(GameTime gameTime)
        {
            if (null == sounds[2])
                return;

            // Allows the game to exit
float pitch;
            int soundIndex = 0;
         

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                
                {
                    mousePosX = tl.Position.X;
                    mousePosY = tl.Position.Y;
                    // add sparkles based on the touch location
                    //spots.Add(new Sprite(gameTime, spot, new Vector2(tl.Position.X,tl.Position.Y)));

                }
            }
    


            for (int i = 0; i <= 2; i++)
            {
                if (sounds[i].State != SoundState.Playing)
                    sounds[i].Play();
                sounds[i].Volume = 0;
            }

            foreach (Sprite s in spots)
            {
                s.Update(gameTime);
            }
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //if (startTime == TimeSpan.FromSeconds(0))
                //{
                //    spots.Clear();
                //    spaceKeyLocked = false;
                //    startTime = gameTime.TotalGameTime;
                //    deathTime = TimeSpan.Zero;
                //}

               // spots.Add(new Sprite(gameTime, spot, new Vector2(mousePosX, mousePosY)));
               // pitchBend = Math.Max(Math.Min(((mousePosY * -1 + graphics.PreferredBackBufferHeight) / graphics.PreferredBackBufferHeight) * RIGHT_Y_MAGNIFIER, 2.9999f), 0);
               // soundIndex = (int)(pitchBend);
               // pitch= ((pitchBend * 2) % 2) - 1;
               // timePitchList.Add((int)(gameTime.TotalGameTime.TotalMilliseconds), new Vector2(pitchBend,volume));
             
               //sounds[soundIndex].Pitch = pitch;
               // volume = Math.Max(Math.Min((mousePosX)/ (graphics.PreferredBackBufferWidth), 1), 0);
               // sounds[soundIndex].Volume = volume;
            }

           

   if (timePitchList.ContainsKey((int)(gameTime.TotalGameTime.TotalMilliseconds-deathTime.TotalMilliseconds)))
                {
       
                    Vector2 value = timePitchList[(int)(gameTime.TotalGameTime.TotalMilliseconds - deathTime.TotalMilliseconds)];
                    soundIndex = (int)(value.X);
                    pitch = ((value.X * 2) % 2) - 1;
                    sounds[soundIndex].Pitch = pitch;
                    sounds[soundIndex].Volume = value.Y;
                }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !spaceKeyLocked)
            {             
                spaceKeyLocked = true;
              deathTime = gameTime.TotalGameTime - startTime;             
               
             //       if (timePitchList.ContainsKey(gameTime.TotalGameTime + deathTime))              

                foreach (Sprite s in spots)
                {               
                    s.PlayItAgain(deathTime);
                }

                startTime = TimeSpan.FromSeconds(0);

            }
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin();
            // spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            //   spriteBatch.Draw(arrowTexture, new Vector2(window.X * volume, (pitchBend) * -1 * window.Y / RIGHT_Y_MAGNIFIER + window.Y - arrowTexture.Height/2), Color.White);
            foreach (Sprite s in spots)
            {
                s.Draw(gameTime, spriteBatch);
            }
            spriteBatch.Draw(cursor, new Vector2(mousePosX-cursor.Width/3, mousePosY-cursor.Height/3f), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }




    }
}
