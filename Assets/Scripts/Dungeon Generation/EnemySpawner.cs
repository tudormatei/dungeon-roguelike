using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Enemy spawning after the dungeon generation so the navmesh can bake
    /// properly.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawner Settings")]
        [SerializeField] GameObject[] enemyPrefabs;
        private bool isCompleted = false;

        private void Update()
        {
            if (!isCompleted && DungeonGenerator.dungeonState == DungeonState.completed)
            {
                isCompleted = true;
                int decorIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject go = Instantiate(enemyPrefabs[decorIndex], transform.position, Quaternion.identity, transform) as GameObject;
                go.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                go.name = enemyPrefabs[decorIndex].name;
            }
        }
    }
}
