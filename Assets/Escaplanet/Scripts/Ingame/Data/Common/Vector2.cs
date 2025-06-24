using System;

namespace Escaplanet.Ingame.Data.Common
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public readonly float X { get; }
        public readonly float Y { get; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        #region Constants

        public static Vector2 Zero => new(0, 0);

        #endregion

        #region Equals

        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        #endregion

        #region Operators(Vector2)

        public Vector2 Add(Vector2 other)
        {
            return new Vector2(X + other.X, Y + other.Y);
        }

        public Vector2 Subtract(Vector2 other)
        {
            return new Vector2(X - other.X, Y - other.Y);
        }

        public Vector2 Multiply(float scalar)
        {
            return new Vector2(X * scalar, Y * scalar);
        }

        public Vector2 Divide(float scalar)
        {
            return new Vector2(X / scalar, Y / scalar);
        }

        #endregion

        #region Operators(scalar)

        public Vector2 Add(int scalar)
        {
            return new Vector2(X + scalar, Y + scalar);
        }

        public Vector2 Subtract(int scalar)
        {
            return new Vector2(X - scalar, Y - scalar);
        }

        public Vector2 Multiply(int scalar)
        {
            return new Vector2(X * scalar, Y * scalar);
        }

        public Vector2 Divide(int scalar)
        {
            return new Vector2(X / scalar, Y / scalar);
        }

        #endregion

        #region Utility

        public float Magnitude()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        public float SquareMagnitude()
        {
            return X * X + Y * Y;
        }

        public Vector2 Normalize()
        {
            var magnitude = Magnitude();
            var normalized = Divide(magnitude);
            return normalized;
        }

        public static Speed Dot(Vector2 a, Vector2 b)
        {
            return new Speed(a.X * b.X + a.Y * b.Y);
        }

        #endregion
    }
}