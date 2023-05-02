using ConnectionLost.Core;


namespace ConnectionLost.Models
{
    public sealed class WhiteNode : ICellContent
    {
        public WhiteNode(ICellContent content)
        {
            WhiteNodeContent = content;
        }

        public bool IsBlock => false;
        public bool IsCanBlocked => false;

        public ICellContent WhiteNodeContent { get; }
    }
}
