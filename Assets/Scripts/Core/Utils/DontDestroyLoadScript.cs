using UnityEngine;

namespace Core.Utils
{
    public class DontDestroyLoadScript : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
