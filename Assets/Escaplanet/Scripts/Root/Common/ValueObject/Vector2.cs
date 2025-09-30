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

        #region Operators(ScalarFloat)

        public static Vector2 operator +(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X + scalar.Value, vector.Y + scalar.Value);
        }

        public static Vector2 operator -(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X - scalar.Value, vector.Y - scalar.Value);
        }

        public static Vector2 operator *(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X * scalar.Value, vector.Y * scalar.Value);
        }

        public static Vector2 operator /(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X / scalar.Value, vector.Y / scalar.Value);
        }

        #endregion

        #region Utility

        public ScalarFloat Magnitude()
        {
            return new ScalarFloat(MathF.Sqrt(X * X + Y * Y));
        }

        public ScalarFloat SquareMagnitude()
        {
            return new ScalarFloat(X * X + Y * Y);
        }

        public Vector2 Normalize()
        {
            var magnitude = Magnitude();
            if (magnitude.Value == 0)
            {
                return new Vector2(0, 0);
            }

            return new Vector2(X / magnitude.Value, Y / magnitude.Value);
        }

        #endregion
    }
}