using System;

namespace Escaplanet.Root.Domain.TempSceneData.ResultData
{
    public class ResultData : IEquatable<ResultData>
    {
        private readonly LastTimeResultDataId id;

        public ResultData(LastTimeResultDataId id, int lastStageId)
        {
            this.id = id;
            LastStageId = lastStageId;
        }

        public int LastStageId { get; private set; }

        public bool Equals(ResultData other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return id.Equals(other.id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    public struct LastTimeResultDataId : IEquatable<LastTimeResultDataId>
    {
        private readonly int value;

        private LastTimeResultDataId(int value)
        {
            this.value = value;
        }

        public static LastTimeResultDataId NewId()
        {
            return new LastTimeResultDataId(ResultDataIdGenerator.Next());
        }

        private static class ResultDataIdGenerator
        {
            private static int _counter;

            public static int Next()
            {
                return _counter++;
            }
        }

        public bool Equals(LastTimeResultDataId other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            return obj is LastTimeResultDataId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}