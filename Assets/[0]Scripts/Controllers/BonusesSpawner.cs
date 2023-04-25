using UnityEngine;
using ConnectionLost.Views;
using ConnectionLost.Core;
using ConnectionLost.Models;
using System;

namespace ConnectionLost.Controllers
{
    internal sealed class BonusesSpawner : MonoBehaviour
    {
        [SerializeField] private BonusView[] bonusesPrefabs;


        public BonusView CreateView(ICellContent bonus)
        {
            BonusView prefab;
            if (bonus is RepairBonus)
            {
                prefab = bonusesPrefabs[0];
            }
            else if (bonus is HalfHpBonus)
            {
                prefab = bonusesPrefabs[1];
            }
            else if (bonus is ShieldBonus)
            {
                prefab = bonusesPrefabs[2];
            }
            else if (bonus is HawkeyeBonus)
            {
                prefab = bonusesPrefabs[3];
            }


            else
            {
                throw new InvalidOperationException($"Cant handle type {bonus.GetType()}");
            }

            return Instantiate(prefab, transform);
        }
    }
}
