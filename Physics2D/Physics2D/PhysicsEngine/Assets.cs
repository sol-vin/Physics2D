using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics2D.PhysicsEngine
{
    public class Assets
    {
        public static Texture2D Pixel;
        public static Texture2D Circle;

        public static void LoadContent(Game game)
        {
            Pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            Pixel.SetData(new []{Color.White});

            Circle = game.Content.Load<Texture2D>("circle");
        }
    }
}
