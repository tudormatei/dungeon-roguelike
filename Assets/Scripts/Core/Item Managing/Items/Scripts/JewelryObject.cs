using UnityEngine;

namespace Core.ItemManagement
{
    public enum JewelryType
    {
        //Amulets
        Fire_Amulet,
        Ice_Amulet,
        Stone_Amulet,
        Electic_Amulet,
        Water_Amulet,
        Wind_Amulet,
        Cloud_Amulet,
        Void_Amulet,
        Magma_Amulet,
        Mecha_Amulet,
        Magic_Amulet,
        Light_Amulet,
        Shadow_Amulet,
        Thief_Amulet,
        //Rings
        Fire_Ring,
        Ice_Ring,
        Stone_Ring,
        Electric_Ring,
        Water_Ring,
        Wind_Ring,
        Cloud_Ring,
        Magma_Ring,
        Void_Ring,
        Mecha_Ring,
        Magic_Ring,
        Light_Ring,
        Shadow_Ring,
        Assasin_Ring
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
