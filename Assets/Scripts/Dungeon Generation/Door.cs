using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Controls door collider so player does not get pushed off
    /// </summary>
    public class Door : MonoBehaviour
    {
        [SerializeField] private BoxCollider box;

        public void TurnOffCollider()
        {
            box.isTrigger = true;
        }

        public void TurnOnCollider()
        {
            box.isTrigger = false;
        }
    }
}
