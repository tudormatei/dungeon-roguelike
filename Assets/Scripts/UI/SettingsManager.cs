using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dungeon.UI
{
    /// <summary>
    /// Settings UI manager.
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        [Header("Settings Properties")]
        [SerializeField] private GameObject buttons;
        [SerializeField] private GameObject saveButton;
        [SerializeField] private GameObject graphicsPanel;
        [SerializeField] private GameObject audioPanel;
        [SerializeField] private GameObject backButton;
        [SerializeField] private Animator cameraAnimator;

        private bool inAudio, inGraphics;
        private bool runOnce = false;

        private void Start()
        {
            saveButton.SetActive(false);
            graphicsPanel.SetActive(false);
            backButton.SetActive(false);

            inAudio = false;
            inGraphics = false;

            buttons.SetActive(true);
        }

        private void Update()
        {
            if (inGraphics && runOnce)
            {
                runOnce = false;
                StartCoroutine(ActivateGraphics());
            }
            else if (inAudio && runOnce)
            {
                runOnce = false;
                StartCoroutine(ActivateAudio());
            }
        }

        private IEnumerator ActivateGraphics()
        {
            yield return new WaitForSeconds(1f);
            backButton.SetActive(true);
            saveButton.SetActive(true);
            graphicsPanel.SetActive(true);
        }

        private IEnumerator ActivateAudio()
        {
            yield return new WaitForSeconds(1f);
            backButton.SetActive(true);
            saveButton.SetActive(true);
        }

        public void MoveToGraphics()
        {
            buttons.SetActive(false);

            runOnce = true;

            inAudio = false;
            inGraphics = true;

            cameraAnimator.SetTrigger("graphics");
        }

        public void MoveToAudio()
        {
            buttons.SetActive(false);

            runOnce = true;

            inAudio = true;
            inGraphics = false;

            cameraAnimator.SetTrigger("controls");
        }

        public void Back()
        {
            buttons.SetActive(true);

            saveButton.SetActive(false);
            graphicsPanel.SetActive(false);
            backButton.SetActive(false);

            if (inAudio)
            {
                cameraAnimator.SetTrigger("controls");
            }
            else if (inGraphics)
            {
                cameraAnimator.SetTrigger("graphics");
            }

            inAudio = false;
            inGraphics = false;
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
