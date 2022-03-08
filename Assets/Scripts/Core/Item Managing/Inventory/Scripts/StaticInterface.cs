using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Core.ItemManagement
{
    public class StaticInterface : UserInterface
    {
        public GameObject[] slots;
        public Sprite helmetDef;
        public Sprite chestDef;
        public Sprite bootsDef;
        public Sprite weaponDef;
        public Sprite jewelryDef;

        private string slotName = "SlotPrefab";

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

                if (image == null) { return; }

                //Can check whether the item type is correct and assign the icon for the default slot

                if (_slot.slotDisplay.name == slotName)
                {
                    //helmet
                    image.sprite = helmetDef;
                }
                else if (_slot.slotDisplay.name == slotName + " (1)")
                {
                    //weapon
                    image.sprite = weaponDef;
                }
                else if (_slot.slotDisplay.name == slotName + " (2)")
                {
                    //ring
                    image.sprite = jewelryDef;
                }
                else if (_slot.slotDisplay.name == slotName + " (3)")
                {
                    //chest
                    image.sprite = chestDef;
                }
                else if (_slot.slotDisplay.name == slotName + " (4)")
                {
                    //boots
                    image.sprite = bootsDef;
                }
                else
                {
                    image.sprite = defaultUIMask;
                }

                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

            for (int i = 0; i < inventory.getSlots.Length; i++)
            {
                var obj = slots[i];

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                inventory.getSlots[i].slotDisplay = obj;

                slotsOnInterface.Add(obj, inventory.getSlots[i]);
            }
        }
    }
}
