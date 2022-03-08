using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Item/Default")]
    public class DefaultObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Default;
        }

        public bool isKey;
        public int keyHash;
    }
}
