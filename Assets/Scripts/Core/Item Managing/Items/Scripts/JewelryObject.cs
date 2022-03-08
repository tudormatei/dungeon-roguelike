using UnityEngine;

namespace Core.ItemManagement
{
    public enum JewelryType
    {
        Fire_Ring
    }

    [CreateAssetMenu(fileName = "New Jewelry Object", menuName = "Inventory System/Item/Jewelry")]
    public class JewelryObject : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Jewelry;
        }

        public JewelryType jewelryType;
        [SerializeField] private GameObject effect;
        [SerializeField] private float timeBetweenAttacks = 5f;
        [SerializeField] private float range = 3f;
        [SerializeField] private float damage = 10f;

        public float TimeBetweenAttacks { get => timeBetweenAttacks; set => timeBetweenAttacks = value; }
        public float Range { get => range; set => range = value; }
        public float Damage { get => damage; set => damage = value; }
        public GameObject Effect { get => effect; set => effect = value; }
    }
}
