using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Physics2D.PhysicsEngine;

namespace Physics2D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		private AABB _aabb3;
		private Circle _circle3;

        private Color bgcolor;

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
            // TODO: Add your initialization logic here

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
            Assets.LoadContent(this);

			//AABB vs Circle
			_aabb3 = new AABB(new Vector2(60,140), 10, 10);
            _aabb3.Color = Color.Yellow;
			_aabb3.Velocity = Vector2.UnitX * .5f; 
            _aabb3.Mass = 10;
            _aabb3.Restitution = .5f;

			_circle3 = new Circle(new Vector2(300,148), 10);
            _circle3.Color = Color.Chartreuse;
			_circle3.Velocity = -Vector2.UnitX * .5f; 
            _circle3.Mass = 10;
            _circle3.Restitution = .5f;
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
        protected override void Update (GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit ();

			_aabb3.Update (gameTime);
			_circle3.Update (gameTime);

			Manifold m = new Manifold ();
			bgcolor = Collision.AABBvsCircle (_aabb3, _circle3, ref m) ? Color.Green : Color.DarkRed;
			if (m.AreColliding) {
				m = new Manifold();
				Collision.AABBvsCircle (_aabb3, _circle3, ref m);
				Collision.ResolveCollision (m);
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgcolor);

            spriteBatch.Begin();
			_aabb3.Draw(spriteBatch);
			_circle3.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
