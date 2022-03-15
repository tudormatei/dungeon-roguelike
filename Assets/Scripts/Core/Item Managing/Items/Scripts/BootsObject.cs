using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Boots Object", menuName = "Inventory System/Item/Boots")]
    public class BootsObject : ItemObject
    {
        public Elements element;

        private void Awake()
        {
            type = ItemType.Boots;
        }
    }
}
