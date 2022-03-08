using UnityEngine;
using UnityEngine.AI;
using Core.Player;

namespace Core.Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        private Transform player;
        private NavMeshAgent navMeshAgent;
        private PlayerHealth playerHealth;

        private float timeSinceLastAttack = 0f;

        private float damage;
        private float attackSpeed;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerHealth = player.GetComponent<PlayerHealth>();

            damage = GetComponent<EnemyAttributes>().AttackDamage;
            attackSpeed = GetComponent<EnemyAttributes>().AttackSpeed;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (DistanceToPlayer() < navMeshAgent.stoppingDistance && timeSinceLastAttack >= attackSpeed)
            {
                AttackPlayer(damage);
                timeSinceLastAttack = 0f;
            }
        }

        private void AttackPlayer(float amount)
        {
            playerHealth.DealDamage(amount, GetComponent<EnemyAttributes>());
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.position, transform.position);
        }
    }
}
