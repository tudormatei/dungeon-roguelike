using UnityEngine;
using UnityEngine.SceneManagement;
using Dungeon.DungeonGeneration;
using Core.ItemManagement;

namespace Dungeon.UI
{
    /// <summary>
    /// Pause menu and state
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        [Header("Pause Settings")]
        [SerializeField] private GameObject pauseMenu;

        [HideInInspector]
        public static bool isPaused = false;

        private Controls controls;
        private float prevTimeScale = 1f;

        private void Awake()
        {
            controls = new Controls();
        }

        private void Start()
        {
            controls.Gameplay.Escape.performed += ctx => PauseGame();
            
            pauseMenu.SetActive(false);
            isPaused = false;
        }

        public void PauseGame()
        {
            if(DungeonGenerator.dungeonState != DungeonState.completed) { return; }
            if(Interract.panelOpen) { return; }

            isPaused = !isPaused;

            if (isPaused)
            {
                pauseMenu.SetActive(true);
                prevTimeScale = Time.timeScale;
                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = prevTimeScale;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void SaveAndExit()
        {
            Time.timeScale = prevTimeScale;

            SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
