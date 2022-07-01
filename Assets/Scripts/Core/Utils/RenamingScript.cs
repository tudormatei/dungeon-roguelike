using System.Collections.Generic;
using UnityEngine;
using Core.ItemManagement;

namespace Core.Utils
{
    public class RenamingScript : MonoBehaviour, ISerializationCallbackReceiver
    {
        [ContextMenu("RENAME!")]

        public void Rename()
        {
            List<GroundItem> gs = new List<GroundItem>();

            foreach (GroundItem x in GetComponentsInChildren<GroundItem>())
            {
                gs.Add(x);
            }

            foreach (GroundItem i in gs)
            {
                string name = "";
                Rarity rarity = i.item.rarity;
                Elements element = ((BootsObject)i.item).element;
                ItemType type = i.item.type;
                name = rarity.ToString() + " " + element.ToString() + " " + type.ToString() + " Pickup";
                i.gameObject.name = name;
                print(name);
            }
        }

        public void OnAfterDeserialize()
        {
            Rename();
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
