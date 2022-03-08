using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using Core.Utils;

namespace Dungeon.Networking
{
	public class Leaderboard : MonoBehaviour
	{
		public GameObject leaderboardParent;
		public GameObject leaderboardItemPrefab;

		void Start()
		{
			StartCoroutine(loadleaderboardIE());
		}
		public IEnumerator loadleaderboardIE()
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			yield return StartCoroutine(tempy.GetComponent<NetworkingPipeline>().downloadDbStateIE());

			var sortedPlayerList = NetworkingPipeline.players.OrderByDescending(o => o.maxLvl).ToList();

			print(sortedPlayerList[0].maxLvl);

			int i = 1;
			while (i <= 5 && i <= sortedPlayerList.Count)
			{
				GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, new Vector3(684f, 358f + -50f * i, 0f), Quaternion.identity, leaderboardParent.transform);
				foreach (TextMeshProUGUI property in leaderboardItem.GetComponentsInChildren<TextMeshProUGUI>())
				{
					if (property.gameObject.name == "Rank")
					{
						property.text = i.ToString();
					}
					else if (property.gameObject.name == "Username")
					{
						property.text = sortedPlayerList[i - 1].name;
					}
					else if (property.gameObject.name == "Max Floor")
					{
						property.text = sortedPlayerList[i - 1].maxXP.ToString();
					}
					else if (property.gameObject.name == "Max Level")
					{
						property.text = sortedPlayerList[i - 1].maxLvl.ToString();
					}
				}
				i++;
			}

			GameObject tempyy = GameObject.Find("Persistent Data");
			for (int j = 0; j < sortedPlayerList.Count; j++)
			{
				if (tempyy.GetComponent<PersistentData>().playerName == sortedPlayerList[j].name)
				{
					GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, new Vector3(684f, 358f + -50f * i, 0f), Quaternion.identity, leaderboardParent.transform);
					foreach (TextMeshProUGUI property in leaderboardItem.GetComponentsInChildren<TextMeshProUGUI>())
					{
						if (property.gameObject.name == "Rank")
						{
							property.text = (j + 1).ToString();
						}
						else if (property.gameObject.name == "Username")
						{
							property.text = sortedPlayerList[j].name + " (you)";
						}
						else if (property.gameObject.name == "Max Floor")
						{
							property.text = sortedPlayerList[j].maxXP.ToString();
						}
						else if (property.gameObject.name == "Max Level")
						{
							property.text = sortedPlayerList[j].maxLvl.ToString();
						}
					}
					break;
				}
			}
		}
	}

}
