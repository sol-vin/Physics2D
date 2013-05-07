using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics2D.PhysicsEngine
{
    public class Circle : PhysicsObject
    {
        public float Radius;
        public Color Color = Color.White;
        public new Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Radius * 2,
                (int)Radius * 2
                );
            }
        }

        public float Diameter
        {
            get { return Radius*2f; }
            set { Radius = value/2f; }
        }

        public Circle(Vector2 pos, float radius)
        {
            if(radius <= 0) throw new Exception("Radius cannot be 0 or negative");
            Radius = radius;
            Position = pos;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Assets.Circle, BoundingBox, Color);
        }
    }
}
