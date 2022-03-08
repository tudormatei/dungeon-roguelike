using UnityEngine;
using Unity.AI.Navigation;
using Dungeon.DungeonGeneration;

namespace Core.Enemy
{
    public class NavmeshBaker : MonoBehaviour
    {
        public NavMeshSurface[] surfaces;

        public void BakeNavMesh()
        {
            NavMeshSurface surface = GetComponent<NavMeshSurface>();
            surface.BuildNavMesh();

            GameObject[] decorObjects = GameObject.FindGameObjectsWithTag("Decor");
            foreach(GameObject go in decorObjects)
            {
                go.GetComponent<MeshCombiner>().CombineMesh();
            }
        }
    }
}
