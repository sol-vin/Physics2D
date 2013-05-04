using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics2D.PhysicsEngine;

namespace Physics2D.PhysicsEngine
{
    public class AABB
    {
        public Vector2 Min;
        public Vector2 Max;
        public Color Color = Color.White;

        public float Width
        {
            get { return Max.X - Min.X; }
            set { Max.X = value + Min.X;  }
        }


        public float Height
        {
            get { return Max.Y - Min.Y; }
            set { Max.Y = value + Min.Y; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Min.X, (int)Min.Y, (int)Width, (int)Height); }
            set
            { 
                Min.X = value.X;
                Min.Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        public AABB(Vector2 min, Vector2 max)
        {
            if(max.X < min.X || max.Y < min.Y) //Sanity check
                throw new Exception("Data is out of bounds!");
            Min = min;
            Max = max;
        }

        public bool TestCollision(AABB aabb)
        {
            if (Max.X > aabb.Min.X || Min.X > aabb.Max.X)
                return true;
            if (Max.Y < aabb.Min.Y || Min.Y > aabb.Max.Y)
                return false;
            return true;
        }

        public static bool AABBvsAABB(AABB a, AABB b)
        {
            if(a.Max.X > b.Min.X || a.Min.X > b.Max.X)
                return false;
            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y) 
                return false;
            return true;
        }

        public void Update(GameTime gt)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Assets.Pixel, Rectangle, Color);
        }
    }
}
