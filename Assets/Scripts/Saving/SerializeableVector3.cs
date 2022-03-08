﻿using UnityEngine;

namespace Dungeon.Saving
{
    /// <summary>
    /// Transforms a Vector3 to a serializeable object and forth.
    /// </summary>
    [System.Serializable]
    public class SerializeableVector3
    {
        float x, y, z;

        public SerializeableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
