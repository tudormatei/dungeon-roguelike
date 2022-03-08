using System.IO;
using UnityEngine;
using Core.Utils;

namespace Dungeon.Saving
{
    /// <summary>
    /// Main class from where the saving and loading is called.
    /// </summary>
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Start()
        {
            if (!PersistentData.Instance.newGame)
            {
                //Check if file exist
                if (!File.Exists(Path.Combine(Application.persistentDataPath, "save.save")))
                    return;
                if (!File.Exists(Path.Combine(Application.persistentDataPath, "inventory.save")))
                    return;
                if (!File.Exists(Path.Combine(Application.persistentDataPath, "equipment.save")))
                    return;

                LoadLevel();
            }
            else
            {
                PlayerPrefs.SetInt("floor", 0);
                PersistentData.Instance.level = 0;
            }
        }

        public void SaveLevel()
        {
            PlayerPrefs.SetString("seed", PersistentData.Instance.seed);
            PlayerPrefs.SetInt("floor", PersistentData.Instance.level);

            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void LoadLevel()
        {
            PersistentData.Instance.level = PlayerPrefs.GetInt("floor");

            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
