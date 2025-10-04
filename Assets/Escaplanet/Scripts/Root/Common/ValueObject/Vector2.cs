using System;

namespace Escaplanet.Root.Common.ValueObject
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public ScalarFloat X { get; }
        public ScalarFloat Y { get; }

        public Vector2(float x, float y)
        {
            X = new ScalarFloat(x);
            Y = new ScalarFloat(y);
        }

        public Vector2(ScalarFloat x, ScalarFloat y)
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
            return new Vector2(vector.X + scalar, vector.Y + scalar);
        }

        public static Vector2 operator -(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X - scalar, vector.Y - scalar);
        }

        public static Vector2 operator *(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2 operator /(Vector2 vector, ScalarFloat scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
        }

        #endregion

        #region Utility

        public ScalarFloat Magnitude()
        {
            return X * X + Y * Y;
        }

        public ScalarFloat SquareMagnitude()
        {
            return X * X + Y * Y;
        }

        public Vector2 Normalize()
        {
            var magnitude = Magnitude();
            if (magnitude.Value == 0)
            {
                return new Vector2(0, 0);
            }

            return new Vector2(X / magnitude, Y / magnitude);
        }

        public static ScalarFloat Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        #endregion
    }
}