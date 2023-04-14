using ConnectionLost.Core;
using UnityEngine;
using DG.Tweening;


namespace ConnectionLost.Views
{
    public sealed class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private ColorConfig colorConfig;

        private CellStates _currentState = CellStates.Undefined;

        public void SetLine(Vector3 pos1, Vector3 pos2)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
        }

        public void SetState(CellStates state)
        {
            if (state == _currentState) return;
            _currentState = state;

            DOTween.Kill(lineRenderer.material);

            switch (_currentState)
            {
                case CellStates.Closed:
                    lineRenderer.material.color = colorConfig.closedColor;
                    break;

                case CellStates.Open:
                    lineRenderer.material.DOColor(colorConfig.openColor, 0.5f);
                    break;

                case CellStates.Blocked:
                    lineRenderer.material.DOColor(colorConfig.blockedColor, 0.8f);
                    break;

                case CellStates.Empty:
                    lineRenderer.material.DOColor(colorConfig.emptyColor, 1.5f);
                    break;
            }
        }

        void OnDestroy()
        {
            DOTween.Kill(lineRenderer.material);
        }
    }
}
