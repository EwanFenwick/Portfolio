using UnityEngine;

namespace Portfolio.Utilities
{
	public class DontDestroy : MonoBehaviour
	{
		void Awake()
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
