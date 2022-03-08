using UnityEngine;
using Dungeon.DungeonGeneration;

namespace Dungeon.UI
{
    /// <summary>
    /// Everything related to player hud.
    /// </summary>
    public class PlayerHud : MonoBehaviour
    {
        [Header("HUD Settings")]
        public GameObject hud;

        private bool activate = false;

        private void Start()
        {
            hud.SetActive(false);
        }

        private void Update()
        {
            if (DungeonGenerator.dungeonState == DungeonState.completed && !activate)
            {
                hud.SetActive(true);
                activate = true;
            }
            else { return; }
        }
    }
}
