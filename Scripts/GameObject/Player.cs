using Godot;
using Com.AdoT_Project.Management;
using System;
using System.Runtime.CompilerServices;
using static Godot.Control;

namespace Com.AdoT_Project.GameObject
{
	public partial class Player : CharacterBody2D
	{
		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Variables

		// MOVEMENTS
		// Parametters
		[Export] private float accelerationSpeed = 250f;
		[Export] private float dashAcceleration = 1000f;
        [Export] private float friction = .1f;
		// Properties
		private Action<float> Move;
		private Vector2 acceleration = Vector2.Zero;
		private Vector2 velocity = Vector2.Zero;
		private Vector2 rotationVector = Vector2.Zero;

		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Initialization
		
		public override void _Ready()
		{
			SetMoveInput();

        }

		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Do On Every Frame
		
		public override void _Process(double delta)
		{
			Move((float)delta);
		}

		// ----------------~~~~~~~~~~~~~~~~~~~==========================# // Movements

		// Set Modes
		private void SetMoveStatic() => Move = MoveStatic;

		private void SetMoveInput() => Move = MoveInput;

        // Movements
        private void MoveStatic(float pDelta) { }

		private void MoveInput(float pDelta)
		{
            acceleration = InputManager.Player.GetDirection() * accelerationSpeed;

			if (velocity.Length() < 1) velocity = InputManager.Player.dashInput ? InputManager.Player.GetDirection() * dashAcceleration : velocity;

            velocity *= Mathf.Pow(friction, pDelta);

            Position += (acceleration + velocity) * pDelta;

            if (InputManager.Player.GetDirection().Length() > 0) Rotation = Mathf.LerpAngle(Rotation, Mathf.Atan2(acceleration.Y, acceleration.X), .5f);
        }
	}
}