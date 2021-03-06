﻿using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConsoleMonogame.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        String text = "Type something in the console";
        Thread thread;
        SpriteFont defaultFont;
        Texture2D gear;
        Texture2D eye;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
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

            base.Initialize();

            thread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Your command?");
                    this.text = Console.ReadLine();
                }
            });

            thread.Start();
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

            defaultFont = Content.Load<SpriteFont>("DefaultFont");
            eye = Content.Load<Texture2D>("eye");
            gear = Content.Load<Texture2D>("gear");
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

            // TODO: Add your update logic here

            var mouse = Mouse.GetState();
            if (
                mouse.LeftButton == ButtonState.Pressed && 
                mouse.X >= 32 && mouse.Y >= 32 && 
                mouse.X <= 32 + 256 && mouse.Y <= 288 + 256)
            {
                Console.WriteLine($"You  clicked on the {(mouse.Y <= 288 ? "eye" : "gear")}!");
            }

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

            base.Draw(gameTime);

            this.spriteBatch.Begin();

            this.spriteBatch.Draw(this.eye, new Vector2(32, 32), Color.White);
            this.spriteBatch.Draw(this.gear, new Vector2(32, 288), Color.White);

            if (this.text != null)
            {
                spriteBatch.DrawString(this.defaultFont, this.text, Vector2.Zero, Color.Black);

                if (this.text == "quit")
                {
                    this.thread.Abort();
                    Environment.Exit(0);
                }
            }

            this.spriteBatch.End();
        }
    }
}
