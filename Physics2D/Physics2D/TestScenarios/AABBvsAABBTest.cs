using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics2D.PhysicsEngine;

namespace Physics2D.TestScenarios
{
	public class AABBvsAABBTest
	{
		private AABB _aabb1, _aabb2;
		private AABB _aabb3, _aabb4; 
		private AABB _aabb5, _aabb6;

		public AABBvsAABBTest ()
		{
						//AABB vs AABB X
			_aabb1 = new AABB(new Vector2(300,20), 20, 20);
            _aabb1.Color = Color.Chocolate;
			_aabb1.Velocity = Vector2.UnitY * .5f; 
            _aabb1.Mass = 10;
            _aabb1.Restitution = .5f;

			_aabb2 = new AABB(new Vector2(300,140), 20, 20);
            _aabb2.Color = Color.CadetBlue;
			_aabb2.Velocity = -Vector2.UnitY * .7f; 
            _aabb2.Mass = 30;
            _aabb2.Restitution = .5f;

			//AABB vs AABB Y
			_aabb3 = new AABB(new Vector2(50,200), 20, 20);
            _aabb3.Color = Color.Chocolate;
			_aabb3.Velocity = Vector2.UnitX * .5f; 
            _aabb3.Mass = 10;
            _aabb3.Restitution = .5f;

			_aabb4 = new AABB(new Vector2(150,200), 20, 20);
            _aabb4.Color = Color.CadetBlue;
			_aabb4.Velocity = -Vector2.UnitX * .7f; 
            _aabb4.Mass = 30;
            _aabb4.Restitution = .5f;

			//AABB Corner Test
			_aabb5 = new AABB(new Vector2(110,100), 20, 20);
            _aabb5.Color = Color.Chocolate;
			_aabb5.Velocity = Vector2.One * .5f; 
            _aabb5.Mass = 10;
            _aabb5.Restitution = .5f;

			_aabb6 = new AABB(new Vector2(300,300), 20, 20);
            _aabb6.Color = Color.CadetBlue;
			_aabb6.Velocity = -Vector2.One * .7f; 
            _aabb6.Mass = 30;
            _aabb6.Restitution = .5f;
		}

		public void Update(GameTime gameTime)
		{
			_aabb1.Update (gameTime);
			_aabb2.Update (gameTime);
			
			Manifold m = new Manifold ();
			//bgcolor = Collision.TestAABBvsAABB(_aabb1, _aabb2, ref m) ? Color.Green : Color.Brown;
			Collision.TestAABBvsAABB(_aabb1, _aabb2, ref m);
			if (m.AreColliding) {
				m = new Manifold();
				Collision.TestAABBvsAABB (_aabb1, _aabb2, ref m);
				Collision.ResolveCollision (m);
				Collision.PositionalCorrection(m);
			}

			_aabb3.Update(gameTime);
			_aabb4.Update(gameTime);
			m = new Manifold();

			Collision.TestAABBvsAABB(_aabb3, _aabb4, ref m);
			if (m.AreColliding) {
				m = new Manifold();
				Collision.TestAABBvsAABB (_aabb3, _aabb4, ref m);
				Collision.ResolveCollision (m);
				Collision.PositionalCorrection(m);
			}

			_aabb5.Update(gameTime);
			_aabb6.Update(gameTime);
			m = new Manifold();

			Collision.TestAABBvsAABB(_aabb5, _aabb6, ref m);
			if (m.AreColliding) {
				m = new Manifold();
				Collision.TestAABBvsAABB (_aabb5, _aabb6, ref m);
				Collision.ResolveCollision (m);
				Collision.PositionalCorrection(m);
			}

		}

		public void Draw(SpriteBatch sb)
		{
			_aabb1.Draw(sb);
			_aabb2.Draw(sb);

			_aabb3.Draw(sb);
			_aabb4.Draw(sb);

			_aabb5.Draw(sb);
			_aabb6.Draw(sb);
		}
	}
}