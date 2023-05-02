using UnityEngine;
using ConnectionLost.Views;
using ConnectionLost.Core;
using ConnectionLost.Models;
using System;

namespace ConnectionLost.Controllers
{
    internal sealed class BonusViewsBuilder : MonoBehaviour
    {
        [SerializeField] private BonusView baseBonusPrefab;
        [SerializeField] private Sprite[] bonusViewSprites;



        internal BonusView CreateView(ICellContent bonus)
        {
            var view = Instantiate(baseBonusPrefab);

            var sprite = GetBonusIcon(bonus);

            view.SetIcon(sprite);
            return view;
        }

        internal Sprite GetBonusIcon(ICellContent bonus)
        {
            if (bonus == null)
                return null;

            var sprite = bonus switch
            {
                RepairBonus => bonusViewSprites[0],
                HalfHpBonus => bonusViewSprites[1],
                ShieldBonus => bonusViewSprites[2],
                HawkEyeBonus => bonusViewSprites[3],
                WhiteNode =>bonusViewSprites[4],
                _ => throw new InvalidOperationException($"Cant handle type {bonus.GetType()}")
            };

            return sprite;
        }
    }
}
