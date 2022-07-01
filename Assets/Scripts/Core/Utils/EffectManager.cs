using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Core.Utils
{
    public class EffectManager : MonoBehaviour
    {
        private List<ParticleSystem> vfxList = new List<ParticleSystem>();

        private void Start()
        {
            foreach(ParticleSystem ps in transform.GetComponentsInChildren<ParticleSystem>())
            {
                vfxList.Add(ps);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (vfxList.Count != 0)
                {
                    foreach(ParticleSystem ps in vfxList)
                    {
                        ps.Play();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (vfxList.Count != 0)
                {
                    foreach (ParticleSystem ps in vfxList)
                    {
                        ps.Stop();
                    }
                }
            }
        }
    }
}
