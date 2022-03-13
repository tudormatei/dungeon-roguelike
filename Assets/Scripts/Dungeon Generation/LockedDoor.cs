using UnityEngine;
using Core.ItemManagement;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Door that has a key.
    /// </summary>
    public class LockedDoor : MonoBehaviour
    {
        [Header("Key Settings")]
        public int keyHash;

        private Animator animator;
        private Controls controls;
        private bool isInZone;

        private Interract playerInterract;

        #region Setup
        private void Awake()
        {
            animator = GetComponent<Animator>();
            controls = new Controls();
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
            bool openDoor = false;
            if (playerInterract == null) { return; }
            InventoryObject inv = playerInterract.inventory;
            if (inv == null) { return; }
            foreach (InventorySlot slot in inv.container.slots)
            {
                if (slot.ItemObject == null) { continue; }

                if (((DefaultObject)slot.ItemObject).isKey)
                {
                    if (((DefaultObject)slot.ItemObject).keyHash == keyHash)
                    {
                        openDoor = true;
                        slot.RemoveItem();
                        break;
                    }
                }
            }

            if (!openDoor) { return; }

            if (isInZone)
            {
                animator.SetTrigger("open");
            }
        }

        #region Collision
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (playerInterract == null)
                    playerInterract = other.GetComponent<Interract>();

                isInZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerInterract = null;

                isInZone = false;
            }
        }
        #endregion
    }
}
