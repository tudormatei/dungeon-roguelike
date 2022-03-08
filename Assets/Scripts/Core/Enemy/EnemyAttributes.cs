using UnityEngine;

namespace Core.Enemy
{
    public class EnemyAttributes : MonoBehaviour
    {
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackSpeed;

        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    }
}
