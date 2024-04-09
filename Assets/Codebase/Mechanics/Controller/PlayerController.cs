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
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _sideAcceleration = 5f;

        [Header("Gameplay mechanics params")]
        [SerializeField] private float _wallBoostStrength = 5000f;
        [SerializeField] private float _jumpForce = 2500f;

        [Header("References")]
        [SerializeField] private UnitContainer _unitContainer;

        private Rigidbody _rigidBody;
        private Vector2 _direction;
        private BoxCollider _triggerCollider;
        private CapsuleCollider _rbCollider;

        private bool _tapIsActive = false;
        private bool _isOnTheWall = false;
        private bool _isFinished = false;
        private bool _allUnitsLost = false;
        private bool _lastUnitRemains = false;

        public UnitContainer UnitContainer => _unitContainer;

        private void Awake()
        {
            _triggerCollider = GetComponent<BoxCollider>();
            _rbCollider = GetComponent<CapsuleCollider>();

            _rigidBody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            _unitContainer.AllignUnits();
            UpdateWheelSize(_unitContainer.Radius);
        }

        private void OnEnable()
        {
            _unitContainer.OnAllUnitsLost += AllUnitsLost;
            _unitContainer.OnLastUnitRemains += LastUnitRemains;
        }

        private void OnDisable()
        {
            _unitContainer.OnAllUnitsLost -= AllUnitsLost;
            _unitContainer.OnLastUnitRemains -= LastUnitRemains;
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

        public void AttachNewUnit(HumanUnit unit)
        {
            _unitContainer.AddUnit(unit);
            UpdateWheelSize(_unitContainer.Radius);
        }

        public HumanUnit GrabAUnit()
        {
            var unit = _unitContainer.RemoveUnit();

            if (_isFinished)
            {
                return unit;
            }

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

        private void AllUnitsLost()
        {
            Debug.Log("Lost all units!");
            _allUnitsLost = true;
        }

        private void LastUnitRemains()
        {
            _lastUnitRemains = true;

            // Rework this
            if (!_isFinished)
            {
                _rigidBody.useGravity = false;
                _rigidBody.velocity = Vector3.zero;
                ServiceLocator.Container.Single<IModelAccesService>().GameplayModel.ActivateView(ViewId.FailView);
            }
        }

        private void UpdateWheelSize(float newRadius)
        {
            //transform.position = new Vector3(transform.position.x, newRadius, transform.position.z);
            _rbCollider.center = new Vector3(0f, -newRadius, 0f);

            _triggerCollider.center = new Vector3(0f, -newRadius, 0f);
            _triggerCollider.size = new Vector3(_triggerCollider.size.x, newRadius + 1, newRadius + 1);
        }


        // Update is called once per frame
        void Update()
        {
            if (_tapIsActive)
            {
                SetDirectionBasedOnPointer();
            }
            else
            {
                SetDirection(Vector2.zero);
            }

            _unitContainer.SetInputDirection(_direction.x);
        }

        private void FixedUpdate()
        {
            if (_allUnitsLost || _lastUnitRemains) return;

            // Logic for climbing walls
            if (_isOnTheWall)
            {
                _rigidBody.velocity = new Vector3(0f, 10f, 0.5f);
                return;
            }

            // Constant forward movement
            if (_rigidBody.velocity.z <= _maxSpeed)
            {
                _rigidBody.AddForce(Vector3.forward * _acceleration);
            }

            // Side movement
            _rigidBody.AddForce(Vector3.right * _direction.x * _sideAcceleration);

            // Prevent inertional side movement
            if (_direction.x == 0f)
            {
                _rigidBody.velocity = new Vector3(0f, _rigidBody.velocity.y, _rigidBody.velocity.z);
            }

            // Set radial rotation speed
            _unitContainer.UpdateWheelSpeed(_rigidBody.velocity.z);
        }
    }
}