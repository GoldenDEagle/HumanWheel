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
                float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * _radius;
                float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * _radius;
                _units[i].transform.position = transform.position + new Vector3(0.0f, z, x);
                _units[i].transform.forward = (_units[i].transform.position - transform.position);
            }
        }
    }
}