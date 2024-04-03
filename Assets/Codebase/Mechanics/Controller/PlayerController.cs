using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess;
using Assets.Codebase.Mechanics.Units;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks.Triggers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Codebase.Mechanics.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Wheel parameters")]
        public float motorTorque = 2000;
        public float brakeTorque = 2000;
        public float maxSpeed = 20;
        public float steeringRange = 30;
        public float steeringRangeAtMaxSpeed = 10;
        public float centreOfGravityOffset = -1f;
        public float steerLerpSpeed = 10f;

        [Header("Gameplay mechanics params")]
        [SerializeField] private float _forwardSpeed = 1f;
        [SerializeField] private float _sidePositionLerpSpeed = 1f;
        [SerializeField] private float _xLimit = 9f;
        [SerializeField] private float _wallBoostStrength = 5000f;
        [SerializeField] private float _jumpForce = 2500f;

        [Header("References")]
        [SerializeField] private UnitContainer _unitContainer;

        private WheelController _wheel;
        private Rigidbody _rigidBody;
        private Vector2 _direction;
        private BoxCollider _collider;

        private bool _tapIsActive = false;
        private bool _isOnTheWall = false;
        private bool _isFinished = false;
        private bool _allUnitsLost = false;

        public event Action OnAllUnitsLost;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();

            _rigidBody = GetComponent<Rigidbody>();

            // Adjust center of mass vertically, to help prevent the car from rolling
            _rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

            // Find all child GameObjects that have the WheelControl script attached
            _wheel = GetComponentInChildren<WheelController>();
        }

        void Start()
        {
            _unitContainer.AllignUnits();
            UpdateWheelSize(_unitContainer.Radius);
        }

        private void OnEnable()
        {
            _unitContainer.OnAllUnitsLost += LostAllUnits;
        }

        private void OnDisable()
        {
            _unitContainer.OnAllUnitsLost -= LostAllUnits;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SetDirectionBasedOnPointer()
        {
            var goPos = Camera.main.WorldToScreenPoint(transform.position);
            goPos.z = 0;
            Vector3 mousePos = Pointer.current.position.ReadValue();

            var dist = Vector3.Distance(goPos, mousePos);
            var dir = (mousePos - goPos).normalized;
            SetDirection(dir);
        }

        public void SetTapStatus(bool status)
        {
            _tapIsActive = status;
        }

        public void AttachNewUnit(Unit unit)
        {
            unit.transform.SetParent(_unitContainer.transform);
            _unitContainer.AddUnit(unit);
            UpdateWheelSize(_unitContainer.Radius);
        }

        public Unit GrabAUnit()
        {
            var unit = _unitContainer.RemoveUnit();
            UpdateWheelSize(_unitContainer.Radius);
            return unit;
        }

        public void ReactToWallCollision()
        {
            //var positionOffset = Vector3.up * wallHeight;
            //_rigidBody.Move(_rigidBody.position + positionOffset, _rigidBody.rotation);
            //_rigidBody.AddForce(transform.up * 5000, ForceMode.Impulse);
            _isOnTheWall = true;
        }

        public void EscapeWall()
        {
            _isOnTheWall = false;
            _rigidBody.AddForce(Vector3.forward * _wallBoostStrength, ForceMode.Impulse);
        }

        public void DoJump()
        {
            _rigidBody.AddForce((0.5f * Vector3.forward + Vector3.up) * _jumpForce, ForceMode.Impulse);
        }

        public void FinishCrossed()
        {
            _isFinished = true;
            var input = GetComponent<InputReader>();
            if (input != null) { input.enabled = false; }
            _tapIsActive = false;
        }

        private void LostAllUnits()
        {
            Debug.Log("All units lost!");
            OnAllUnitsLost?.Invoke();
            _allUnitsLost = true;
            _rigidBody.useGravity = false;
            _rigidBody.velocity = Vector3.zero;

            // Rework
            if (!_isFinished)
            {
                ServiceLocator.Container.Single<IModelAccesService>().GameplayModel.ActivateView(ViewId.FailView);
            }
        }

        private void UpdateWheelSize(float newRadius)
        {
            //_wheel.WheelCollider.radius = newRadius + 0.5f;
            //transform.position = new Vector3(transform.position.x, newRadius, transform.position.z);
            _collider.center = new Vector3(0f, -newRadius, 0f);
            _collider.size = new Vector3(_collider.size.x, newRadius + 1, newRadius + 1);
        }


        // Update is called once per frame
        void Update()
        {
            if (_allUnitsLost) return;

            if (_isOnTheWall)
            {
                _rigidBody.velocity = new Vector3(0f, 10f, 0.5f);
                _wheel.WheelCollider.rotationSpeed = 90f;
                return;
            }

            float vInput = 1f;
            float hInput;

            if (_tapIsActive)
            {
                SetDirectionBasedOnPointer();
            }
            else
            {
                SetDirection(Vector2.zero);
            }

            hInput = _direction.x;

            var targetX = _direction.x * _xLimit;
            
            var newX = Mathf.MoveTowards(_rigidBody.position.x, targetX, _sidePositionLerpSpeed * Time.deltaTime);
            //_rigidBody.position = new Vector3(_rigidBody.position.x, _rigidBody.position.y, _rigidBody.position.z);
            _rigidBody.MovePosition(new Vector3(newX, _rigidBody.position.y, _rigidBody.position.z + _forwardSpeed * Time.deltaTime));
            return;

            // Calculate current speed in relation to the forward direction of the car
            // (this returns a negative number when traveling backwards)
            float forwardSpeed = Vector3.Dot(transform.forward, _rigidBody.velocity);


            // Calculate how close the car is to top speed
            // as a number from zero to one
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

            // Use that to calculate how much torque is available 
            // (zero torque at top speed)
            float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

            // …and to calculate how much to steer 
            // (the car steers more gently at top speed)
            float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

            // Check whether the user input is in the same direction 
            // as the car's velocity
            bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);


            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (_wheel.steerable)
            {
                var currentSteer = _wheel.WheelCollider.steerAngle;
                var targetSteer = hInput * currentSteerRange;
                _wheel.WheelCollider.steerAngle = Mathf.Lerp(currentSteer, targetSteer, Time.deltaTime * steerLerpSpeed);
                //_wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (_wheel.motorized)
                {
                    _wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                _wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                _wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                _wheel.WheelCollider.motorTorque = 0;
            }
        }
    }
}