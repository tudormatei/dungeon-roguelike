using UnityEngine;
using Core.ItemManagement;
using Core.Utils;

namespace Core.Loader
{
    public class InventoryLoader : MonoBehaviour
    {
        [SerializeField] private InventoryObject inventory;
        [SerializeField] private InventoryObject equipment;

        private void Start()
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            if (!PersistentData.Instance.newGame)
            {
                inventory.Load();
                equipment.Load();
            }
            else
            {
                inventory.Clear();
                equipment.Clear();
            }
        }

        public void SaveInventory()
        {
            inventory.Save();
            equipment.Save();
        }

        private void OnApplicationQuit()
        {
            inventory.Clear();
            equipment.Clear();
        }
    }

}
