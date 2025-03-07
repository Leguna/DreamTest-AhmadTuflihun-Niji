using UnityEngine;

namespace Facing
{
    public enum FacingDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Side
    {
        Front,
        Back,
        Side
    }

    public static class Vector2Extensions
    {
        public static FacingDirection ToFacingDirection(this Vector2 vector2)
        {
            float angle = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;

            return angle switch
            {
                >= -45 and < 45 => FacingDirection.Right,
                >= 45 and < 135 => FacingDirection.Up,
                >= -135 and < -45 => FacingDirection.Down,
                _ => FacingDirection.Left
            };
        }

        public static Vector2 ToUnityVector2(this FacingDirection facingDirection)
        {
            return facingDirection switch
            {
                FacingDirection.Up => Vector2.up,
                FacingDirection.Down => Vector2.down,
                FacingDirection.Left => Vector2.left,
                FacingDirection.Right => Vector2.right,
                _ => Vector2.zero
            };
        }

        public static Side GetSide(this FacingDirection facingDirection, Vector2 target)
        {
            return target.GetSide(facingDirection);
        }

        public static Side GetSide(this Vector2 target, FacingDirection facingDirection)
        {
            Vector2 direction = target.normalized;
            float dot = Vector2.Dot(direction, facingDirection.ToUnityVector2());

            return dot switch
            {
                > 0.5f => Side.Front,
                < -0.5f => Side.Back,
                _ => Side.Side
            };
        }

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Quaternion ToRotation(this FacingDirection facingDirection, float additionalAngle = 0)
        {
            return facingDirection switch
            {
                FacingDirection.Up => Quaternion.Euler(0, 0, 0 + additionalAngle),
                FacingDirection.Down => Quaternion.Euler(0, 0, 180 + additionalAngle),
                FacingDirection.Left => Quaternion.Euler(0, 0, 90 + additionalAngle),
                FacingDirection.Right => Quaternion.Euler(0, 0, -90 + additionalAngle),
                _ => Quaternion.identity
            };
        }
    }
}