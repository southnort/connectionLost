using UnityEngine;


namespace ConnectionLost.Views
{
    [CreateAssetMenu(fileName = "ColorConfig", menuName = "Configs/Color config", order = 1)]
    internal sealed class ColorConfig : ScriptableObject
    {
        public Color closedColor;
        public Color openColor;
        public Color blockedColor;
        public Color emptyColor;
    }
}
