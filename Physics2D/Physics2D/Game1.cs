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
		private AABB _aabb1, _aabb2;
		private AABB _aabb3;
		private Circle _circle1;

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
			_aabb1 = new AABB(new Vector2(300,20), 20, 20);
            _aabb1.Color = Color.Chocolate;
			_aabb1.Velocity = Vector2.UnitY * .5f; 
            _aabb1.Mass = 10;
            _aabb1.Restitution = .5f;

			_aabb2 = new AABB(new Vector2(300,140), 20, 20);
            _aabb2.Color = Color.CadetBlue;
			_aabb2.Velocity = -Vector2.UnitY * .7f; 
            _aabb2.Mass = 10;
            _aabb2.Restitution = .5f;

			_aabb3 = new AABB(new Vector2(300,200), 20, 20);
            _aabb3.Color = Color.CadetBlue;
			_aabb3.Velocity = -Vector2.UnitX * .5f; 
            _aabb3.Mass = 10;
            _aabb3.Restitution = .5f;

			_circle1 = new Circle(new Vector2(60,203), 10);
            _circle1.Color = Color.Chocolate;
			_circle1.Velocity = Vector2.UnitX * .5f; 
            _circle1.Mass = 10;
            _circle1.Restitution = .5f;
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

			_aabb1.Update (gameTime);
			_aabb2.Update (gameTime);

			_aabb3.Update(gameTime);
			_circle1.Update(gameTime);
			
			Manifold m = new Manifold ();
			bgcolor = Collision.TestAABBvsAABB(_aabb2, _aabb1, ref m) ? Color.Green : Color.DarkRed;
			if (m.AreColliding) {
				m = new Manifold();
				Collision.TestAABBvsAABB (_aabb2, _aabb1, ref m);
				Collision.ResolveCollision (m);
				Collision.PositionalCorrection(m);
			}

			//TODO: Problems with AABBvsCircle Collision
//			m = new Manifold ();
//			bgcolor = Collision.AABBvsCircle(_aabb3, _circle1, ref m) ? Color.Green : Color.DarkRed;
//			if (m.AreColliding) {
//				m = new Manifold();
//				Collision.AABBvsCircle (_aabb3, _circle1, ref m);
//				Collision.ResolveCollision (m);
//			}5

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
			_aabb1.Draw(spriteBatch);
			_aabb2.Draw(spriteBatch);

			_aabb3.Draw(spriteBatch);
			_circle1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
