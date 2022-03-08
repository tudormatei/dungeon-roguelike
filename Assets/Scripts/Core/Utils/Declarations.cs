using UnityEngine;
using UnityEngine.UI;

namespace Dungeon.Networking
{
	public class Declarations : MonoBehaviour
	{
		#region Main Menu
		[Header("Main Menu")]
		public GameObject errorPanelNS;
		public static GameObject errorPanel;
		public GameObject menuNS;
		public static GameObject menu;
		public GameObject audioSourceSoundtrackNS;
		public static GameObject audioSourceSoundtrack;
		#endregion

		#region Register
		[Header("Register")]
		public InputField usernameIFNS;
		public static InputField usernameIF;

		public InputField passwordIFNS;
		public static InputField passwordIF;
		#endregion

		#region Login
		[Header("Login")]
		public InputField usernameIFNSL;
		public static InputField usernameIFL;
		public InputField passwordIFNSL;
		public static InputField passwordIFL;

		public GameObject profileTickedNS;
		public static GameObject profileTicked;
		#endregion

		#region Seed
		[Header("Seed")]
		public InputField seedIFNS;
		public static InputField seedIF;
		#endregion

		void Awake()
		{
			errorPanel = errorPanelNS;
			menu = menuNS;
			audioSourceSoundtrack = audioSourceSoundtrackNS;
			usernameIF = usernameIFNS;
			passwordIF = passwordIFNS;
			usernameIFL = usernameIFNSL;
			passwordIFL = passwordIFNSL;
			profileTicked = profileTickedNS;
			seedIF = seedIFNS;
		}
	}
}
