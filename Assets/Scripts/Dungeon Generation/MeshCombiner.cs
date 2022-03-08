using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Combines all the mesh of the child gameobjects of the script 
    /// (used for room decor).
    /// </summary>
    public class MeshCombiner : MonoBehaviour
    {
        public void CombineMesh()
        {
            StaticBatchingUtility.Combine(gameObject);
        }
    }
}
