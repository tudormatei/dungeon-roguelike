using UnityEngine;

namespace Core.Utils
{
    public class StartRoomLightActivate : MonoBehaviour
    {
        private void Start()
        {
            Light light = GetComponentInChildren<Light>();
            light.intensity = 1f;

            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Play();
        }
    }
}
