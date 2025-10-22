﻿using Escaplanet.Root.Common.ValueObject;

namespace Escaplanet.Ingame.Core.Camera
{
    public struct CameraState
    {
        public Vector2 Position;
        public float Rotation;
        public float OrthographicSize;

        public CameraState(Vector2 position, float rotation, float orthographicSize)
        {
            Position = position;
            Rotation = rotation;
            OrthographicSize = orthographicSize;
        }

        public static CameraState Lerp(in CameraState a, in CameraState b, float t)
        {
            return new CameraState
            {
                Position = Vector2.Lerp(a.Position, b.Position, t),
                Rotation = LerpAngle(a.Rotation, b.Rotation, t),
                OrthographicSize = LerpFloat(a.OrthographicSize, b.OrthographicSize, t),
            };
        }

        private static float LerpFloat(float a, float b, float t) => a + (b - a) * t;

        private static float LerpAngle(float a, float b, float t)
        {
            var delta = Repeat((b - a) + 180f, 360f) - 180f;
            return a + delta * t;
        }

        private static float Repeat(float v, float len) => v - System.MathF.Floor(v / len) * len;
    }
}