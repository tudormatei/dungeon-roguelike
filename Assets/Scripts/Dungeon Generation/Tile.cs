using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Tile class.
    /// </summary>
    [System.Serializable]
    public class Tile
    {
        public Transform tile;
        public Transform origin;
        public Connector connector;

        public Tile(Transform tile, Transform origin)
        {
            this.tile = tile;
            this.origin = origin;
        }
    }
}
