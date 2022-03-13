using UnityEngine;
using Core.Combat;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Item/Weapon")]
    public class WeaponObject : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Weapon;
        }

        [Header("Base Options")]
#pragma warning disable
        [SerializeField] private string weaponName = "Unarmed";
#pragma warning restore
        [SerializeField] private GameObject weaponPrefab;
        public float attackSpeed = 1f;
        [SerializeField] private int numberOfAttackAnimations = 1;
        [SerializeField] private float critDamageMultiplier = 1.5f;

        [Header("Ranged Weapon Settings")]
        [SerializeField] private bool isRanged;
        [SerializeField] private bool needsBullets;
        [SerializeField] private GameObject projectilePrefab = null;
        [SerializeField] private float weaponRange;
        [SerializeField] private float projectileLifetime = 5f;
        [SerializeField] private float projectileSpeed = 20f;
        [SerializeField] private Vector3 projectileSpawnOffset;

        [Header("Weapon Position and Rotation Settings")]
        [SerializeField] private Vector3 weaponRotation = Vector3.zero;
        [SerializeField] private Vector3 weaponPosition = Vector3.zero;

        [HideInInspector] public GameObject weaponGO;

        public void Spawn(Transform weaponParent)
        {
            if (weaponPrefab != null)
            {
                weaponGO = Instantiate(weaponPrefab, weaponParent);
                weaponGO.transform.localPosition = weaponPosition;
                weaponGO.transform.localRotation = Quaternion.Euler(weaponRotation);
                weaponGO.GetComponent<Sway>().ActivateSway();
            }
        }

        public void ShootProjectile(Vector3 target, float damageToDeal, bool crit)
        {
            GameObject go = Instantiate(projectilePrefab);
            go.transform.rotation = Quaternion.identity;
            go.transform.position = weaponGO.transform.TransformPoint(projectileSpawnOffset);
            go.GetComponent<Projectile>().Target = target;
            go.GetComponent<Projectile>().Speed = projectileSpeed;
            go.GetComponent<Projectile>().DamageToDeal = damageToDeal;
            go.GetComponent<Projectile>().IsCrit = crit;
            Destroy(go, projectileLifetime);
        }

        public bool IsRanged { get => isRanged; set => isRanged = value; }
        public Vector3 WeaponRotation { get => weaponRotation; set => weaponRotation = value; }
        public Vector3 WeaponOffset { get => weaponPosition; set => weaponPosition = value; }
        public int NumberOfAttackAnimations { get => numberOfAttackAnimations; set => numberOfAttackAnimations = value; }
        public float CritDamageMultiplier { get => critDamageMultiplier; set => critDamageMultiplier = value; }
        public bool NeedsBullets { get => needsBullets; set => needsBullets = value; }
    }

}
