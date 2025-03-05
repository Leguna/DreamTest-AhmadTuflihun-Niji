namespace Constant
{
    public static class GameConst
    {
        public const int SplashScreenDelayInSecond = 2;
        public const float LoadingScreenDelay = 2f;

        // Object name
        public const string PlayerObjectName = "Player";
        public const string PlayerCameraObjectName = "PlayerCamera";
        public const string CameraBound = "CameraBound";

        // Tag
        public const string BoundingTag = "Bounding";

        // Resources Path
        public const string ItemIconPath = "Icon/Item/";
        public const string ItemSoPath = "SO/Items/";
    }
    
    public enum FacingDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}