using System.Collections;
using UnityEngine;

namespace Dungeon.Networking
{
	public class DummyNetworking : MonoBehaviour
	{
		public void testDownload()
		{
			StartCoroutine(testDownloadIE());
		}
		public IEnumerator testDownloadIE()
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			yield return StartCoroutine(tempy.GetComponent<NetworkingPipeline>().downloadDbStateIE());
			print(NetworkingPipeline.players[0].name);
		}
		public void testUpdate()
		{
			StartCoroutine(testUpdateIE());
		}
		public IEnumerator testUpdateIE()
		{
			GameObject tempy = GameObject.Find("Persistent Data");
			yield return StartCoroutine(tempy.GetComponent<NetworkingPipeline>().updateUserIE("test1", 60, 60, 60));
		}
	}

}
