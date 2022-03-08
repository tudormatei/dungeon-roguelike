using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Enables the door after generation, so navmesh can bake properly.
    /// </summary>
    public class DoorEnabling : MonoBehaviour
    {
        [SerializeField] private GameObject door;
        private bool isCompleted = false;

        private void Update()
        {
            if (!isCompleted && DungeonGenerator.dungeonState == DungeonState.completed)
            {
                isCompleted = true;
                door.SetActive(true);
            }
        }
    }
}
