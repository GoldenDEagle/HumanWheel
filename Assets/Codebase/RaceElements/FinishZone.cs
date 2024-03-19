using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess;
using Assets.Codebase.Mechanics.Controller;
using Assets.Codebase.RaceElements;
using Assets.Codebase.Views.Base;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.RaceElements
{
    [RequireComponent((typeof(Collider)))]
    public class FinishZone : MonoBehaviour
    {
        [SerializeField] private List<Wall> _walls;
        [SerializeField] private Transform _cameraPoint;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerWheel = other.GetComponent<PlayerController>();
            if (playerWheel)
            {
                _collider.enabled = false;
                Debug.Log("Finish crossed!");

                // Connect with player
                playerWheel.FinishCrossed();
                playerWheel.OnAllUnitsLost += AllUnitsPlaced;

                // Set Camera
                var camera = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
                if (camera)
                {
                    camera.Follow = _cameraPoint;
                }
            }
        }

        private void AllUnitsPlaced()
        {
            int humanCount = 0;
            foreach (var wall in _walls)
            {
                if (wall.IsOccupied)
                {
                    humanCount++;
                }
            }

            Debug.Log("GAME SCORE: " + humanCount);
            ServiceLocator.Container.Single<IModelAccesService>().GameplayModel.ActivateView(ViewId.EndgameView);
        }
    }
}
