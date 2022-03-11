using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Globalization;
using Dungeon.UI;

namespace Core.ItemManagement
{
    public enum CanDestroyItem
    {
        Yes,
        No,
        Default
    }

    public abstract class UserInterface : MonoBehaviour
    {
        [SerializeField] private GameObject destroyItemPanel;
        [SerializeField] private GameObject cantDestroyEquipmentPanel;
        private bool result = false;
        private CanDestroyItem panelResult = CanDestroyItem.Default;
        public Sprite defaultUIMask;
        public InventoryObject inventory;

        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        private Controls controls;

        public Transform parent;
        private GameObject description;
        public GameObject descriptionPrefab;
        private Transform canvas;
        private bool dragging;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        private void CreateDescription(InventorySlot hoveringItem)
        {
            Transform _trans;
            if (parent == null)
                _trans = canvas;
            else
                _trans = parent;
            
            Vector2 mousePos = controls.Gameplay.MousePosition.ReadValue<Vector2>();
            Vector2 _pos = new Vector2(mousePos.x, mousePos.y);

            if (_pos.x > Screen.width / 2)
                _pos.x = mousePos.x - 250f;
            else
                _pos.x = mousePos.x + 250f;

            if (_pos.y > Screen.height / 2)
                _pos.y = mousePos.y - 200f;
            else
                _pos.y = mousePos.y + 200f;

            description = Instantiate(descriptionPrefab, Vector2.zero, Quaternion.identity, _trans);

            description.transform.position = _pos;

            string _buffs = "";
            for (int i = 0; i < hoveringItem.item.buffs.Length; i++)
            {
                _buffs = string.Concat(_buffs, " ", hoveringItem.item.buffs[i].attribute.ToString(), " +", hoveringItem.item.buffs[i].value.ToString(), "%", Environment.NewLine);
            }
            _buffs = _buffs.Replace("_", " ");

            var _item = hoveringItem.item;

            Debug.Log(hoveringItem.ItemObject.description);
            Debug.Log(hoveringItem.item.name);

            description.GetComponent<Description>().AssignValues(_item.name, hoveringItem.ItemObject.description, textInfo.ToTitleCase(_buffs), hoveringItem.ItemObject.uIDisplay, hoveringItem.ItemObject.rarity);
        }

        private void Awake()
        {
            controls = new Controls();

            for (int i = 0; i < inventory.getSlots.Length; i++)
            {
                inventory.getSlots[i].parent = this;
                inventory.getSlots[i].OnAfterUpdate += OnSlotUpdate;
            }

            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
        }

        public abstract void OnSlotUpdate(InventorySlot _slot);

        public abstract void CreateSlots();

        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoveredOver = obj;

            if (!dragging)
            {
                InventorySlot hoveringItem = slotsOnInterface[obj];
                if (hoveringItem.item.id >= 0)
                {
                    CreateDescription(hoveringItem);
                }
            }
        }

        public void OnExit(GameObject obj)
        {
            MouseData.slotHoveredOver = null;
            if (description != null)
                Destroy(description);
        }

        public void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }

        public void OnExitInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = null;
        }

        public void OnDragStart(GameObject obj)
        {
            dragging = true;

            MouseData.tempItemBeingDragged = CreateTempItem(obj);
        }

        public GameObject CreateTempItem(GameObject obj)
        {
            GameObject tempItem = null;
            if (slotsOnInterface[obj].item.id >= 0)
            {
                tempItem = new GameObject();
                var rt = tempItem.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(50, 50);
                tempItem.transform.SetParent(transform.parent);
                var image = tempItem.AddComponent<Image>();
                image.sprite = slotsOnInterface[obj].ItemObject.uIDisplay;
                image.raycastTarget = false;
            }
            return tempItem;
        }

        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.tempItemBeingDragged);

            dragging = false;

            if (MouseData.interfaceMouseIsOver == null)
            {
                InventorySlot hoveringItem = slotsOnInterface[obj];
                if (hoveringItem.item.id <= -1) { dragging = false; return; }

                if (inventory.type == InterfaceType.Equipment) { StartCoroutine(CantDestroyEquipedItems()); }
                else
                {
                    StartCoroutine(DestroyItemSlot(obj));
                }
                return;
            }
            if (MouseData.slotHoveredOver && MouseData.interfaceMouseIsOver)
            {
                InventorySlot mouseSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                inventory.SwapItems(slotsOnInterface[obj], mouseSlotData);
                if (description != null)
                    Destroy(description);
            }
        }

        private IEnumerator DestroyItemSlot(GameObject obj)
        {
            destroyItemPanel.SetActive(true);
            panelResult = CanDestroyItem.Default;
            yield return new WaitUntil(() => panelResult == CanDestroyItem.Yes || panelResult == CanDestroyItem.No);
            switch (panelResult)
            {
                case CanDestroyItem.Yes:
                    panelResult = CanDestroyItem.Default;
                    destroyItemPanel.SetActive(false);
                    slotsOnInterface[obj].RemoveItem();
                    OnSlotUpdate(slotsOnInterface[obj]);
                    break;
                case CanDestroyItem.No:
                    panelResult = CanDestroyItem.Default;
                    destroyItemPanel.SetActive(false);
                    break;
                case CanDestroyItem.Default:
                    break;
                default:
                    break;
            }
        }

        private IEnumerator CantDestroyEquipedItems()
        {
            cantDestroyEquipmentPanel.SetActive(true);
            result = false;
            yield return new WaitUntil(() => result);
            result = false;
            cantDestroyEquipmentPanel.SetActive(false);
        }

        public void DestroyItem(bool b)
        {
            if (b) { panelResult = CanDestroyItem.Yes; }
            else { panelResult = CanDestroyItem.No; }
        }

        public void CantDestroyItem()
        {
            result = true;
        }

        public void OnDrag(GameObject obj)
        {
            if (MouseData.tempItemBeingDragged != null)
            {
                Vector2 mousePos = controls.Gameplay.MousePosition.ReadValue<Vector2>();
                MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = mousePos;
            }
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            Destroy(description);

            controls.Disable();
        }
    }

    public static class MouseData
    {
        public static UserInterface interfaceMouseIsOver;
        public static GameObject tempItemBeingDragged;
        public static GameObject slotHoveredOver;
    }

    public static class ExtentionMethods
    {
        public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
            {
                if (_slot.Value.item.id >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uIDisplay;
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}
