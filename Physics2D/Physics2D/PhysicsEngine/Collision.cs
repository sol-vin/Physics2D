using System;
using Microsoft.Xna.Framework;

namespace Physics2D.PhysicsEngine
{
    public static class Collision
    {
        public static bool AABBvsAABB (AABB a, AABB b, ref Manifold m)
		{
			m.A = a;
			m.B = b;
			m.Normal = b.Position - a.Position;

			//Calculate the extent on the X axis
			float aExtent = (a.Right - a.Left) / 2;
			float bExtent = (b.Right - b.Left) / 2;

			//Find the X overlap
			float xExtent = aExtent + bExtent - Math.Abs (m.Normal.X);

			//SAT Test on X
			if (xExtent > 0) {
				//There was overlap on the X axis, now lets try to Y
				aExtent = (a.Bottom - a.Top) / 2;
				bExtent = (b.Bottom - b.Top) / 2;

				//Calculate Y overlap
				float yExtent = aExtent + bExtent - Math.Abs(m.Normal.Y);

				//SAT Test on Y axis
				if (yExtent > 0){
					//Find which axis has the biggest penetration ;D
					if (xExtent > yExtent){
						if(m.Normal.X < 0)
							m.Normal = new Vector2(-1,0);
						else
							m.Normal= Vector2.Zero;
						m.PenetrationDepth = xExtent;	
						m.AreColliding = true;
						return true;
					}
					else {
						if(m.Normal.Y < 0)
							m.Normal = new Vector2(0,-1);
						else
							m.Normal= Vector2.Zero;
						m.PenetrationDepth = yExtent;
						m.AreColliding = true;
						return true;
					}
				}
			}

			return false;
        }

        public static bool AABBvsCircle (AABB a, Circle b, ref Manifold m)
		{
			m.A = a;
			m.B = b;

			Vector2 n = b.Position - a.Position;
			//Closest edge
			Vector2 closest = m.Normal;

			//Find extents for our AABB
			float xExtent = (a.Right - a.Left) / 2;
			float yExtent = (a.Right - a.Left) / 2;

			closest.X = MathHelper.Clamp (-xExtent, xExtent, closest.X);
			closest.Y = MathHelper.Clamp (-yExtent, yExtent, closest.Y);

			//whether or not the circle is inside the aabb
			bool inside = false;
			if (n == closest) {
				inside = true;

				//Find closest edge
				if (Math.Abs (n.X) > Math.Abs (n.Y)) {
					// Clamp to closest extent
					if (closest.X > 0)
						closest.X = xExtent;
					else
						closest.X = -xExtent;
				}
			 
			    // Y axis is shorter
			    else {
					// Clamp to closest extent
					if (closest.Y > 0)
						closest.Y = yExtent;
					else
						closest.Y = -yExtent;
				}
			}

			m.Normal = n - closest;
			float d = n.LengthSquared ();
			float r = b.Radius;

			//Early out if the circle's radius is shorter than the distance to the closest point
			//and the circle isn't in the AABB.

			if (d > r * r && !inside)
				return false;
			d = (float)Math.Sqrt (d);
			if (inside) {
				m.Normal = -PhysicsMath.GetNormal(a.Position, b.Position);
				m.PenetrationDepth = r + d;
				m.AreColliding = true;
				return true;
			} 
			else {
				m.Normal = PhysicsMath.GetNormal(a.Position, b.Position);
				m.PenetrationDepth = r + d;
				m.AreColliding = true;
				return true;
			}
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