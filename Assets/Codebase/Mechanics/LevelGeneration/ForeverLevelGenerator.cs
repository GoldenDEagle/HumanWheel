using Assets.Codebase.Mechanics.Controller;
using Dreamteck.Forever;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Codebase.Mechanics.LevelGeneration
{
    public class ForeverLevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private ForeverLevel _level;
        [SerializeField]
        private int _activeSegmentsBehind;
        [SerializeField]
        private int _activeSegmentsAhead;

        private Transform _playerTransform;

        private List<GameObject> _selectedSegments = new List<GameObject>();
        private List<LevelSegment> _generatedSegments = new List<LevelSegment>();

        private float _progress;

        public float Progress => _progress;
        public ForeverLevel Level { get => _level; set => _level = value; }
        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public event Action OnGenerated;

        // Move away from here
        private void Start()
        {
            Generate();
        }

        private void Update()
        {
            if (_playerTransform != null)
            {
                int current = -1;
                for (int i = 0; i < _generatedSegments.Count; i++)
                {
                    var segment = _generatedSegments[i];
                    if (segment.customEntrance.position.z <= _playerTransform.position.z &&
                        segment.customExit.position.z >= _playerTransform.position.z)
                    {
                        current = i;
                        break;
                    }
                }
                if (current == -1)
                {
                    var segment = _generatedSegments[0];
                    if (segment.customEntrance.position.z >= _playerTransform.position.z)
                    {
                        current = 0;
                    }
                    else
                    {
                        current = _generatedSegments.Count - 1;
                    }
                }
                int start = current - _activeSegmentsBehind;
                int end = current + _activeSegmentsAhead;
                start = Mathf.Clamp(start, 0, _generatedSegments.Count - 1);
                end = Mathf.Clamp(end, 0, _generatedSegments.Count - 1);

                for (int i = 0; i < _generatedSegments.Count; i++)
                {
                    var segment = _generatedSegments[i];
                    if (i >= start && i <= end)
                    {
                        if (!segment.gameObject.activeSelf) segment.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (segment.gameObject.activeSelf) segment.gameObject.SetActive(false);
                    }
                }
            }
        }
        public void Generate()
        {
            foreach (var segment in _generatedSegments)
            {
                Destroy(segment.gameObject);
            }
            _generatedSegments.Clear();
            _selectedSegments.Clear();
            SelectSegments();
            StartCoroutine(GenerationCoroutine());
        }
        private IEnumerator GenerationCoroutine()
        {
            Vector3 lastPos = Vector3.zero;
            int totalSergments = _selectedSegments.Count;

            foreach (var segment in _selectedSegments)
            {
                var instantiated = Instantiate(segment);
                if (instantiated.TryGetComponent<LevelSegment>(out var levelSegment))
                {
                    if (levelSegment.customEntrance == null || levelSegment.customExit == null)
                    {
                        throw new Exception($"Segment {instantiated.name} doesn't have entrance and exit");
                    }
                    var entranceLocalPos = instantiated.transform.worldToLocalMatrix.MultiplyPoint(levelSegment.customEntrance.position);
                    instantiated.transform.position = lastPos - entranceLocalPos;
                    levelSegment.InvokeOnExtruded();
                    lastPos = levelSegment.customExit.position;
                    _generatedSegments.Add(levelSegment);
                }
                else
                {
                    throw new Exception("Segment doesn't have LevelSegment component");
                }
                _progress += 1.0f / totalSergments;
                yield return null;
            }
            _progress = 1.0f;
            OnGenerated?.Invoke();

            // Remove later
            _playerTransform = FindObjectOfType<PlayerController>().transform;
        }
        private void SelectSegments()
        {
            var sequenceCollection = _level.sequenceCollection;
            var sequences = sequenceCollection.sequences;

            var initial = new SegmentDefinition();
            initial.nested = true;

            Stack<SegmentDefinition> openSet = new Stack<SegmentDefinition>();

            for (int i = sequences.Length - 1; i >= 0; i--)
            {
                var current = new SegmentDefinition();
                current.nestedSequence = sequences[i];
                current.nested = true;
                openSet.Push(current);
            }
            List<SegmentDefinition> currentOrder = new List<SegmentDefinition>();

            while (openSet.Count > 0)
            {
                var current = openSet.Pop();
                if (current.nested)
                {
                    currentOrder.Clear();
                    var currentSegments = current.nestedSequence.segments;

                    if (current.nestedSequence.type == SegmentSequence.Type.Ordered)
                    {
                        for (int i = currentSegments.Length - 1; i >= 0; i--)
                        {
                            openSet.Push(currentSegments[i]);
                        }
                    }
                    else if (current.nestedSequence.type == SegmentSequence.Type.RandomByChance)
                    {
                        List<SegmentDefinition> currentSegmentsList = new List<SegmentDefinition>();
                        currentSegmentsList.AddRange(currentSegments);

                        float totalChances = 0.0f;
                        foreach (var segment in currentSegments)
                        {
                            totalChances += segment.randomPickChance;
                        }
                        for (int i = 0; i < current.nestedSequence.spawnCount && currentSegmentsList.Count > 0; i++)
                        {
                            float selection = Random.Range(0.0f, totalChances);
                            int currentSelectedSegment = 0;
                            selection -= currentSegmentsList[currentSelectedSegment].randomPickChance;
                            while (selection > 0.0f)
                            {
                                currentSelectedSegment++;
                                selection -= currentSegmentsList[currentSelectedSegment].randomPickChance;
                            }
                            openSet.Push(currentSegmentsList[currentSelectedSegment]);
                            if (current.nestedSequence.preventRepeat)
                            {
                                totalChances -= currentSegmentsList[currentSelectedSegment].randomPickChance;
                                currentSegmentsList.RemoveAt(currentSelectedSegment);
                            }
                        }
                    }
                    else if (current.nestedSequence.type == SegmentSequence.Type.Shuffled)
                    {
                        float totalChances = 0.0f;
                        foreach (var segment in currentSegments)
                        {
                            totalChances += segment.randomPickChance;
                        }
                        for (int i = 0; i < current.nestedSequence.spawnCount; i++)
                        {
                            float selection = Random.Range(0.0f, totalChances);
                            int currentSelectedSegment = 0;
                            selection -= currentSegments[currentSelectedSegment].randomPickChance;
                            while (selection > 0.0f)
                            {
                                currentSelectedSegment++;
                                selection -= currentSegments[currentSelectedSegment].randomPickChance;
                            }
                            openSet.Push(currentSegments[currentSelectedSegment]);
                        }
                    }
                    else
                    {
                        throw new Exception("Not supported type");
                    }
                }
                else
                {
                    _selectedSegments.Add(current.prefab);
                }
            }
        }
    }
}