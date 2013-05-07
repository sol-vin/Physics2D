using System;
using Microsoft.Xna.Framework;

namespace Physics2D.PhysicsEngine
{
    public static class Collision
    {
        public static bool AABBvsAABB(Manifold m)
        {
            AABB a, b;
            a = (AABB)m.A;
            b = (AABB)m.B;
            if (a.Max.X > b.Position.X || a.Position.X > b.Max.X)
                return false;
            if (a.Max.Y < b.Position.Y || a.Position.Y > b.Max.Y)
                return false;
            return true;
        }

        public static bool AABBvsCircle(Manifold m)
        {
            AABB a;
            Circle b;

            //Find out which object is of what type.
            if (m.A is AABB)
            {
                a = (AABB)m.A;
                b = (Circle)m.B;
            }
            else
            {
                a = (AABB)m.B;
                b = (Circle)m.A;
            }

            return false;
        }

        public static bool CirclevsCircle(Circle a, Circle b, ref Manifold m)
        {
            m = new Manifold();
            m.A = a;
            m.B = b;

            float collisionzone = a.Radius + b.Radius;
            //collisionzone *= collisionzone;

            float distance = 0f;
            if (a.Position == b.Position)
                distance = 0;
            else if (a.Position.Y == b.Position.Y)
                distance = Math.Abs(a.Position.X - b.Position.X);
            else if (a.Position.X == b.Position.X)
                distance = Math.Abs(a.Position.Y - b.Position.Y);
            else
            {
                var lega = Math.Abs(a.Position.X - b.Position.X);
                var legb = Math.Abs(a.Position.Y - b.Position.Y);
                //lega *= lega;
                //legb *= legb;
                distance = lega + legb;
            }

            m.Normal = PhysicsMath.GetNormal(a.Position, b.Position);

            bool collided = (collisionzone > distance);
            if (collided)
            {
                //Perform an actual square root here
                m.PenetrationDepth = a.Radius + b.Radius - (float)Math.Sqrt(distance);
            }
            else
                m.PenetrationDepth = 0;
            m.AreColliding = collided;
            return collided;
        }

        public static void ResolveCollision(Manifold m)
        {
            Vector2 relVelocity = m.B.Velocity - m.A.Velocity;
            //Finds out if the objects are moving towards each other.
            //We only need to resolve collisions that are moving towards, not away.
            float velAlongNormal = PhysicsMath.DotProduct(relVelocity, m.Normal);
            if (velAlongNormal > 0)
                return;
            float e = Math.Min(m.A.Restitution, m.B.Restitution);

            float j = -(1 + e)*velAlongNormal;
            j /= m.A.InvertedMass + m.B.InvertedMass;

            Vector2 impulse = j*m.Normal;
            m.A.Velocity -= m.A.InvertedMass*impulse;
            m.B.Velocity += m.B.InvertedMass*impulse;
        }
    }
}