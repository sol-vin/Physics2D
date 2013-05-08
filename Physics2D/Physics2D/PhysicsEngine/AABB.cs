using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics2D.PhysicsEngine;

namespace Physics2D.PhysicsEngine
{
    public class AABB : PhysicsObject
    {
        public Color Color = Color.White;

		public float Width;
		public float Height;

		public float Top{ get { return Position.Y; } }
		public float Left{ get { return Position.X; } }
		public float Right{ get { return Position.X + Width; } }
		public float Bottom{ get { return Position.Y + Height; } }

        public new Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); }
            set
            { 
                Position.X = value.X;
                Position.Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

		public AABB(Vector2 position, int width, int height)
        {
            Position = position;
			Width = width;
			Height = height;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Assets.Pixel, BoundingBox, Color);
        }
    }
}
