using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using Core.Utils;

namespace Dungeon.Networking
{
	public class Login : MonoBehaviour
	{
		string LoginURL = "http://dungeon.tudormatei.ro/login.php";
		public void LoginTheUser()
		{
			StartCoroutine(LoginF());
		}
		public IEnumerator LoginF()
		{
			WWWForm form = new WWWForm();
			form.AddField("usernamePost", Declarations.usernameIFL.text);
			form.AddField("passwordPost", ComputeSha256Hash(Declarations.passwordIFL.text));

			UnityWebRequest www = UnityWebRequest.Post(LoginURL, form);
			yield return www.SendWebRequest();

			string temp = Encoding.UTF8.GetString(www.downloadHandler.data);

			if (temp.Contains("true"))
			{
				GameObject tempy = GameObject.Find("Persistent Data");
				tempy.GetComponent<PersistentData>().playerName = Declarations.usernameIFL.text;
				PlayerPrefs.SetString("username", Declarations.usernameIFL.text);
				checkTickedLogin();
				GameObject tempg;
				tempg = GameObject.Find("Account");
				tempg.SetActive(false);
				tempg = GameObject.Find("Login Panel Background");
				tempg.SetActive(false);
			}
			else if (temp.Contains("false"))
			{
				Declarations.errorPanel.SetActive(true);
				GameObject tempg = GameObject.Find("Error Text");
				tempg.GetComponent<TextMeshProUGUI>().text = "The password is incorrect.";
			}
		}
		public void startNewGame()
		{
			if (File.Exists(Path.Combine(Application.persistentDataPath, "save.save")))
			{
				File.Delete(Path.Combine(Application.persistentDataPath, "save.save"));
			}
			if (File.Exists(Path.Combine(Application.persistentDataPath, "inventory.save")))
			{
				File.Delete(Path.Combine(Application.persistentDataPath, "inventory.save"));
			}
			if (File.Exists(Path.Combine(Application.persistentDataPath, "equipment.save")))
			{
				File.Delete(Path.Combine(Application.persistentDataPath, "equipment.save"));
			}

			PlayerPrefs.SetString("seed", Declarations.seedIF.text);
			updatePersistentData();
			SceneManager.LoadScene("Game", LoadSceneMode.Single);
			Declarations.menu.SetActive(false);
			GameObject tempy = GameObject.Find("Persistent Data");
			tempy.GetComponent<PersistentData>().newGame = true;
		}
		public void continueGame()
		{
			if (!File.Exists(Path.Combine(Application.persistentDataPath, "save.save"))) return;

			if (!File.Exists(Path.Combine(Application.persistentDataPath, "inventory.save"))) return;

			if (!File.Exists(Path.Combine(Application.persistentDataPath, "equipment.save"))) return;

			updatePersistentData();
			SceneManager.LoadScene("Game", LoadSceneMode.Single);
			Declarations.menu.SetActive(false);
			GameObject tempy = GameObject.Find("Persistent Data");
			tempy.GetComponent<PersistentData>().newGame = false;
		}
		static string ComputeSha256Hash(string rawData)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
		void Start()
		{
			checkTickedLogin();
			updatePersistentData();
		}
		public static void checkTickedLogin()
		{
			if (PlayerPrefs.HasKey("username"))
			{
				if (!PlayerPrefs.GetString("username").Equals("Guest"))
				{
					Declarations.profileTicked.SetActive(true);
				}
			}
			else
			{
				Declarations.profileTicked.SetActive(false);
			}
		}
		public static void updatePersistentData()
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			if (PlayerPrefs.HasKey("username"))
			{
				if (!PlayerPrefs.GetString("username").Equals("Guest"))
				{
					tempy.GetComponent<PersistentData>().playerName = PlayerPrefs.GetString("username");
				}
				else
				{
					tempy.GetComponent<PersistentData>().playerName = "Guest";
				}
			}
			else
			{
				tempy.GetComponent<PersistentData>().playerName = "Guest";
			}
			if (PlayerPrefs.HasKey("seed"))
			{
				tempy.GetComponent<PersistentData>().seed = PlayerPrefs.GetString("seed");
			}
		}
		public void logoutUser()
		{
			PlayerPrefs.DeleteKey("username");
			GameObject tempy = GameObject.Find("Persistent Data");
			tempy.GetComponent<PersistentData>().seed = "";
			tempy.GetComponent<PersistentData>().playerName = "Guest";
			checkTickedLogin();
		}
		public void quitApp()
		{
			if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
			{
				Application.Quit();
			}
			else
			{
				Declarations.menu.SetActive(false);
			}
		}
	}
}


