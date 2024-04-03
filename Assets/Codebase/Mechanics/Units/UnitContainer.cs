using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Xml.Xsl;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Units
{
    public class UnitContainer : MonoBehaviour
    {
        [SerializeField] private List<Unit> _units;
        [SerializeField] private Transform _humanCircle;
        [SerializeField] private float _maxTiltAngle = 30f;
        [SerializeField] private float _tiltLerpSpeed = 10f;

        private float _wheelForwardSpeed = 0f;
        private float _currentSideInput = 0f;
        private float _radius;
        public float Radius => _radius;

        public event Action OnAllUnitsLost;

        public void AddUnit(Unit newUnit)
        {
            newUnit.transform.SetParent(_humanCircle);
            _units.Add(newUnit);
            AllignUnits();
        }

        public Unit RemoveUnit()
        {
            // If no units left
            if (_units.Count < 1)
            {
                Debug.Log("No available units!");
                return null;
            }

            var unit = _units[0];
            _units.RemoveAt(0);
            unit.transform.SetParent(null);
            unit.DisableInteractions();
            AllignUnits();

            // If removed last unit
            if (_units.Count < 1)
            {
                OnAllUnitsLost?.Invoke();
            }

            return unit;
        }

        [ContextMenu("Allign")]
        public void AllignUnits()
        {
            _radius = _units.Count - 0.6f * _units.Count;

            int numberOfObjects = _units.Count;
            float angleStep = 360.0f / numberOfObjects;

            for (int i = 0; i < numberOfObjects; i++)
            {
                // Calculate angle for this object in radians
                float angle = i * angleStep;
                float radianAngle = angle * Mathf.Deg2Rad;

                // Position calculation based on circle formula in local space
                Vector3 localPositionOnWheel = new Vector3(0, Mathf.Sin(radianAngle) * _radius, Mathf.Cos(radianAngle) * _radius);

                // Convert local position to world position
                Vector3 worldPositionOnWheel = transform.TransformPoint(localPositionOnWheel);

                // Update the object's position
                _units[i].transform.position = worldPositionOnWheel;

                Vector3 localRadiusVector = localPositionOnWheel - Vector3.zero; // Local radius vector
                Vector3 worldRadiusVector = transform.TransformDirection(localRadiusVector); // Convert local radius vector to world space

                Vector3 tangentDirection = Vector3.Cross(worldRadiusVector, transform.right).normalized; // Cross product in world space

                // Align the object's up vector with the calculated tangentDirection
                _units[i].transform.up = tangentDirection;

                //// Additional rotation beacause of leg pivot
                //_units[i].transform.Rotate(-20f, 0f, 0f);
            }
        }

        private void Update()
        {
            var rotationSpeedRad = _wheelForwardSpeed / _radius;
            var rotationSpeedDegrees = rotationSpeedRad * 57.3f;

            // Rotate around X axis
            _humanCircle.Rotate(Vector3.right, Time.deltaTime * rotationSpeedDegrees);

            // Rotate around Z axis
            var targetRotation = Quaternion.Euler(0f, 0f, -_currentSideInput * _maxTiltAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _tiltLerpSpeed * Time.deltaTime);
        }

        public void UpdateWheelSpeed(float forwardSpeed)
        {
            _wheelForwardSpeed = forwardSpeed;
        }

        public void SetInputDirection(float sideInput)
        {
            _currentSideInput = sideInput;
        }
    }
}