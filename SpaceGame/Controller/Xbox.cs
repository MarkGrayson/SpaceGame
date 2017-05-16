using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceGame.Model;
using SpaceGame.View;
using SpaceGame;

namespace SpaceGame.Controller
{
	public class Xbox : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Player player;

		// Enemies
		private Texture2D enemyTexture;
		private List enemies;

		// The rate at which the enemies appear
		private TimeSpan enemySpawnTime;
		private TimeSpan previousSpawnTime;

		// A random number generator
		private Random random;
		private KeyboardState currentKeyboardState;
		private KeyboardState previousKeyboardState;
		private GamePadState currentGamePadState;
		private GamePadState previousGamePadState;
		private float playerMoveSpeed;

		public Xbox()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		private void UpdatePlayer(GameTime gameTime)
		{
			player.Update(gameTime);
			// Get Thumbstick Controls
			player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
			player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

			// Use the Keyboard / Dpad
			if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
			{
				player.Position.X -= playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
			{
				player.Position.X += playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
			{
				player.Position.Y -= playerMoveSpeed;
			}
			if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
			{
				player.Position.Y += playerMoveSpeed;
			}

			// Make sure that the player does not go out of bounds
			player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
			player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			player = new Player();
			playerMoveSpeed = 8.0f;
			base.Initialize();

			// Initialize the enemies list
			enemies = new List();

			// Set the time keepers to zero
			previousSpawnTime = TimeSpan.Zero;

			// Used to determine how fast enemy respawns
			enemySpawnTime = TimeSpan.FromSeconds(1.0f);

			// Initialize our random number generator
			random = new Random();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			enemyTexture = Content.Load("Animation/mineAnimation");

			// Load the player resources
			Animation playerAnimation = new Animation();
			Texture2D playerTexture = Content.Load("shipAnimation");
			playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);

			Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
			player.Initialize(playerAnimation, playerPosition);

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif
private void AddEnemy()
{
	// Create the animation object
	Animation enemyAnimation = new Animation();

	// Initialize the animation with the correct animation information
	enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

	// Randomly generate the position of the enemy
	Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

	// Create an enemy
	Enemy enemy = new Enemy();

	// Initialize the enemy
	enemy.Initialize(enemyAnimation, position);

	// Add the enemy to the active enemies list
	enemies.Add(enemy);
}

			// Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
			previousGamePadState = currentGamePadState;
			previousKeyboardState = currentKeyboardState;

			// Read the current state of the keyboard and gamepad and store it
			currentKeyboardState = Keyboard.GetState();
			currentGamePadState = GamePad.GetState(PlayerIndex.One);


			//Update the player
			UpdatePlayer(gameTime);
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			player.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
