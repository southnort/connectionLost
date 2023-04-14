using ConnectionLost.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace ConnectionLost.Views
{
    public sealed class CellView : MonoBehaviour
    {
        [SerializeField] Image baseImage;
        [SerializeField] ColorConfig colorConfig;
        private CellStates _currentState = CellStates.Undefined;


        public void SetState(CellStates state)
        {
            if (state == _currentState) return;
            _currentState = state;

            StopAllTweens();

            switch (_currentState)
            {
                case CellStates.Closed:
                    baseImage.DOColor(colorConfig.closedColor, 0.01f);
                    break;

                case CellStates.Open:
                    baseImage.DOColor(colorConfig.openColor, 0.5f);
                    break;

                case CellStates.Blocked:
                    baseImage.DOColor(colorConfig.blockedColor, 0.8f);
                    break;

                case CellStates.Empty:
                    baseImage.DOColor(colorConfig.emptyColor, 0.8f);
                    break;
            }
        }

        private void StopAllTweens()
        {
            DOTween.Kill(baseImage);
        }

        private void OnDestroy()
        {
            StopAllTweens();
        }
    }
}
