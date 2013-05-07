using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Physics2D.PhysicsEngine
{
    public struct Manifold
    {
        public PhysicsObject A, B;
        public float PenetrationDepth;
        public Vector2 Normal;
        public bool AreColliding;
    }
}
