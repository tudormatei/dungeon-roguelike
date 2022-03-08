using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Item/Food")]
    public class FoodObject : ItemObject
    {
        public int restoreHealth;
        private void Awake()
        {
            type = ItemType.Food;
        }
    }
}
