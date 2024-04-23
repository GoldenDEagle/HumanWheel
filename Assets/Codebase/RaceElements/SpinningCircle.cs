using UnityEngine;

namespace Assets.Codebase.RaceElements
{
    public class SpinningCircle : MonoBehaviour
    {
        [SerializeField] private Transform _circleModel;
        [SerializeField] private float rotationSpeedDegrees = 30f;

        // Update is called once per frame
        void Update()
        {
            _circleModel.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeedDegrees);
        }
    }
}