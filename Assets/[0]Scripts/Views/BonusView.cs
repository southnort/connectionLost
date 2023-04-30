using UnityEngine;
using UnityEngine.UI;

namespace ConnectionLost.Views
{
    public sealed class BonusView : MonoBehaviour
    {
        [SerializeField] private Image bonusIcon;


        public void SetIcon(Sprite icon)
        {
            bonusIcon.sprite = icon;
        }
    }
}
