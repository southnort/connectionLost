using UnityEngine;
using TMPro;


namespace ConnectionLost.Views
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpTmp;
        [SerializeField] private TextMeshProUGUI attackTmp;

        public void SetInitialData(string hpText, string attackText)
        {
            hpTmp.text = hpText;
            attackTmp.text = attackText;
        }

        public void UpdateHp(string hpText)
        {
            hpTmp.text = hpText;
        }
    }
}