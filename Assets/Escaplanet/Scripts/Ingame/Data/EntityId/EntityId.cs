using System;

namespace Escaplanet.Ingame.Data.EntityId
{
    public struct EntityId : IEquatable<EntityId>
    {
        private readonly uint _value;

        public ushort Index => (ushort)(_value & 0xFFFF);
        public byte Generation => (byte)((_value >> 16) & 0xFF);

        public bool IsValid => Index != 0;

        public EntityId(ushort index, byte generation)
        {
            _value = ((uint)generation << 16) | index;
        }

        public bool Equals(EntityId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is EntityId o && Equals(o);
        }

        public override int GetHashCode()
        {
            return (int)_value;
        }

        public static bool operator ==(EntityId a, EntityId b)
        {
            return a._value == b._value;
        }

        public static bool operator !=(EntityId a, EntityId b)
        {
            return a._value != b._value;
        }

        public override string ToString()
        {
            return IsValid ? $"{Index}:{Generation}" : "Invalid";
        }
    }
}