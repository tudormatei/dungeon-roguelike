using UnityEngine;

namespace Core.ItemManagement
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] itemObjects;

        [ContextMenu("Update ID's")]
        public void UpdateID()
        {
            for (int i = 0; i < itemObjects.Length; i++)
            {
                if (itemObjects[i].data.id != i)
                    itemObjects[i].data.id = i;
            }
        }

        public void OnAfterDeserialize()
        {
            UpdateID();
        }

        public void OnBeforeSerialize()
        {

        }
    }
}
