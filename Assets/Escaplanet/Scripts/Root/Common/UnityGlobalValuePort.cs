using UnityEngine;

namespace Escaplanet.Root.Common
{
    public class UnityGlobalValuePort : MonoBehaviour, IGlobalValuePort
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
    }
}