using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Boots Object", menuName = "Inventory System/Item/Boots")]
    public class BootsObject : ItemObject
    {
        public float atkBonus;
        public float defenseBonus;

        private void Awake()
        {
            type = ItemType.Boots;
        }
    }
}
