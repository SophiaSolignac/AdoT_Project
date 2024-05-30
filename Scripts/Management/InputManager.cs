using Godot;

namespace Com.AdoT_Project.Management
{
	public static class InputManager
	{
		private const string NAME_PLAYER_GO_LEFT = "playerGoLeft";
		private const string NAME_PLAYER_GO_UP = "playerGoUp";
		private const string NAME_PLAYER_GO_RIGHT = "playerGoRight";
		private const string NAME_PLAYER_GO_DOWN = "playerGoDown";

        private const string NAME_PLAYER_DASH_LEFT = "playerDashLeft";
        private const string NAME_PLAYER_DASH_UP = "playerDashUp";
        private const string NAME_PLAYER_DASH_RIGHT = "playerDashRight";
        private const string NAME_PLAYER_DASH_DOWN = "playerDashDown";
        private const string NAME_PLAYER_DASH = "dash";

        public static class Player
		{
			// JOY
			private const float JOY_DETECTION = .2f;

			// MOVEMENTS
			private static float goLeftStrength => GetCorrectedStrength(NAME_PLAYER_GO_LEFT);
			private static float goUpStrength => GetCorrectedStrength(NAME_PLAYER_GO_UP);
			private static float goRightStrength => GetCorrectedStrength(NAME_PLAYER_GO_RIGHT);
			private static float goDownStrength => GetCorrectedStrength(NAME_PLAYER_GO_DOWN);

            private static float dashLeftStrength => GetCorrectedStrength(NAME_PLAYER_DASH_LEFT);
            private static float dashUpStrength => GetCorrectedStrength(NAME_PLAYER_DASH_UP);
            private static float dashRightStrength => GetCorrectedStrength(NAME_PLAYER_DASH_RIGHT);
            private static float dashDownStrength => GetCorrectedStrength(NAME_PLAYER_DASH_DOWN);
            public static bool dashInput => Input.IsActionJustPressed(NAME_PLAYER_DASH);


            private static Vector2 direction = Vector2.Zero;
			private static Vector2 dashDirection = Vector2.Zero;

			private static float GetCorrectedStrength(string pName) => Input.GetActionRawStrength(pName) < JOY_DETECTION ? 0 : Input.GetActionRawStrength(pName);

            public static Vector2 GetDirection()
			{
				// Get Direction Based On Inputs
				direction.X = goRightStrength - goLeftStrength;
				direction.Y = goDownStrength - goUpStrength;
				// Clamp Under 1f
				if (direction.Length() > 1f) direction = direction.Normalized();

                return direction;
            }

            public static Vector2 GetDashDirection()
            {
                // Get Direction Based On Inputs
                dashDirection.X = dashRightStrength - dashLeftStrength;
                dashDirection.Y = dashDownStrength - dashUpStrength;

                return dashDirection.Normalized();
            }
        }
	}
}
