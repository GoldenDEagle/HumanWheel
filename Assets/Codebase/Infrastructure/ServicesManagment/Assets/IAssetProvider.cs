using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Assets
{
    /// <summary>
    /// Service for assets load.
    /// </summary>
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(GameObject prefab);
        T LoadResource<T>(string path) where T : UnityEngine.Object;
    }
}
