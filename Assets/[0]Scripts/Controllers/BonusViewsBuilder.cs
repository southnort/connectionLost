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

            Sprite sprite;
            if (bonus is RepairBonus)
            {
                sprite = bonusViewSprites[0];
            }
            else if (bonus is HalfHpBonus)
            {
                sprite = bonusViewSprites[1];
            }
            else if (bonus is ShieldBonus)
            {
                sprite = bonusViewSprites[2];
            }
            else if (bonus is HawkeyeBonus)
            {
                sprite = bonusViewSprites[3];
            }


            else
            {
                throw new InvalidOperationException($"Cant handle type {bonus.GetType()}");
            }

            return sprite;
        }
    }
}
