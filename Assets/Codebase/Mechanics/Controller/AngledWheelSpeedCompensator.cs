using System.Collections;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Controller
{
    public class AngledWheelSpeedCompensator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WheelCollider _wheelCollider;
        [SerializeField] private float _compensationForcePerDegree = 1f;

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.z > 10f) return;

            var wheelRotation = _wheelCollider.steerAngle;
            var appliedForce = Mathf.Abs(_compensationForcePerDegree * wheelRotation);

            _rigidbody.AddForce(Vector3.forward * appliedForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}