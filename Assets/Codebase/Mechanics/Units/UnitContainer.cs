using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Units
{
    public class UnitContainer : MonoBehaviour
    {
        [SerializeField] private List<Unit> _units;

        public event Action<float> OnRadiusChanged;

        [ContextMenu("Allign")]
        public void AllignUnits()
        {
            float radius = _units.Count;
            OnRadiusChanged?.Invoke(radius);
            for (int i = 0; i < _units.Count; i++)
            {
                float angle = i * Mathf.PI * 2f / radius;
                Vector3 newPos = transform.position + (new Vector3(0f, Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius));
                _units[i].transform.position = newPos;
            }
        }
    }
}