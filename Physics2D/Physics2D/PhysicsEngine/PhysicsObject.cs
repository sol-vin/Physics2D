using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Physics2D.PhysicsEngine
{
    public abstract class PhysicsObject
    {
		/// <summary>
		/// The direction and speed at which this object is moving
		/// </summary>
        public Vector2 Velocity = Vector2.Zero;


		/// <summary>
		/// The direction and speed the object should accelerate at.
		/// </summary>
        public Vector2 Acceleration = Vector2.Zero;

		//// <summary>
		/// Backing field for Mass.
		/// </summary>
        private float _mass = 1f;
		/// <summary>
		/// The mass of the object.
		/// </summary>
		/// <value>
		/// The mass.
		/// </value>
        public float Mass
        {
            get { return _mass; }
            set 
            {
				if(value < 0) throw new Exception("Mass cannot be less than zero!");
				_mass = value;

				if(value == 0)
					InvertedMass = 0;
				else
	                InvertedMass = 1/_mass;
            }
        }
		/// <summary>
		/// Gets one divided by mass (1/mass).
		/// </summary>
		/// <value>
		/// The inverted mass.
		/// </value>
        public float InvertedMass { get; private set; }

		/// <summary>
		/// Bounciness of this object
		/// </summary>
        public float Restitution = 0f;
		/// <summary>
		/// The ammount of friction
		/// </summary>
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
