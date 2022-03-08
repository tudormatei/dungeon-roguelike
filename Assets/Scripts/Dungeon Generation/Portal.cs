using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Utils;

namespace Dungeon.DungeonGeneration
{
    /// <summary>
    /// Behaviour for the exit room portal.
    /// </summary>
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(LoadNextLevel());
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(1f);
            PersistentData.Instance.level++;
            SceneManager.LoadScene(1);
        }
    }
}
