using UnityEngine;

namespace Escaplanet.Root.Common
{
    public class UnityGlobalValuePort : MonoBehaviour, IGlobalValuePort
    {
        public float FixedDeltaTime => Time.fixedDeltaTime;
        public float Epsilon => Mathf.Epsilon;

        public bool Approximately(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }
    }
}