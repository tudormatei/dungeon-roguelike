using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using Core.Utils;

namespace Dungeon.Networking
{
	public class Profile : MonoBehaviour
	{
		public GameObject profileParent;
		public GameObject profileTextPrefab;

		void Start()
		{
			StartCoroutine(loadProfileIE());
		}
		public IEnumerator loadProfileIE()
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			yield return StartCoroutine(tempy.GetComponent<NetworkingPipeline>().downloadDbStateIE());

			var sortedPlayerList = NetworkingPipeline.players.OrderByDescending(o => o.maxLvl).ToList();

			GameObject tempyy = GameObject.Find("Persistent Data");
			for (int j = 0; j < sortedPlayerList.Count; j++)
			{
				if (tempyy.GetComponent<PersistentData>().playerName == sortedPlayerList[j].name)
				{
					GameObject profileItem = Instantiate(profileTextPrefab, new Vector3(684f, 308f, 0f), Quaternion.identity, profileParent.transform);
					foreach (TextMeshProUGUI property in profileItem.GetComponentsInChildren<TextMeshProUGUI>())
					{
						if (property.gameObject.name == "Rank")
						{
							property.text = (j + 1).ToString();
						}
						else if (property.gameObject.name == "Username")
						{
							property.text = sortedPlayerList[j].name;
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
