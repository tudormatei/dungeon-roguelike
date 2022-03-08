using UnityEngine;

namespace Core.Utils
{
	public class PersistentData : MonoBehaviour
	{
		public string playerName = "";
		public string seed = "";
		public bool newGame = false;
		public int level = 0;

		public static PersistentData Instance;

		void Start()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}

			DontDestroyOnLoad(this.gameObject);
		}
	}
}
