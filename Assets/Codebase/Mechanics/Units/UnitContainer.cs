using System;
using System.Collections.Generic;
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

        [ContextMenu("Allign")]
        public void AllignUnits()
        {
            _radius = _units.Count - 0.7f * _units.Count;
            for (int i = 0; i < _units.Count; i++)
            {
                float circleposition = (float)i / (float)_units.Count;
                float z = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * _radius;
                float y = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * _radius;
                _units[i].transform.position = transform.position + new Vector3(0.0f, y, z);
                _units[i].transform.forward = transform.position - _units[i].transform.position;

                //// Now calculate the tangent vector at this point
                //Vector3 tangent = new Vector3(0, _units[i].transform.position.z, _units[i].transform.position.y);

                //// Use the tangent to set the object's rotation
                //// Quaternion.LookRotation looks along the forward vector with the up vector being Vector3.up by default
                //_units[i].transform.rotation = Quaternion.LookRotation(tangent, _units[i].transform.up);
            }
        }
    }
}