using Assets.Codebase.RaceElements;
using DG.Tweening;
using UnityEngine;

namespace Assets.Codebase.Utils.UI
{
    public class SlidingHand : MonoBehaviour
    {
        [SerializeField] private MovementDirection _startingDirection;
        [SerializeField] private float _movementTime = 2f;
        [SerializeField] private RectTransform _hand;
        [SerializeField] private RectTransform _rightBorder;
        [SerializeField] private RectTransform _leftBorder;

        private void OnEnable()
        {
            StartMovement();
        }

        private void OnDisable()
        {
            DOTween.Kill(_hand);
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
            _hand.DOAnchorPosX(_rightBorder.anchoredPosition.x, _movementTime).SetEase(Ease.InOutSine).OnComplete(MoveLeft);
        }

        private void MoveLeft()
        {
            _hand.DOAnchorPosX(_leftBorder.anchoredPosition.x, _movementTime).SetEase(Ease.InOutSine).OnComplete(MoveRight);
        }
    }
}