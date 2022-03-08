using UnityEngine;
using UnityEngine.AI;
using Dungeon.DungeonGeneration;

namespace Core.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float rotateSpeedToTarget = 3f;

        private Transform player;
        private NavMeshAgent navMeshAgent;

        private Vector3 guardPosition;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (DungeonGenerator.dungeonState != DungeonState.completed) { return; }

            if (DistanceToPlayer() < chaseDistance)
            {
                TriggerEnemy();
                FaceTarget(player.position);
            }
            else if (transform.position != guardPosition)
            {
                navMeshAgent.SetDestination(guardPosition);
                //FaceTarget(guardPosition);
            }
        }

        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeedToTarget);
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.position, transform.position);
        }

        public void TriggerEnemy()
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

}
