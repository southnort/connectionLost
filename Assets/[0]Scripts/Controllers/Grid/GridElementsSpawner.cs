using ConnectionLost.Views;
using UnityEngine;

namespace ConnectionLost.Controllers
{
    internal sealed class GridElementsSpawner : MonoBehaviour
    {
        [SerializeField] private CellView cellPrefab;
        [SerializeField] private Line linePrefab;

        public CellView CreateCell()
        {
            var cell = Instantiate(cellPrefab);
            return cell;
        }

        public Line CreateLine()
        {
            var line = Instantiate(linePrefab);
            return line;
        }
    }
}
