using ConnectionLost.Core;
using UnityEngine;
using DG.Tweening;


namespace ConnectionLost.Views
{
    public sealed class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private ColorConfig colorConfig;

        public void SetLine(Vector3 pos1, Vector3 pos2)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
        }


        public void SetOpened()
        {
            StopAllTweens();
            lineRenderer.material.DOColor(colorConfig.openColor, 0.5f);
        }

        public void SetClosed()
        {
            StopAllTweens();
            lineRenderer.material.color = colorConfig.closedColor;
        }

        public void SetBlocked()
        {
            StopAllTweens();
            lineRenderer.material.DOColor(colorConfig.blockedColor, 0.8f);
        }

        public void SetEmpty()
        {
            StopAllTweens();
            lineRenderer.material.DOColor(colorConfig.emptyColor, 1.5f);
        }      


        private void StopAllTweens()
        {
            DOTween.Kill(lineRenderer.material);
        }

        private void OnDestroy()
        {
            StopAllTweens();
        }
    }
}
