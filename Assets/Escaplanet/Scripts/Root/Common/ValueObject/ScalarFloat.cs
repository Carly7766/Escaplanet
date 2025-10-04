using System;
using UnityEngine;

namespace Escaplanet.Root.Common.ValueObject
{
    public struct ScalarFloat : IEquatable<ScalarFloat>, IComparable<ScalarFloat>
    {
        public float Value { get; }

        public ScalarFloat(float value)
        {
            Value = value;
        }

        #region Constants

        public static ScalarFloat Zero => new(0f);
        public static ScalarFloat One => new(1f);
        public static ScalarFloat Two => new(2f);
        public static ScalarFloat Rad2Deg => new(57.29578f);
        public static ScalarFloat Epsilon => new(Mathf.Epsilon);

        #endregion

        #region Interfaces

        public bool Equals(ScalarFloat other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is ScalarFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public int CompareTo(ScalarFloat other)
        {
            return Value.CompareTo(other.Value);
        }

        #endregion

        #region Operators(ScalarFloat)

        public static ScalarFloat operator +(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(a.Value + b.Value);
        }

        public static ScalarFloat operator -(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(a.Value - b.Value);
        }

        public static ScalarFloat operator *(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(a.Value * b.Value);
        }

        public static ScalarFloat operator /(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(a.Value / b.Value);
        }

        public static ScalarFloat operator %(ScalarFloat a, ScalarFloat b)
        {
            return new ScalarFloat(a.Value % b.Value);
        }

        public static bool operator ==(ScalarFloat a, ScalarFloat b)
        {
            return (double)MathF.Abs((b - a).Value) <
                   (double)MathF.Max(1E-06f * MathF.Max(MathF.Abs(a.Value), MathF.Abs(b.Value)), Mathf.Epsilon * 8f);
        }

        public static ScalarFloat operator -(ScalarFloat a)
        {
            return new ScalarFloat(-a.Value);
        }

        public static bool operator !=(ScalarFloat a, ScalarFloat b)
        {
            return !(a == b);
        }

        public static bool operator >(ScalarFloat a, ScalarFloat b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(ScalarFloat a, ScalarFloat b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(ScalarFloat a, ScalarFloat b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(ScalarFloat a, ScalarFloat b)
        {
            return a.Value <= b.Value;
        }

        #endregion

        #region Operators(Vector2)

        public static Vector2 operator +(ScalarFloat scalar, Vector2 vector)
        {
            return new Vector2(scalar + vector.X, scalar + vector.Y);
        }

        public static Vector2 operator -(ScalarFloat scalar, Vector2 vector)
        {
            return new Vector2(scalar - vector.X, scalar - vector.Y);
        }

        public static Vector2 operator *(ScalarFloat scalar, Vector2 vector)
        {
            return new Vector2(scalar * vector.X, scalar * vector.Y);
        }

        public static Vector2 operator /(ScalarFloat scalar, Vector2 vector)
        {
            return new Vector2(scalar / vector.X, scalar / vector.Y);
        }

        #endregion
    }
}