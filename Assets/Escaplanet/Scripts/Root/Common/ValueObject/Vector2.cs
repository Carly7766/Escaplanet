using System;

namespace Escaplanet.Root.Common.ValueObject
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public float X { get; }
        public float Y { get; }

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

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        #endregion

        #region Operators(float)

        public static Vector2 operator +(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X + scalar, vector.Y + scalar);
        }

        public static Vector2 operator -(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X - scalar, vector.Y - scalar);
        }

        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
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
            if (magnitude <= 0) return Zero;
            return new Vector2(X / magnitude, Y / magnitude);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t
            );
        }

        #endregion
    }
}