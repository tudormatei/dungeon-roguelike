using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Core.ItemManagement
{
    public class DynamicInterface : UserInterface
    {
        public GameObject inventoryPrefab;

        public int X_START;
        public int Y_START;
        public int X_SPACE_BETWEEN_ITEMS;
        public int NUMBER_OF_COLUMNS;
        public int Y_SPACE_BETWEEN_ITEMS;

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

            for (int i = 0; i < inventory.getSlots.Length; i++)
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                inventory.getSlots[i].slotDisplay = obj;

                slotsOnInterface.Add(obj, inventory.getSlots[i]);
            }
        }

        public override void OnSlotUpdate(InventorySlot _slot)
        {
            if (_slot.item.id >= 0)
            {
                if (_slot.slotDisplay == null) { return; }

                Image image = _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>();
                if (image != null)
                    image.sprite = _slot.ItemObject.uIDisplay;
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            }
            else
            {
                if (_slot.slotDisplay == null) { return; }

                Image image = _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>();
                if (image != null)
                    image.sprite = defaultUIMask;
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        private Vector3 GetPosition(int i)
        {
            return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
        }
    }
}
