using UnityEngine;
using Yrr.UI;
using DG.Tweening;


namespace ConnectionLost.UI
{
    public class MainGameCanvas : UIScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;

        protected override void ShowProcedure()
        {
            canvasGroup.alpha = 0;
            base.ShowProcedure();            

            var seq = DOTween.Sequence().SetUpdate(true);
            seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.3f, .5f));

            for (int i = 0; i < 3; i++)
            {
                seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.2f, .05f));
                seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.5f, .15f));
            }

            seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, .7f, .5f));
            seq.Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, .3f));
        }


    }
}