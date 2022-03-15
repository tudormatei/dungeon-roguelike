using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.ItemManagement;

namespace Core.Player
{
    public class CheckSets : MonoBehaviour
    {
        public InventoryObject equipment;
        public Dictionary<Elements, int> elementDic;

        public void CheckSetsBehaviour()
        {
            if (equipment == null) { return; }

            elementDic = new Dictionary<Elements, int>();

            foreach (InventorySlot slot in equipment.container.slots)
            {
                if (slot.ItemObject == null) { continue; }

                Elements key;
                switch (slot.ItemObject.type)
                {
                    case ItemType.Boots:
                        key = ((BootsObject)slot.ItemObject).element;
                        if (elementDic.ContainsKey(key))
                        {
                            elementDic[key] = elementDic[key] + 1;
                        }
                        else
                        {
                            elementDic.Add(key, 1);
                        }
                        break;
                    case ItemType.Chest:
                        key = ((ChestObject)slot.ItemObject).element;
                        if (elementDic.ContainsKey(key))
                        {
                            elementDic[key] = elementDic[key] + 1;
                        }
                        else
                        {
                            elementDic.Add(key, 1);
                        }
                        break;
                    case ItemType.Helmet:
                        key = ((HelmetObject)slot.ItemObject).element;
                        if (elementDic.ContainsKey(key))
                        {
                            elementDic[key] = elementDic[key] + 1;
                        }
                        else
                        {
                            elementDic.Add(key, 1);
                        }
                        break;
                    case ItemType.Weapon:
                        key = ((WeaponObject)slot.ItemObject).element;
                        if (elementDic.ContainsKey(key))
                        {
                            elementDic[key] = elementDic[key] + 1;
                        }
                        else
                        {
                            elementDic.Add(key, 1);
                        }
                        break;
                    case ItemType.Jewelry:
                        key = ((JewelryObject)slot.ItemObject).element;
                        if (elementDic.ContainsKey(key))
                        {
                            elementDic[key] = elementDic[key] + 1;
                        }
                        else
                        {
                            elementDic.Add(key, 1);
                        }
                        break;
                }
            }

            SetBonuses();
        }

        private void SetBonuses()
        {
            foreach (Elements key in elementDic.Keys)
            {
                Debug.Log("For element " + key.ToString() + " there is a count of " + elementDic[key]);
            }
        }
    }
}
