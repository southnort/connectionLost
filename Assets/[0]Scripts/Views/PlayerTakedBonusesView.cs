using System;
using UnityEngine;


namespace ConnectionLost.Views
{
    public sealed class PlayerTakedBonusesView : MonoBehaviour
    {
        [SerializeField] private TakedBonusView[] bonusViews;


        public void SetInitialized(int index, Sprite bonusIcon, int count)
        {
            bonusViews[index].SetInitialized(bonusIcon, count);
        }

        public void UpdateCount(int index, int count)
        {
            bonusViews[index].UpdateCountInfo(count);
        }

        public void ClearBonus(int index)
        {
            bonusViews[index].Remove();
        }
    }
}
