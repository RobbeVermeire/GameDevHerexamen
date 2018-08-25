using Microsoft.Xna.Framework;

namespace PlatformGame.Source
{
    public static class Constants
    {
        public static Vector2 Gravity = new Vector2(0, 1);
        public static int ScreenWidth = 1050;
        public static int ScreenHeight = 700;
    }
    public enum Direction
    {
        Top,
        Right,
        Bottom,
        Left
    }
}
