using UnityEngine;

namespace Dungeon.Audio
{
    /// <summary>
    /// Taking care of the audio clips to play in order.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Settings")]
        public GameObject audioSourcePrefab;
        public AudioClip[] audioClips;

        public void TriggerSoundEffect(int index)
        {
            GameObject soundEffect = Instantiate(audioSourcePrefab);
            AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
            audioSource.clip = audioClips[index];
            audioSource.Play();
            Destroy(soundEffect, audioClips[index].length);
        }
    }
}
