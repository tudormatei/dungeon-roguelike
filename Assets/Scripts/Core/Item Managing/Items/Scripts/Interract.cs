using System.Collections;
using UnityEngine;
using Dungeon.DungeonGeneration;
using Dungeon.UI;
using Dungeon.Audio;
using Core.Combat;

namespace Core.ItemManagement
{
    public class Interract : MonoBehaviour
    {
        [Header("Interraction")]
        [SerializeField] private GameObject crosshair;
        [SerializeField] private GameObject grabIcon;
        private bool hasChangedToGrabIcon = false;
        [SerializeField] private LayerMask layerGrab;
        [SerializeField] private float pickUpRange = 5f;

        [Header("Middle Variables")]
        public static bool panelOpen = false;
        private Controls controls;
        private Fighter fighter;

        [Header("Inventory")]
        public InventoryObject inventory;
        [SerializeField] private GameObject inventoryPanel;
        private bool inventoryOpen = false;

        [Header("Attributes")]
        [SerializeField] private GameObject attributePanel;
        private bool attributeOpen = false;

        private void Awake()
        {
            controls = new Controls();
        }

        private void Start()
        {
            crosshair.SetActive(true);
            grabIcon.SetActive(false);
            fighter = GetComponent<Fighter>();

            controls.Gameplay.Interract.performed += ctx => InterractWithItem();
            controls.Gameplay.Inventory.performed += ctx => OpenInventory();
            controls.Gameplay.Attributes.performed += ctx => OpenAttributes();
        }

        private void Update()
        {
            ChangeIcon();
        }

        private void ChangeIcon()
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), pickUpRange, LayerMask.GetMask("Interractable")))
            {
                if (hasChangedToGrabIcon) { return; }

                hasChangedToGrabIcon = true;
                crosshair.SetActive(false);
                grabIcon.SetActive(true);
                return;
            }

            if (!hasChangedToGrabIcon) { return; }

            hasChangedToGrabIcon = false;
            crosshair.SetActive(true);
            grabIcon.SetActive(false);
        }

        private void InterractWithItem()
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit, pickUpRange, LayerMask.GetMask("Interractable")))
            {
                GroundItem item = hit.collider.GetComponent<GroundItem>();
                if (item)
                {
                    if (inventory.AddItem(new Item(item.item), 1))
                    {
                        GameObject.Find("Canvas").GetComponent<AudioManager>().TriggerSoundEffect(1);
                        UpdateAmmo();
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

        private void UpdateAmmo()
        {
            foreach (InventorySlot slot in inventory.container.slots)
            {
                if (slot.ItemObject == null) { continue; }

                if (slot.ItemObject.name.Contains("Bullet"))
                {
                    fighter.UpdateAmmoCounter();
                }
            }
        }

        private readonly float panelOpenSpeed = 0.15f;
        private void OpenInventory()
        {
            if (DungeonGenerator.dungeonState != DungeonState.completed) { return; }
            if (PauseMenu.isPaused) { return; }
            if (attributeOpen) { return; }

            if (!panelOpen)
            {
                StopAllCoroutines();
                StartCoroutine(LerpInventoryPanel(panelOpenSpeed, new Vector3(1f, 1f, 1f), true, inventoryPanel));
                StartCoroutine(LerpAttributePanel(panelOpenSpeed, Vector3.zero, false, attributePanel));

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                inventoryOpen = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(LerpInventoryPanel(panelOpenSpeed, Vector3.zero, false, inventoryPanel));

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryOpen = false;
            }

            panelOpen = !panelOpen;
        }

        private IEnumerator LerpInventoryPanel(float duration, Vector3 endValue, bool open, GameObject panel)
        {
            panel.SetActive(true);

            Vector3 startValue = panel.transform.localScale;
            float time = 0f;

            while (time < duration)
            {
                panel.transform.localScale = Vector3.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            panel.transform.localScale = endValue;

            if (!open)
            {
                panel.SetActive(false);
            }
        }

        private void OpenAttributes()
        {
            if (DungeonGenerator.dungeonState != DungeonState.completed) { return; }
            if (PauseMenu.isPaused) { return; }
            if (inventoryOpen) { return; }

            if (!panelOpen)
            {
                StopAllCoroutines();
                StartCoroutine(LerpAttributePanel(panelOpenSpeed, new Vector3(1f, 1f, 1f), true, attributePanel));
                StartCoroutine(LerpInventoryPanel(panelOpenSpeed, Vector3.zero, false, inventoryPanel));

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                attributeOpen = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(LerpAttributePanel(panelOpenSpeed, Vector3.zero, false, attributePanel));

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                attributeOpen = false;
            }

            panelOpen = !panelOpen;
        }

        private IEnumerator LerpAttributePanel(float duration, Vector3 endValue, bool open, GameObject panel)
        {
            panel.SetActive(true);

            Vector3 startValue = panel.transform.localScale;
            float time = 0f;

            while (time < duration)
            {
                panel.transform.localScale = Vector3.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            panel.transform.localScale = endValue;

            if (!open)
            {
                panel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
