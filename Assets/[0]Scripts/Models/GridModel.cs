using System.Collections.Generic;


namespace ConnectionLost.Models
{
    [System.Serializable]
    public sealed class GridModel
    {
        public List<CellModel> Cells { get; set; }
    }
}
