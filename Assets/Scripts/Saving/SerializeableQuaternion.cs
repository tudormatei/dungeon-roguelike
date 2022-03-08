using UnityEngine;

namespace Dungeon.Saving
{
    /// <summary>
    /// Transforms a Quaternion to a serializable object and forth.
    /// </summary>
    [System.Serializable]
    public class SerializeableQuaternion
    {
        float x, y, z, w;

        public SerializeableQuaternion(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public Quaternion ToQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}
