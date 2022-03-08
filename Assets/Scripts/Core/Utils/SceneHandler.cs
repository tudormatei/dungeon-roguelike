using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Utils
{
	public class SceneHandler : MonoBehaviour
	{
		public void loadSceneByName(string name)
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			GameObject soundtrack = GameObject.Find("Soundtrack");
			if (SceneManager.GetActiveScene().name == "Game")
			{
				Destroy(soundtrack);
			}
			if (tempy && name == "MainMenu")
			{
				Destroy(tempy);
			}
			SceneManager.LoadScene(name, LoadSceneMode.Single);
		}
	}
}
