using System;

namespace Escaplanet.Ingame.Data
{
    public class Speed : IEquatable<Speed>
    {
        public readonly float Value;

        public Speed(float value)
        {
            Value = value;
        }

        #region Equals

        public bool Equals(Speed other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Speed otherSpeed)
            {
                return Equals(otherSpeed);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region Operators(Speed)

        #endregion

        #region Utility

        public Speed Absolute()
        {
            return Value >= 0 ? this : new Speed(-Value);
        }

        #endregion
    }
}