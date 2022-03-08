using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using TMPro;

namespace Dungeon.Networking
{
	public class Register : MonoBehaviour
	{
		string CreateUserURL = "http://dungeon.tudormatei.ro/register.php";

		public void CreateTheUser()
		{
			if (Declarations.usernameIF.text.Length <= 20 && !Declarations.usernameIF.text.Equals("") && !Declarations.usernameIF.text.Equals("Guest") && !Declarations.usernameIF.text.Equals("guest") && !Declarations.usernameIF.text.Contains(" "))
			{
				StartCoroutine(CreateUser());
			}
			else 
			{
				Declarations.errorPanel.SetActive(true);
				GameObject tempg = GameObject.Find("Error Text");
				tempg.GetComponent<TextMeshProUGUI>().text = "Username cannot contain spaces, is too long or is invalid.";
			}
		}
		public IEnumerator CreateUser()
		{
			WWWForm form = new WWWForm();
			form.AddField("usernamePost", Declarations.usernameIF.text);
			form.AddField("passwordPost", ComputeSha256Hash(Declarations.passwordIF.text));
			form.AddField("maxLvlPost", 0);
			form.AddField("maxXPPost", 0);
			form.AddField("mobsKilledPost", 0);

			UnityWebRequest www = UnityWebRequest.Post(CreateUserURL, form);
			yield return www.SendWebRequest();

			string temp = Encoding.UTF8.GetString(www.downloadHandler.data);

			if (temp.Contains("true"))
			{
				print(temp);
				PlayerPrefs.SetString("username", Declarations.usernameIF.text);
				Login.updatePersistentData();
				Login.checkTickedLogin();
				GameObject tempg;
				tempg = GameObject.Find("Account");
				tempg.SetActive(false);
				tempg = GameObject.Find("Register Panel Background");
				tempg.SetActive(false);
			}
			else if (temp.Contains("error1"))
			{
				Declarations.errorPanel.SetActive(true);
				GameObject tempg = GameObject.Find("Error Text");
				tempg.GetComponent<TextMeshProUGUI>().text = "Username is already picked.";
			}
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
	}
}
