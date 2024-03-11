using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Xml.Xsl;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Units
{
    public class UnitContainer : MonoBehaviour
    {
        [SerializeField] private List<Unit> _units;

        private float _radius;
        public float Radius => _radius;

        public void AddUnit(Unit newUnit)
        {
            _units.Add(newUnit);
            AllignUnits();
        }

        public Unit RemoveUnit()
        {
            // If no units left
            if (_units.Count < 1)
            {
                Debug.Log("All units lost!");
                return null;
            }

            var unit = _units[0];
            _units.RemoveAt(0);
            unit.transform.SetParent(null);
            unit.DisableInteractions();
            AllignUnits();
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
                // Calculate angle for this object
                float angle = i * angleStep;
                float radianAngle = angle * Mathf.Deg2Rad;

                // Position calculation based on circle formula
                Vector3 positionOnWheel = transform.position + new Vector3(0, Mathf.Sin(radianAngle) * _radius, Mathf.Cos(radianAngle) * _radius);

                // Update the object's position
                _units[i].transform.position = positionOnWheel;

                // Orientation calculation - objects should be tangentially oriented
                // The tangent to a circle at any point is perpendicular to the radius at that point
                // So, we calculate the forward direction for the object as the cross product of the radius vector and the upward axis
                Vector3 radiusVector = positionOnWheel - transform.position;
                Vector3 tangentDirection = Vector3.Cross(radiusVector, Vector3.right).normalized;

                // Align the object's up vector with the calculated tangentDirection
                // Since we want the object's local up vector to be aligned with the tangent, we use the cross product to ensure this alignment
                _units[i].transform.up = tangentDirection;

                // Rotation offset because of pivot in the legs
                _units[i].transform.Rotate(-20f, 0, 0);
            }
        }
    }


}