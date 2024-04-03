using System.Collections;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Controller
{
    public class WheelController : MonoBehaviour
    {
        public Transform _wheelModel;

        [HideInInspector] public WheelCollider WheelCollider;

        // Create properties for the CarControl script
        // (You should enable/disable these via the 
        // Editor Inspector window)
        public bool steerable;
        public bool motorized;

        Vector3 position;
        Quaternion rotation;

        private void Awake()
        {
            //WheelCollider = GetComponent<WheelCollider>();
            //WheelCollider.ConfigureVehicleSubsteps(5, 12, 15);
        }

        // Update is called once per frame
        void Update()
        {
            // Get the Wheel collider's world pose values and
            // use them to set the wheel model's position and rotation
            //WheelCollider.GetWorldPose(out position, out rotation);
            //_wheelModel.transform.position = position;
            //_wheelModel.transform.rotation = rotation;
        }
    }
}