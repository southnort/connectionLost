using UnityEngine;
using TMPro;


namespace ConnectionLost.Views
{
    public sealed class PlayerStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpTmp;
        [SerializeField] private TextMeshProUGUI dmgTmp;

        public void UpdateHp(string hpText)
        {
            hpTmp.text = hpText;
        }

        public void UpdateDmg(string dmgText)
        {
            dmgTmp.text = dmgText;
        }
    }
}
