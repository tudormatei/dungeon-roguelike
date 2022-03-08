using System.Collections;
using UnityEngine;

namespace Core.Utils
{
    public class TorchFireManager : MonoBehaviour
    {
        private ParticleSystem ps;

        private Light torchLight;

        [SerializeField] private GameObject teleportParticles;

        private void Start()
        {
            if(teleportParticles != null)
                teleportParticles.SetActive(false);

            ps = GetComponent<ParticleSystem>();
            torchLight = GetComponentInChildren<Light>();

            torchLight.intensity = 0f;
            ps.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (ps != null)
                {
                    if (teleportParticles != null)
                        teleportParticles.SetActive(true);

                    ps.Play();
                    StopAllCoroutines();
                    StartCoroutine(LerpLightIntesity(1f, 1f));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (ps != null)
                {
                    if (teleportParticles != null)
                        teleportParticles.SetActive(false);

                    ps.Stop();
                    StopAllCoroutines();
                    StartCoroutine(LerpLightIntesity(0f, .5f));
                }
            }
        }

        private IEnumerator LerpLightIntesity(float endValue, float duration)
        {
            float startValue = torchLight.intensity;
            float time = 0f;

            while (time < duration)
            {
                torchLight.intensity = Mathf.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            torchLight.intensity = endValue;
        }
    }
}
