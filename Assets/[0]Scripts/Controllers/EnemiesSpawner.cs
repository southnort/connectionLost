using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Models.Enemy;
using ConnectionLost.Views;
using System;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    internal sealed class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyView[] enemyPrefabs;
        [SerializeField] private EnemyView[] corePrefabs;


        public EnemyView CreateView(ICellContent enemy)
        {
            EnemyView prefab;
            if (enemy is CoreModel core)
            {
                prefab = corePrefabs[(int)core.CoreDifficult];
            }

            else if (enemy is FirewallModel)
            {
                prefab = enemyPrefabs[0];
            }
            else if (enemy is AntivirusModel)
            {
                prefab = enemyPrefabs[1];
            }
            else if (enemy is HealerModel)
            {
                prefab = enemyPrefabs[2];
            }
            else if (enemy is SuppressorModel)
            {
                prefab = enemyPrefabs[3];
            }


            else
            {
                throw new InvalidOperationException($"Cant handle type {enemy.GetType()}");
            }

            return Instantiate(prefab, transform);
        }
    }
}