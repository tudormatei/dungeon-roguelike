using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Core.ItemManagement;
using Core.Utils;

namespace Dungeon.UI
{
    /// <summary>
    /// Item description UI manager.
    /// </summary>
    public class Description : MonoBehaviour
    {
        [Header("Description Properties")]
        public TextMeshProUGUI itemName, description, buffs, rarityText;
        public Image item;

        [HideInInspector]
        public static Description Instance;

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.LogError("Error: There are multiple descriptions present, deleting old one");
                Destroy(Instance);
                Instance = this;
            }
        }

        private void OnDisable()
        {
            Instance = null;
        }

        public void AssignValues(string _name, string _description, string _buffs, Sprite _itemSprite, Rarity rarity)
        {
            itemName.text = _name;
            description.text = _description;
            buffs.text = _buffs;
            item.sprite = _itemSprite;

            switch (rarity)
            {
                case Rarity.Common:
                    rarityText.text = "Common";
                    rarityText.color = Colors.commonColor;
                    break;
                case Rarity.Uncommon:
                    rarityText.text = "Uncommon";
                    rarityText.color = Colors.uncommonColor;
                    break;
                case Rarity.Rare:
                    rarityText.text = "Rare";
                    rarityText.color = Colors.rareColor;
                    break;
                case Rarity.Epic:
                    rarityText.text = "Epic";
                    rarityText.color = Colors.epicColor;
                    break;
                case Rarity.Legendary:
                    rarityText.text = "Legendary";
                    rarityText.color = Colors.legendaryColor;
                    break;
                default:
                    break;
            }
        }
    }
}
