using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Assets
{
    /// <summary>
    /// Provides assets using Resources folder.
    /// </summary>
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(GameObject prefab)
            => Object.Instantiate(prefab);

        public T LoadResource<T>(string path) where T : Object
            => Resources.Load<T>(path);
    }
}
