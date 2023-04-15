using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace ConnectionLost.Views
{
    public sealed class CellView : MonoBehaviour
    {
        [SerializeField] Image baseImage;
        [SerializeField] ColorConfig colorConfig;


        public void SetOpened()
        {
            StopAllTweens();
            baseImage.DOColor(colorConfig.openColor, 0.5f);
        }

        public void SetClosed()
        {
            StopAllTweens();
            baseImage.DOColor(colorConfig.closedColor, 0.01f);
        }

        public void SetBlocked()
        {
            StopAllTweens();
            baseImage.DOColor(colorConfig.blockedColor, 0.8f);
        }

        public void SetEmpty()
        {
            StopAllTweens();
            baseImage.DOColor(colorConfig.emptyColor, 0.8f);
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
