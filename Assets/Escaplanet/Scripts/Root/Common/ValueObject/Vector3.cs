using System;

namespace Escaplanet.Root.Common.ValueObject
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Constants

        public static Vector3 Zero => new(0, 0, 0);

        #endregion

        #region Equals

        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        #endregion

        #region Operators(Vector3)

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        #endregion

        #region Operators(float)

        public static Vector3 operator +(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X + scalar, vector.Y + scalar, vector.Z + scalar);
        }

        public static Vector3 operator -(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X - scalar, vector.Y - scalar, vector.Z - scalar);
        }

        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }

        #endregion

        #region Utilities

        public static Vector3 Lerp(in Vector3 a, in Vector3 b, float t)
        {
            return new Vector3(
                a.X + (b.X - a.X) * t,
                a.Y + (b.Y - a.Y) * t,
                a.Z + (b.Z - a.Z) * t);
        }

        #endregion
    }
}