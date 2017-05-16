using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame.Model
{
	public class Player
	{
		// Animation representing the player
		private Animation playerAnimation;
		public Animation PlayerAnimation
		{
			get { return playerAnimation; }
			set { playerAnimation = value; }
		}

		// Position of the Player relative to the upper left side of the screen
		// As a struct it cannot be used as a property 😢 sad panda
		public Vector2 Position;

		// State of the player
		private bool Active;
		public bool active
		{
			get { return active; }
			set { active = value; }
		}

		public void Update()
		{
// Update the player animation
		public void Update(GameTime gameTime)
		{
			playerAnimation.Position = Position;
			playerAnimation.Update(gameTime);
		}
	}

	// Amount of hit points that player has
	private int health;
	public int Health
	{
		get { return health; }
		set { health = value; }
	}

	// Get the width of the player ship
	public int Width
	{
		get { return playerAnimation.FrameWidth; }
	}

	// Draw the player
	public void Draw(SpriteBatch spriteBatch)
	{
		playerAnimation.Draw(spriteBatch);
	}
}
}

