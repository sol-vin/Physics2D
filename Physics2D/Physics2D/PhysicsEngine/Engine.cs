using System;
using Microsoft.Xna.Framework;

namespace Physics2DMono
{
	public class Engine
	{
		public const int MAXFRAMESPERSECOND = 60;
		public int DeltaTime {get{ return 1000 / MAXFRAMESPERSECOND;}}
		/// <summary>
		/// The time in between each physics update
		/// </summary>
		private int _timebetweenupdate;

		public Engine ()
		{
		}

		public void Update (GameTime gt)
		{
			_timebetweenupdate += gt.ElapsedGameTime.Milliseconds;

			//Clamp the time between update to prevent it from the spiral of death.
			if(_timebetweenupdate > 2 * DeltaTime)
				_timebetweenupdate = 2 * DeltaTime;


			if (_timebetweenupdate > DeltaTime) {
				_timebetweenupdate -= DeltaTime; //Ensure the counter is reset but, does not lose extra time it already had
				UpdatePhysics(gt);
			}
		}

		public void UpdatePhysics(GameTime gt)
		{
		}
	}
}