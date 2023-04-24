using DG.Tweening;
using UnityEngine;
using Yrr.UI;


namespace ConnectionLost.UI
{
    internal sealed class GameStartCanvas : UIScreen
    {
        [SerializeField] private Transform rootObject;

        protected override void ShowProcedure()
        {
            DOTween.KillAll(rootObject);
            rootObject.transform.transform.localScale = Vector3.zero;
            base.ShowProcedure();
            var sequence = DOTween.Sequence(rootObject);

            sequence
                .Append(rootObject.DOScale(1.2f, 0.5f))
                .Append(rootObject.DOScale(1f, 0.3f));
        }

        protected override void HidingProcedure()
        {
            var sequence = DOTween.Sequence(rootObject);

            sequence
                .Append(rootObject.DOScale(1.2f, 0.3f))
                .Append(rootObject.DOScale(0f, 0.5f))
                .AppendCallback(base.HidingProcedure)
                ;
        }

    }
}