using UnityEngine;
using TMPro;
using Core.Player;
using Core.ItemManagement;
using System.Collections.Generic;

namespace Dungeon.UI
{
    /// <summary>
    /// UI attribute panel display player equipment attributes.
    /// </summary>
    public class AttributePanel : MonoBehaviour
    {
        [Header("Attribute Settings")]
        [SerializeField] private GameObject statPrefab;
        [SerializeField] private float stepSize = 50f;

        private float startPosY = 275f;
        private float startY;
        private PlayerAttributes playerAttributes;
        private List<GameObject> stats;

        private void Awake()
        {
            playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        }

        private void OnEnable()
        {
            DisplayAttributes();
        }

        private void DisplayAttributes()
        {
            startY = startPosY;
            stats = new List<GameObject>();

            foreach(Attribute at in playerAttributes.attributes)
            {
                GameObject go = Instantiate(statPrefab, transform);
                stats.Add(go);
                go.transform.localPosition = new Vector3(0f, startY, 0f);
                startY -= stepSize;
                TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();

                float value = playerAttributes.defaultAttributes[at.type.ToString()] + playerAttributes.GetAttributeModifiedValue(at.type);

                if (at.type == Attributes.Health_Regeneration)
                {
                    text.text = at.type.ToString().Replace("_", " ") + ": " + value.ToString() + " %\nof max health";
                    continue;
                }

                text.text = at.type.ToString().Replace("_", " ") + ": " + value.ToString();
            }
        }

        private void OnDisable()
        {
            foreach(GameObject go in stats)
            {
                Destroy(go);
            }
        }
    }
}
