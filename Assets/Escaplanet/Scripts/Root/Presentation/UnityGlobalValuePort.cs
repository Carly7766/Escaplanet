using Escaplanet.Root.Common;
using Escaplanet.Root.Core.Common;
using UnityEngine;

namespace Escaplanet.Root.Presentation
{
    public class UnityGlobalValuePort : MonoBehaviour, IGlobalValuePort
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
    }
}