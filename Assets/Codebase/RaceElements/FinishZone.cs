using UnityEngine;

namespace CodeBase.RaceElements
{
    [RequireComponent((typeof(Collider)))]
    public class FinishZone : MonoBehaviour
    {
        //private const string PlayerLayerName = "Player";
        
        //private int _playerLayer;
        //private TriangleGrid _triangleGrid;
        //private ISpawnService _spawnService;

        //private void Awake()
        //{
        //    _spawnService = ServiceLocator.Container.Single<ISpawnService>();
        //    _triangleGrid = GetComponent<TriangleGrid>();
        //    _playerLayer = LayerMask.NameToLayer(PlayerLayerName);
        //}

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.layer == _playerLayer)
        //    {
        //        _spawnService.Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //        _spawnService.MasterUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //        foreach (GameObject unit in _spawnService.Player.UnitList)
        //        {
        //            unit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //        }

        //        _triangleGrid.enabled = true;
        //    }
        //}
    }
}
