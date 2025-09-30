using Escaplanet.Root.Common.ValueObject;
using UnityEngine;

namespace Escaplanet.Root.Common
{
    public class UnityGlobalPort : MonoBehaviour, IUnityGlobalPort
    {
        public ScalarFloat FixedDeltaTime => new(Time.fixedDeltaTime);
    }
}