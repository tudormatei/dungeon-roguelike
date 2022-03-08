using UnityEngine;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Detects if player can open door.
    /// </summary>
    public class ToggleDoor : MonoBehaviour
    {
        private Animator animator;

        private bool isInZone;
        private Controls controls;
        private Door doorScript;

        #region Setup
        private void Awake()
        {
            animator = GetComponent<Animator>();
            controls = new Controls();

            doorScript = GetComponent<Door>();
        }

        private void Start()
        {
            controls.Gameplay.Interract.performed += ctx => OpenDoor();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
        #endregion

        public void OpenDoor()
        {
            if (isInZone)
            {
                doorScript.TurnOffCollider();

                bool isOpen = animator.GetBool("isOpen");
                animator.SetBool("isOpen", !isOpen);
            }
        }

        #region Collision
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                isInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isInZone = false;
            }
        }
        #endregion
    }
}

