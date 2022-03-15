using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Helmet Object", menuName = "Inventory System/Item/Helmet")]
    public class HelmetObject : ItemObject
    {
        public Elements element;

        private void Awake()
        {
            type = ItemType.Helmet;
        }
    }
}
