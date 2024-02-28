using UnityEngine;

namespace Assets.Codebase.Utils.GOComponents
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}