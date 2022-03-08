using UnityEngine;
using System.Collections.Generic;
using Core.Combat;
using Core.ItemManagement;

namespace Core.Player
{
    public class PlayerAttributes : MonoBehaviour
    {
        public Dictionary<string, float> defaultAttributes = new Dictionary<string, float>();
        public InventoryObject equipment;
        public Attribute[] attributes;

        private Fighter fighter;

        private void Awake()
        {
            SetupDefaultAttributes();
            fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }
            for (int i = 0; i < equipment.getSlots.Length; i++)
            {
                equipment.getSlots[i].OnBeforeUpdate += OnRemoveItem;
                equipment.getSlots[i].OnAfterUpdate += OnAddItem;
            }
        }

        private void SetupDefaultAttributes()
        {
            foreach (Attribute a in attributes)
            {
                defaultAttributes.Add(a.type.ToString(), a.value.baseValue);
            }
        }

        public void OnRemoveItem(InventorySlot _slot)
        {
            if (_slot.ItemObject == null) { return; }

            switch (_slot.parent.inventory.type)
            {
                case InterfaceType.Inventroy:
                    break;
                case InterfaceType.Equipment:

                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].attribute)
                            {
                                attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                            }
                        }
                    }

                    if (_slot.ItemObject.type == ItemType.Weapon)
                    {
                        fighter.UnequipWeapon();
                    }
                    else if (_slot.ItemObject.type == ItemType.Jewelry)
                    {
                        fighter.UnequipRing();
                    }

                    break;
                case InterfaceType.Chest:
                    break;
                default:
                    break;
            }
        }

        public void OnAddItem(InventorySlot _slot)
        {
            if (_slot.ItemObject == null) { return; }

            switch (_slot.parent.inventory.type)
            {
                case InterfaceType.Inventroy:
                    break;
                case InterfaceType.Equipment:

                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].attribute)
                            {
                                attributes[j].value.AddModifier(_slot.item.buffs[i]);
                            }
                        }
                    }

                    if (_slot.ItemObject.type == ItemType.Weapon)
                    {
                        fighter.EquipWeapon((WeaponObject)_slot.ItemObject);
                    }
                    else if(_slot.ItemObject.type == ItemType.Jewelry)
                    {
                        fighter.EquipRing((JewelryObject)_slot.ItemObject);
                    }

                    break;
                case InterfaceType.Chest:
                    break;
                default:
                    break;
            }
        }

        public float GetAttributeModifiedValue(Attributes attribute)
        {
            foreach (Attribute a in attributes)
            {
                if (a.type == attribute)
                {
                    return a.value.ModifiedValue;
                }
            }

            return 0f;
        }

        public void AttributeModified(Attribute attribute)
        {

        }
    }

}
