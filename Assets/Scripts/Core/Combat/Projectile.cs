using UnityEngine;
using Core.Enemy;

namespace Core.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool enemyProjectile = false;
        [SerializeField] private bool useGravity = false;

        private Vector3 target;
        private float speed;
        private float damageToDeal;
        private bool isCrit;

        private Fighter fighter;

        public float Speed { get => speed; set => speed = value; }
        public Vector3 Target { get => target; set => target = value; }
        public float DamageToDeal { get => damageToDeal; set => damageToDeal = value; }
        public bool IsCrit { get => isCrit; set => isCrit = value; }

        private void Start()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();

            if (!enemyProjectile)
            {
                Physics.IgnoreLayerCollision(11, 12, true);
            }
            else
            {
                Physics.IgnoreLayerCollision(11, 12, false);
            }

            rb.useGravity = useGravity;
            transform.LookAt(target);
            rb.velocity = transform.forward * speed;
        }

        private void FixedUpdate()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if(enemyHealth != null)
                {
                    enemyHealth.DealDamage(damageToDeal);
                    fighter.DisplayDamage(damageToDeal, isCrit, enemyHealth.transform);
                }

                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
