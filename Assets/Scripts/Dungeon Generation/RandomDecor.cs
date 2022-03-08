using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Spawning the random decor in each room prefab.
    /// </summary>
    public class RandomDecor : MonoBehaviour
    {
        [Header("Decor Settings")]
        [SerializeField] GameObject[] decorPrefabs;

        public void Decorate()
        {
            int decorIndex = Random.Range(0, decorPrefabs.Length);
            GameObject go = Instantiate(decorPrefabs[decorIndex], transform.position, Quaternion.identity, transform) as GameObject;
            go.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            go.name = decorPrefabs[decorIndex].name;
        }
    }
}
