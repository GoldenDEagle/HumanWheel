using System.Collections;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Controller
{
    public class AntiRollBar : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        //[SerializeField] private WheelCollider WheelL;
        [SerializeField] private WheelCollider _wheelCollider;
        [SerializeField] private float _antiRoll = 5000f;

        private void FixedUpdate()
        {
            WheelHit hit;
            float travelL = 1f;
            float travelR = 1f;

            //var groundedL = WheelL.GetGroundHit(out hit);
            //if (groundedL)
            //    travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

            var groundedR = _wheelCollider.GetGroundHit(out hit);
            if (groundedR)
                travelR = (-_wheelCollider.transform.InverseTransformPoint(hit.point).y - _wheelCollider.radius) / _wheelCollider.suspensionDistance;

            var antiRollForce = (travelL - travelR) * _antiRoll;

            //if (groundedL)
            //    _rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce,
            //           WheelL.transform.position);
            if (groundedR)
                _rigidbody.AddForceAtPosition(transform.forward * antiRollForce,
                       _wheelCollider.transform.position);
        }
    }
}