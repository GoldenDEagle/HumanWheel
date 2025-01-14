﻿using DG.Tweening;
using UnityEngine;

namespace Assets.Codebase.RaceElements
{
    public class MovingWall : MonoBehaviour
    {
        [SerializeField] private MovementDirection _startingDirection;
        [SerializeField] private float _movementTime = 2f;
        [SerializeField] private Transform _wall;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _leftBorder;

        private void OnEnable()
        {
            StartMovement();
        }

        private void OnDisable()
        {
            DOTween.Kill(_wall);
        }

        private void StartMovement()
        {
            switch (_startingDirection)
            {
                case MovementDirection.Left:
                    MoveLeft();
                    break;
                case MovementDirection.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        private void MoveRight()
        {
            _wall.DOMoveX(_rightBorder.position.x, _movementTime).SetEase(Ease.InOutSine).OnComplete(MoveLeft);
        }

        private void MoveLeft()
        {
            _wall.DOMoveX(_leftBorder.position.x, _movementTime).SetEase(Ease.InOutSine).OnComplete(MoveRight);
        }
    }

    public enum MovementDirection
    {
        Right,
        Left,
    }
}