using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dungeon.DungeonGeneration;
using TMPro;
using UnityEngine.UI;

namespace Dungeon.UI
{
    /// <summary>
    /// Loading dungeon UI manager
    /// Loading bar
    /// Random prompts
    /// </summary>
    public class LoadingDungeonMenu : MonoBehaviour
    {
        [Header("Loading Settings")]
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] private Slider progressBar;
        [SerializeField] private GameObject inventoryGameobject;

        [Header("Generation of text")]
        [SerializeField] private List<string> generationPrompts;

        private void Start()
        {
            loadingPanel.SetActive(true);
            progressBar.interactable = false;
            progressBar.value = 0f;

            StartCoroutine(LoadDungeon());
        }

        private IEnumerator LoadDungeon()
        {
            yield return new WaitUntil(() => DungeonGenerator.dungeonState == DungeonState.generatingMain);
            StartCoroutine(MoveSlider(.5f, 1f));
            string text2 = generationPrompts[Random.Range(0, generationPrompts.Count)];
            generationPrompts.Remove(text2);
            loadingText.text = text2;
            
            yield return new WaitUntil(() => DungeonGenerator.dungeonState == DungeonState.generatingBranches);
            StartCoroutine(MoveSlider(1f, 1f));
            string text3 = generationPrompts[Random.Range(0, generationPrompts.Count)];
            generationPrompts.Remove(text3);
            loadingText.text = text3;

            inventoryGameobject.SetActive(false);
            yield return new WaitUntil(() => DungeonGenerator.dungeonState == DungeonState.completed);
            StartCoroutine(MoveSlider(1f, 1f));
            string text4 = generationPrompts[Random.Range(0, generationPrompts.Count)];
            generationPrompts.Remove(text4);
            loadingText.text = text4;

            loadingPanel.SetActive(false);
        }

        private IEnumerator MoveSlider(float value, float sliderTime)
        {
            var startValue = progressBar.value;
            var endValue = value;
            float lerp = 0f;

            while (lerp < sliderTime)
            {
                lerp += Time.deltaTime;
                progressBar.value = Mathf.SmoothStep(startValue, endValue, lerp);
                yield return null;
            }

            progressBar.value = endValue;
        }
    }
}
