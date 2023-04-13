using UnityEngine;

namespace ConnectionLost.Views
{
    public sealed class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;


        public void SetLine(Vector3 pos1, Vector3 pos2)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
        }
    }
}
