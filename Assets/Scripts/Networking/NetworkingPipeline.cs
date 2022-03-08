using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Dungeon.Networking
{
	public class NetworkingPipeline : MonoBehaviour
	{
		public string[] users;
		public static List<PlayerNetworking> players = new List<PlayerNetworking>();
		public static int usersLength;

		public void downloadDbState()
		{
			StartCoroutine(downloadDbStateIE());
		}
		public IEnumerator downloadDbStateIE()
		{
			players = new List<PlayerNetworking>();
			string UsersDataURL = "http://dungeon.tudormatei.ro/stats.php";
			UnityWebRequest usersData = UnityWebRequest.Get(UsersDataURL);
			yield return usersData.SendWebRequest();
			string usersDataString = Encoding.UTF8.GetString(usersData.downloadHandler.data);
			usersLength = 0;
			for (int i = 0; i < usersDataString.Length; i++)
			{
				if (usersDataString[i] == ';')
				{
					usersLength++;
				}
			}
			users = usersDataString.Split(';');
			for (int i = 0; i < usersLength; i++)
			{
				if (users[i] != null)
				{
					players.Add(new PlayerNetworking(GetDataValue(users[i], "username:"),
													Int32.Parse(GetDataValue(users[i], "maxLvl:")),
													Int32.Parse(GetDataValue(users[i], "maxXP:")),
													Int32.Parse(GetDataValue(users[i], "mobsKilled:"))));
				}
			}
		}
		public void updateUser(string name, int maxLvl, int maxXP, int mobsKilled)
		{
			StartCoroutine(updateUserIE(name, maxLvl, maxXP, mobsKilled));
		}
		public IEnumerator updateUserIE(string name, int maxLvl, int maxXP, int mobsKilled)
		{
			if (!name.Equals("Guest"))
			{
				yield return StartCoroutine(downloadDbStateIE());
				int userid = 0;
				for (int i = 0; i < usersLength; i++)
				{
					if (players[i].name.Equals(name))
					{
						userid = i + 1;
						break;
					}
				}
				string updateUserURL = "http://dungeon.tudormatei.ro/updatePlayer.php";
				WWWForm form = new WWWForm();
				form.AddField("userid", userid);
				form.AddField("maxLvlPost", maxLvl);
				form.AddField("maxXPPost", maxXP);
				form.AddField("mobsKilledPost", mobsKilled);

				UnityWebRequest www = UnityWebRequest.Post(updateUserURL, form);
				yield return www.SendWebRequest();
				yield return StartCoroutine(downloadDbStateIE());
			}
		}
		string GetDataValue(string data, string index)
		{
			string value = data.Substring(data.IndexOf(index) + index.Length);
			if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
			return value;
		}
	}

}