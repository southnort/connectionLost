using UnityEngine;
using DG.Tweening;
using ConnectionLost.Core;
using UnityEngine.UI;

namespace ConnectionLost.Views
{
    public sealed class CellView : MonoBehaviour
    {
        [SerializeField] Image baseImage;

        [SerializeField] Color closedColor;
        [SerializeField] Color openColor;
        [SerializeField] Color blockedColor;
        [SerializeField] Color emptyColor;

        private CellStates _currentState;

        public void Click()
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(transform.DOScale(1.5f, 2f))
                .Append(transform.DOScale(1f, 2f));
        }

        public void SetState(CellStates state)
        {
            if(state == _currentState) return;
            state = _currentState;

            switch (state)
            {
                //case CellStates.Closed:
                //    DOTween.instance.ima

                //    baseImage.Twee


            }
        }
    }
}
