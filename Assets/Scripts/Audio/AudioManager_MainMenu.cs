using UnityEngine;
using Dungeon.Networking;

namespace Dungeon.Audio
{
    /// <summary>
    /// Main menu section of
    /// </summary>
    public class AudioManager_MainMenu : MonoBehaviour
    {
        private void Start()
        {
            if (!GameObject.Find("Soundtrack(Clone)"))
            {
                Instantiate(Declarations.audioSourceSoundtrack);
            }
        }
    }
}
