using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Chest Object", menuName = "Inventory System/Item/Chest")]
    public class ChestObject : ItemObject
    {
        public Elements element;

        private void Awake()
        {
            type = ItemType.Chest;
        }
    }
}
