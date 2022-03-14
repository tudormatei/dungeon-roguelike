using System.Collections.Generic;
using UnityEngine;
using Core.Player;

namespace Core.ItemManagement
{
    public enum Elements
    {
        Fire,
        Ice,
        Stone,
        Electic,
        Water,
        Wind,
        Cloud,
        Void,
        Magma,
        Mecha,
        Magic,
        Light,
        Shadow
    }

    public enum ItemType
    {
        Food,
        Jewelry,
        Helmet,
        Weapon,
        Chest,
        Boots,
        Default
    }

    public enum Attributes
    {
        //Weapon
        Damage,
        Skill_Damage,
        Attack_Speed,
        Critical_Chance,
        BleedChance,

        //Chest
        Defense,
        Max_Health,
        Max_Mana,

        //Boots
        Chance_To_Dodge,
        Projectile_Resistance,
        Movement_Speed,

        //Helmet
        Health_Regeneration,
        Mana_Regeneration,
        Chance_To_Reflect_Damage,

        //Jewelry

    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public abstract class ItemObject : ScriptableObject
    {
        public Sprite uIDisplay;
        public bool stackable;
        public ItemType type;
        public Rarity rarity;
        [TextArea(15, 20)]
        public string description;
        public Item data = new Item();

        public Item CreateItem()
        {
            Item item = new Item(this);
            return item;
        }
    }

    [System.Serializable]
    public class Item
    {
        [NonReorderable]
        public string name;
        [NonReorderable]
        public int id = -1;
        [NonReorderable]
        public ItemBuff[] buffs;
        [NonReorderable]
        public List<int> dropProcentages;

        public Item()
        {
            name = "";
            id = -1;
        }

        public Item(ItemObject item)
        {
            name = item.name;
            id = item.data.id;
            buffs = new ItemBuff[item.data.buffs.Length];
            for (int i = 0; i < item.data.buffs.Length; i++)
            {
                buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max, item.data.buffs[i].dropProcentages);
                buffs[i].attribute = item.data.buffs[i].attribute;
            }
        }
    }

    [System.Serializable]
    public class ItemBuff : IModifires
    {
        public Attributes attribute;
        public int value;
        public int min;
        public int max;
        public List<int> dropProcentages;

        public ItemBuff(int _min, int _max, List<int> _dropProcentages)
        {
            min = _min;
            max = _max;
            dropProcentages = _dropProcentages;

            value = GenerateValue();
        }

        public int GenerateValue()
        {
            int lastCurrent = min;
            int current;
            for (int i = 0; i < dropProcentages.Count; i++)
            {
                current = (int)max / (dropProcentages.Count - i);
                float rand = Random.value;
                float compare = dropProcentages[i] / 100f;
                if (rand >= compare)
                {
                    int value = Random.Range(lastCurrent, current);
                    return value;
                }
                else
                {
                    lastCurrent = current;
                }
            }
            return Random.Range(lastCurrent, max);
        }

        public void AddValue(ref int baseValue)
        {
            baseValue += value;
        }
    }
}
