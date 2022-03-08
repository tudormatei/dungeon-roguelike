using Dungeon.Saving;
using UnityEngine;
using Dungeon.DungeonGeneration;

namespace Core.Player
{
    public class FollowCamera : MonoBehaviour, ISaveable
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            transform.position = target.position;
        }

        public object CaptureState()
        {
            return new SerializeableQuaternion(transform.localRotation) ;
        }

        public void RestoreState(object state)
        {
            SerializeableQuaternion rot = (SerializeableQuaternion)state;
            transform.localRotation = rot.ToQuaternion();
        }
    }
}
