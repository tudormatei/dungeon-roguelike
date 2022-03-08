using UnityEngine;

namespace Dungeon.UI
{
    /// <summary>
    /// Gameobject looks at player.
    /// </summary>
    public class SpriteLookAtPlayer : MonoBehaviour
    {
        [SerializeField] private bool onStart = false;

        private Transform mainCamera;
        private bool startRotate = false;

        private void Start()
        {
            mainCamera = Camera.main.transform;
            if (onStart)
            {
                StartRotation();
            }
        }

        public void StartRotation()
        {
            startRotate = true;
        }

        private void LateUpdate()
        {
            if (!startRotate) { return; }

            transform.LookAt(mainCamera.position, Vector3.up);
        }
    }
}
