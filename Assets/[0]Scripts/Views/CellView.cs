using UnityEngine;
using TMPro;

namespace ConnectionLost.Views
{
    public sealed class CellView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI label;

        public void SetLabel(string text)
        {
            label.text = text;
        }

    }
}
