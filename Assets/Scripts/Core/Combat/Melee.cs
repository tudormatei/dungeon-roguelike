using System.Collections.Generic;
using UnityEngine;
using Core.Enemy;

namespace Core.Combat
{
    public class Melee : MonoBehaviour
    {
        private Collider col;
        private Fighter fighter;
        private List<Collider> enemyColliders;

        private bool canDamage;

        private void Start()
        {
            col = GetComponent<Collider>();

            canDamage = false;
            col.enabled = false;

            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        public void StartAttack()
        {
            col.enabled = true;
            canDamage = true;
            enemyColliders = new List<Collider>();
        }

        public void StopAttack()
        {
            col.enabled = false;
            canDamage = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!canDamage) { return; }

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (!enemyColliders.Contains(other))
                {
                    DealDamage(other);
                    AddEnemies(other);
                }
            }
        }

        private void AddEnemies(Collider col)
        {
            enemyColliders.Add(col);
        }

        private void DealDamage(Collider col)
        {
            EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float damage;
                bool crit;
                (damage, crit) = fighter.CalculateDamage();
                enemyHealth.DealDamage(damage);
                fighter.DisplayDamage(damage, crit, enemyHealth.transform);
            }
        }
    }
}
