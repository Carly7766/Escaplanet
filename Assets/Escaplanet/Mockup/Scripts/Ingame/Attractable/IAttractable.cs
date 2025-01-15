using UnityEngine;

namespace Escaplanet.Mockup.Ingame.Attractable
{
    public interface IAttractable
    {
        Vector2 Position { get; }
        void Attract(Vector2 acceleration);
    }
}