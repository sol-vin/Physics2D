using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Physics2D.PhysicsEngine
{
    public abstract class PhysicsObject
    {
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;

        private float _mass = 0f;
        public float Mass
        {
            get { return _mass; }
            set 
            {
                _mass = value;
                InvertedMass = 1/_mass;
            }
        }
        public float InvertedMass { get; private set; }


        public float Restitution = 0f;
        public float Drag = 1f;
        public float Angle = 0f;
        public float AngularVelocity = 0f;
        public float AngularAcceleration = 0f;
        public float AngularDrag = 0f;

        public Vector2 Position;
        public Rectangle BoundingBox { get; set; }

        public virtual void Update(GameTime gt)
        {
            Velocity += Acceleration;
            Velocity *= Drag;
            Position += Velocity;

            AngularVelocity += AngularAcceleration;
            AngularVelocity *= AngularDrag;
            Angle += AngularVelocity;
        }
    }
}
