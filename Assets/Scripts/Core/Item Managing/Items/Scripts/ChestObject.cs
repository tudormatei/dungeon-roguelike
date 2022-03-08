using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Chest Object", menuName = "Inventory System/Item/Chest")]
    public class ChestObject : ItemObject
    {
        public float atkBonus;
        public float defenseBonus;

        private void Awake()
        {
            type = ItemType.Chest;
        }
    }
}
