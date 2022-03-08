using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using Core.Player;

namespace Core.ItemManagement
{
    public enum InterfaceType
    {
        Inventroy,
        Equipment,
        Chest
    }

    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {

        public string savePath;
        public ItemDatabaseObject database;
        public InterfaceType type;
        public Inventory container;
        public InventorySlot[] getSlots { get { return container.slots; } }


        public bool AddItem(Item _item, int _amount)
        {
            if (EmptySlotCount <= 0) { return false; }

            InventorySlot slot = FindItemOnInventory(_item);
            if (!database.itemObjects[_item.id].stackable || slot == null)
            {
                SetEmptySlot(_item, _amount);
                return true;
            }
            slot.AddAmount(_amount);
            return true;
        }

        public InventorySlot FindItemOnInventory(Item _item)
        {
            for (int i = 0; i < getSlots.Length; i++)
            {
                if (getSlots[i].item.id == _item.id)
                {
                    return getSlots[i];
                }
            }

            return null;
        }

        public int EmptySlotCount
        {
            get
            {
                int counter = 0;
                for (int i = 0; i < getSlots.Length; i++)
                {
                    if (getSlots[i].item.id <= -1)
                    {
                        counter++;
                    }
                }
                return counter;
            }
        }

        public InventorySlot SetEmptySlot(Item _item, int amount)
        {
            for (int i = 0; i < getSlots.Length; i++)
            {
                if (getSlots[i].item.id <= -1)
                {
                    getSlots[i].UpdateSlot(_item, amount);
                    return getSlots[i];
                }
            }

            return null;
        }

        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            }
        }

        public void RemoveItem(Item _item)
        {
            for (int i = 0; i < getSlots.Length; i++)
            {
                if (getSlots[i].item == _item)
                {
                    getSlots[i].UpdateSlot(null, 0);
                }
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            /*string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            bf.Serialize(file, saveData);
            file.Close();*/

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, container);
            stream.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                /*BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();*/

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                Inventory newContainer = (Inventory)formatter.Deserialize(stream);
                for (int i = 0; i < getSlots.Length; i++)
                {
                    getSlots[i].UpdateSlot(newContainer.slots[i].item, newContainer.slots[i].amount);
                }
                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            container.Clear();
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] slots = new InventorySlot[35];
        public void Clear()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].RemoveItem();
            }
        }
    }

    public delegate void SlotUpdated(InventorySlot _slot);

    [System.Serializable]
    public class InventorySlot
    {
        [NonReorderable]
        public ItemType[] allowedItems = new ItemType[0];
        [NonReorderable]
        [System.NonSerialized]
        public UserInterface parent;
        [NonReorderable]
        [System.NonSerialized]
        public GameObject slotDisplay;
        [NonReorderable]
        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;
        [NonReorderable]
        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;
        [NonReorderable]
        public int amount;
        [NonReorderable]
        public Item item = new Item();

        public ItemObject ItemObject
        {
            get
            {
                if (item.id >= 0)
                {
                    return parent.inventory.database.itemObjects[item.id];
                }
                return null;
            }
        }

        public InventorySlot()
        {
            UpdateSlot(new Item(), 0);
        }

        public InventorySlot(Item _item, int _amount)
        {
            UpdateSlot(_item, _amount);
        }

        public void UpdateSlot(Item _item, int _amount)
        {
            if (OnBeforeUpdate != null)
            {
                OnBeforeUpdate.Invoke(this);
            }

            item = _item;
            amount = _amount;

            if (OnAfterUpdate != null)
            {
                OnAfterUpdate.Invoke(this);
            }
        }

        public void RemoveItem()
        {
            UpdateSlot(new Item(), 0);
        }

        public void AddAmount(int value)
        {
            UpdateSlot(item, amount += value);
        }

        public bool CanPlaceInSlot(ItemObject _itemObject)
        {
            if (allowedItems.Length <= 0 || _itemObject == null || _itemObject.data.id < 0)
            {
                return true;
            }

            for (int i = 0; i < allowedItems.Length; i++)
            {
                if (_itemObject.type == allowedItems[i])
                    return true;
            }

            return false;
        }
    }
}
