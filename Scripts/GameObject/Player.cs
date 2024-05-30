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
		private Vector2 direction = Vector2.Zero;
		private Vector2 acceleration = Vector2.Zero;
		private float additionalAccelerationSpeed = 0f;
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

		private void SetMoveInput()
		{
			Move = MoveInput;
			UpDateDirection();
		}

        // Movements
        private void MoveStatic(float pDelta) { }

		private void MoveInput(float pDelta)
		{
            additionalAccelerationSpeed = InputManager.Player.dashInput ? dashAcceleration : additionalAccelerationSpeed;

            acceleration = InputManager.Player.GetDirection() * (accelerationSpeed);
			if (additionalAccelerationSpeed > 1f && acceleration.Length() <= 0f) acceleration = direction * additionalAccelerationSpeed;
			else acceleration += InputManager.Player.GetDirection() * additionalAccelerationSpeed;
            additionalAccelerationSpeed *= Mathf.Pow(friction, pDelta);

            Position += acceleration * pDelta;

            if (InputManager.Player.GetDirection().Length() > 0) Rotation = Mathf.LerpAngle(Rotation, Mathf.Atan2(acceleration.Y, acceleration.X), .25f);

			UpDateDirection();
        }

		private void UpDateDirection()
		{
            direction = new Vector2();
            direction.X = Mathf.Cos(Rotation);
            direction.Y = Mathf.Sin(Rotation);
        }
	}
}