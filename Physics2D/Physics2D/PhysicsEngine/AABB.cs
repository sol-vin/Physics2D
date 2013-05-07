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
        public Vector2 Max;
        public Color Color = Color.White;

        public float Width
        {
            get { return Max.X - Position.X; }
            set { Max.X = value + Position.X;  }
        }


        public float Height
        {
            get { return Max.Y - Position.Y; }
            set { Max.Y = value + Position.Y; }
        }

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

        public AABB(Vector2 position, Vector2 max)
        {
            if(max.X < position.X || max.Y < position.Y) //Sanity check
                throw new Exception("Data is out of bounds! Max cannot be behind position!");
            Position = position;
            Max = max;
        }



        public override void Update(GameTime gt)
        {
            Max += Velocity; //Make sure the back updates!
            base.Update(gt);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Assets.Pixel, BoundingBox, Color);
        }
    }
}
