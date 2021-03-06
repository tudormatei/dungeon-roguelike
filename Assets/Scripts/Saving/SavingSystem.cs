using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Dungeon.Saving
{
    /// <summary>
    /// The backend saving stuff for the saving wrapper.
    /// </summary>
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            using(FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            using(FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        private object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
        }

        private void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                saveable.RestoreState(stateDict[saveable.GetUniqueIdentifier()]);
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".save");
        }
    }
}
